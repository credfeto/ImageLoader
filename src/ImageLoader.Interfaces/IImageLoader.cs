using System.Collections.Generic;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageLoader.Interfaces
{
    public interface IImageLoader
    {
        IReadOnlyCollection<string> SupportedExtensions { get; }

        Task<Image<Rgba32>> LoadImageAsync(string fileName);

        bool CanLoad(string fileName);
    }
}