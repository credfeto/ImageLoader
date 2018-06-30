using System;
using System.IO;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageLoader.Photoshop
{
    public class PhotoshopImageConverter : IImageConverter
    {
        public string[] SupportedExtensions => new[] {@".psd"};

        public Task<Image<Rgba32>> LoadImageAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<Image<Rgba32>> LoadImageAsync(Stream source)
        {
            throw new NotImplementedException();
        }
    }
}