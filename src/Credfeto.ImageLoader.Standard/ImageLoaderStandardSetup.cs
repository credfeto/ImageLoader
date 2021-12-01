using System;
using System.Diagnostics.CodeAnalysis;
using Credfeto.ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Credfeto.ImageLoader.Standard;

[ExcludeFromCodeCoverage]
public static class ImageLoaderStandardSetup
{
    public static void Configure(IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddSingleton<IImageConverter, StandardImageConverter>();
    }
}