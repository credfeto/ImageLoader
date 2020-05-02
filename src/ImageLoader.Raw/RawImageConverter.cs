using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using ImageMagick;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageLoader.Raw
{
    public sealed class RawImageConverter : IImageConverter
    {
        public IReadOnlyList<string> SupportedExtensions =>
            new[] {@"arw", @"cf2", @"cr2", @"cr3", @"crw", @"dng", @"erf", @"mef", @"mrw", @"nef", @"orf", @"pef", @"raf", @"raw", @"rw2", @"sr2", @"x3f"};

        public async Task<Image<Rgba32>> LoadImageAsync(string fileName)
        {
            using (MagickImage mi = new MagickImage(fileName))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    mi.Write(stream: ms, format: MagickFormat.Bmp);

                    await ms.FlushAsync();
                    ms.Seek(offset: 0, loc: SeekOrigin.Begin);

                    return Image.Load<Rgba32>(ms);
                }
            }
        }
    }
}