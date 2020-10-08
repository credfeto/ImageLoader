using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Credfeto.ImageLoader.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Credfeto.ImageLoader.Standard
{
    public sealed class StandardImageConverter : IImageConverter
    {
        public IReadOnlyList<string> SupportedExtensions => new[] {@"jpg", @"jpeg", @"bmp", @"png"};

        public async Task<Image<Rgba32>> LoadImageAsync(string fileName)
        {
            byte[] content = await File.ReadAllBytesAsync(fileName);

            using (MemoryStream ms = new MemoryStream(buffer: content, writable: false))
            {
                return (await Image.LoadAsync(ms)).CloneAs<Rgba32>();
            }
        }
    }
}