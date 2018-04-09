using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp;

namespace ImageLoader.Interfaces
{
    public interface IImageConverter
    {
        string[] SupportedExtensions { get; }
        
        Task<Image<Rgba32>> LoadImageAsync(string fileName);

        Task<Image<Rgba32>> LoadImageAsync(Stream source);
    }
}