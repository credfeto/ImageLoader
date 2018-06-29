using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageLoader.Raw
{
    public class RawImageConverter : IImageConverter
    {
        public string[] SupportedExtensions => new[]
        {
            @"arw",
            @"cf2",
            @"cr2",
            @"crw",
            @"dng",
            @"erf",
            @"mef",
            @"mrw",
            @"nef",
            @"orf",
            @"pef",
            @"raf",
            @"raw",
            @"rw2",
            @"sr2",
            @"x3f"
        };

        public Task<Image<Rgba32>> LoadImageAsync(string fileName)
        {
            return Task.FromResult(LoadImageInternal(fileName));
        }

        public Task<Image<Rgba32>> LoadImageAsync(Stream source)
        {
            throw new NotSupportedException();
        }

        private static Image<Rgba32> LoadFromStream(Stream source)
        {
            Image<Rgba32> image = null;

            try
            {
                image = Image.Load(source);

                return image;
            }
            catch
            {
                image?.Dispose();

                throw;
            }
        }

        private static Image<Rgba32> LoadImageInternal(string filename)
        {
            using (var process = CreateProcess("dcraw", filename))
            {
                if (!process.Start())
                {
                    Debug.WriteLine(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "Executing : {0} {1}",
                            process.StartInfo.FileName,
                            process.StartInfo.Arguments));
                    return null;
                }

                using (var stream = process.StandardOutput.BaseStream)
                {
                    Image<Rgba32> image = null;

                    try
                    {
                        image = LoadFromStream(stream);

                        process.WaitForExit();

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
                    Arguments =
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "-6 -w -q 3 -c -T \"{0}\"",
                            fileName),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
        }
    }
}