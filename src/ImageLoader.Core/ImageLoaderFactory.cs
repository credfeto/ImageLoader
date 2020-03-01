using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageLoader.Core
{
    internal sealed class ImageLoaderFactory : IImageLoader
    {
        private readonly IReadOnlyDictionary<string, IImageConverter> _converters;

        public ImageLoaderFactory(IEnumerable<IImageConverter> converters)
        {
            if (converters == null)
            {
                throw new ArgumentNullException(nameof(converters));
            }

            Dictionary<string, IImageConverter> supportedConverters = new Dictionary<string, IImageConverter>(StringComparer.OrdinalIgnoreCase);

            foreach (IImageConverter converter in converters)
            foreach (string extension in converter.SupportedExtensions)
            {
                supportedConverters.Add(extension, converter);
            }

            if (!supportedConverters.Any())
            {
                throw new ArgumentOutOfRangeException(nameof(converters), converters, message: "No Converters Loaded");
            }

            this._converters = supportedConverters;
            this.SupportedExtensions = this._converters.Keys.ToArray();
        }

        public Task<Image<Rgba32>> LoadImageAsync(string fileName)
        {
            string extension = Path.GetExtension(fileName);

            IImageConverter converter = this.FindConverter(extension);

            return converter.LoadImageAsync(fileName);
        }

        public IReadOnlyCollection<string> SupportedExtensions { get; }

        private IImageConverter FindConverter(string extension)
        {
            if (!this._converters.TryGetValue(extension, out IImageConverter converter) || converter == null)
            {
                throw new ArgumentOutOfRangeException(nameof(extension), extension, message: "No Converter available for extension");
            }

            return converter;
        }
    }
}