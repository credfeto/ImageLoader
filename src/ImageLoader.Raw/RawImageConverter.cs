using System.IO;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageLoader.Raw
{
    public class RawImageConverter : IImageConverter
    {
        public string[] SupportedExtensions => new[] {@".cr2", @".rw2", @".mrw", @".dng"};

        public async Task<Image<Rgba32>> LoadImageAsync(string fileName)
        {
            var content = await File.ReadAllBytesAsync(fileName);

            using (var ms = new MemoryStream(content, false))
            {
                return LoadFromStream(ms);
            }
        }

        public Task<Image<Rgba32>> LoadImageAsync(Stream source)
        {
            return Task.FromResult(LoadFromStream(source));
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
                if (image != null)
                    image.Dispose();

                throw;
            }
        }
    }
}