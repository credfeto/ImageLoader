using System.Collections.Generic;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Credfeto.ImageLoader.Interfaces
{
    public interface IImageConverter
    {
        IReadOnlyList<string> SupportedExtensions { get; }

        Task<Image<Rgba32>> LoadImageAsync(string fileName);
    }
}