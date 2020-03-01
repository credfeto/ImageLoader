using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageLoader.Standard
{
    public class StandardImageConverter : IImageConverter
    {
        public IReadOnlyList<string> SupportedExtensions => new[] {@"jpg", @"jpeg", @"bmp", @"png"};

        public async Task<Image<Rgba32>> LoadImageAsync(string fileName)
        {
            byte[] content = await File.ReadAllBytesAsync(fileName);

            using (MemoryStream ms = new MemoryStream(content, writable: false))
            {
                return LoadFromStream(ms);
            }
        }

        private static Image<Rgba32> LoadFromStream(Stream source)
        {
            Image<Rgba32>? image = null;

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
    }
}