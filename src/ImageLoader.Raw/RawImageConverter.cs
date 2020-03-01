using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using BitMiracle.LibTiff.Classic;
using ImageLoader.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ImageLoader.Raw
{
    public class RawImageConverter : IImageConverter
    {
        public IReadOnlyList<string> SupportedExtensions =>
            new[] {@"arw", @"cf2", @"cr2", @"crw", @"dng", @"erf", @"mef", @"mrw", @"nef", @"orf", @"pef", @"raf", @"raw", @"rw2", @"sr2", @"x3f"};

        public Task<Image<Rgba32>> LoadImageAsync(string fileName)
        {
            return Task.FromResult(LoadImageInternal(fileName));
        }

        private static Image<Rgba32> LoadImageInternal(string filename)
        {
            using (Process process = CreateProcess(dcraw: "dcraw", filename))
            {
                if (!process.Start())
                {
                    Debug.WriteLine(string.Format(CultureInfo.InvariantCulture, format: "Executing : {0} {1}", process.StartInfo.FileName, process.StartInfo.Arguments));

                    return null;
                }

                using (Stream stream = process.StandardOutput.BaseStream)
                {
                    Image<Rgba32> image = null;

                    try
                    {
                        image = ConverterCommon.OpenBitmapFromTiffStream(stream);

                        process.WaitForExit();

                        //  Image is upside down otherwise
                        image.Mutate(operation: ctx => ctx.Rotate(degrees: 180));

                        return image;
                    }
                    catch (Exception)
                    {
                        image?.Dispose();

                        throw;
                    }
                }
            }
        }

        private static Process CreateProcess(string dcraw, string fileName)
        {
            Contract.Requires(!string.IsNullOrEmpty(dcraw));
            Contract.Requires(!string.IsNullOrEmpty(fileName));

            //string.Format(CultureInfo.InvariantCulture, "-6 -w -q 3 -c -T \"{0}\"", fileName),
            return new Process
                   {
                       StartInfo =
                       {
                           FileName = dcraw,
                           Arguments = string.Format(CultureInfo.InvariantCulture, format: "-6 -w -q 3 -c -T \"{0}\"", fileName),
                           UseShellExecute = false,
                           CreateNoWindow = true,
                           RedirectStandardOutput = true,
                           RedirectStandardError = true
                       }
                   };
        }
    }

    internal static class ConverterCommon
    {
        /// <summary>
        ///     Opens the bitmap from stream.
        /// </summary>
        /// <param name="stream">
        ///     The stream.
        /// </param>
        /// <returns>
        ///     The image that was contained in the stream.
        /// </returns>
        [SuppressMessage(category: "Microsoft.Usage", checkId: "CA1801:ReviewUnusedParameters", MessageId = "fileName", Justification = "Used for logging")]
        public static Image<Rgba32> OpenBitmapFromTiffStream(Stream stream)
        {
            Contract.Requires(stream != null);

            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                ms.Seek(offset: 0, SeekOrigin.Begin);

                using (Tiff tif = Tiff.ClientOpen(name: "in-memory", mode: "r", ms, new TiffStream()))
                {
                    if (tif == null)
                    {
                        return null;
                    }

                    // Find the width and height of the image
                    FieldValue[] value = tif.GetField(TiffTag.IMAGEWIDTH);
                    int width = value[0]
                        .ToInt();

                    value = tif.GetField(TiffTag.IMAGELENGTH);
                    int height = value[0]
                        .ToInt();

                    // Read the image into the memory buffer
                    int[] raster = new int[height * width];

                    if (!tif.ReadRGBAImage(width, height, raster))
                    {
                        return null;
                    }

                    bool ok = false;
                    Image<Rgba32> bmp = null;

                    try
                    {
                        bmp = new Image<Rgba32>(width, height);

                        for (int y = 0; y < bmp.Height; y++)
                        {
                            int rasterOffset = y * bmp.Width;

                            for (int x = 0; x < bmp.Width; x++)
                            {
                                int rgba = raster[rasterOffset++];
                                byte r = (byte) ((rgba >> 16) & 0xff);
                                byte g = (byte) ((rgba >> 8) & 0xff);
                                byte b = (byte) (rgba & 0xff);
                                byte a = (byte) ((rgba >> 24) & 0xff);

                                bmp[x, y] = new Rgba32(r, g, b, a);
                            }
                        }

                        ok = true;

                        return bmp;
                    }
                    finally
                    {
                        if (!ok)
                        {
                            bmp?.Dispose();
                        }
                    }
                }
            }
        }
    }
}