using System.Collections.Generic;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageLoader.Interfaces
{
    public interface IImageLoader
    {
        Task<Image<Rgba32>> LoadImageAsync(string fileName);
        
        IReadOnlyCollection<string> SupportedExtensions { get; }
    }
}