using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Credfeto.ImageLoader.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Credfeto.ImageLoader.Core;

public sealed class ImageLoaderFactory : IImageLoader
{
    private readonly IReadOnlyDictionary<string, IImageConverter> _converters;

    public ImageLoaderFactory(IEnumerable<IImageConverter> converters)
    {
        if (converters == null)
        {
            throw new ArgumentNullException(nameof(converters));
        }

        this._converters = LoadConverters(converters);
        this.SupportedExtensions = this._converters.Keys.ToArray();
    }

    public Task<Image<Rgba32>> LoadImageAsync(string fileName)
    {
        string extension = GetExtension(fileName);

        IImageConverter converter = this.FindConverter(extension);

        return converter.LoadImageAsync(fileName);
    }

    public bool CanLoad(string fileName)
    {
        return this._converters.TryGetValue(GetExtension(fileName), out IImageConverter? _);
    }

    public IReadOnlyCollection<string> SupportedExtensions { get; }

    private static string GetExtension(string fileName)
    {
        return Path.GetExtension(fileName)
                   .TrimStart(trimChar: '.');
    }

    private static Dictionary<string, IImageConverter> LoadConverters(IEnumerable<IImageConverter> converters)
    {
        Dictionary<string, IImageConverter> supportedConverters = new(StringComparer.OrdinalIgnoreCase);

        foreach (IImageConverter converter in converters)
        {
            foreach (string extension in converter.SupportedExtensions)
            {
                supportedConverters.Add(key: extension, value: converter);
            }
        }

        if (supportedConverters.Count == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(converters), actualValue: converters, message: "No Converters Loaded");
        }

        return supportedConverters;
    }

    private IImageConverter FindConverter(string extension)
    {
        if (!this._converters.TryGetValue(key: extension, out IImageConverter? converter))
        {
            throw new ArgumentOutOfRangeException(nameof(extension), actualValue: extension, message: @"No Converter available for extension");
        }

        return converter;
    }
}