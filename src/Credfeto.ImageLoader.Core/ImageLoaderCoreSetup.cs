using System;
using System.Diagnostics.CodeAnalysis;
using Credfeto.ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Credfeto.ImageLoader.Core;

[ExcludeFromCodeCoverage]
public static class ImageLoaderCoreSetup
{
    public static void Configure(IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddSingleton<IImageLoader, ImageLoaderFactory>();
    }
}