using System.Collections.Generic;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using Ntreev.Library.Psd;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageLoader.Photoshop
{
    public sealed class PhotoshopImageConverter : IImageConverter
    {
        public IReadOnlyList<string> SupportedExtensions => new[] {@"psd"};

        public Task<Image<Rgba32>> LoadImageAsync(string fileName)
        {
            using (PsdDocument imageSource = PsdDocument.Create(fileName))
            {
                if (!imageSource.HasImage)
                {
                    throw new ImageProcessingException(errorMessage: "PSD has no image");
                }

                byte[] data = imageSource.MergeChannels();

                return Task.FromResult(Image.Load(data));
            }
        }
    }
}