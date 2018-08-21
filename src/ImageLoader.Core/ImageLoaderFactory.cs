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
            if (converters == null) throw new ArgumentNullException(nameof(converters));

            var supportedConverters = new Dictionary<string, IImageConverter>(StringComparer.OrdinalIgnoreCase);

            foreach (var converter in converters)
            foreach (var extension in converter.SupportedExtensions)
                supportedConverters.Add(extension, converter);

            if (!supportedConverters.Any())
                throw new ArgumentOutOfRangeException(nameof(converters), converters, "No Converters Loaded");

            _converters = supportedConverters;
        }

        public Task<Image<Rgba32>> LoadImageAsync(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            var converter = FindConverter(extension);

            return converter.LoadImageAsync(fileName);
        }


        private IImageConverter FindConverter(string extension)
        {
            if (!_converters.TryGetValue(extension, out var converter) || converter == null)
                throw new ArgumentOutOfRangeException(nameof(extension), extension,
                    "No Converter available for extension");

            return converter;
        }
    }
}