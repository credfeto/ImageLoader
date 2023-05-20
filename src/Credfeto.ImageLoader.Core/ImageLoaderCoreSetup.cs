using System.Diagnostics.CodeAnalysis;
using Credfeto.ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Credfeto.ImageLoader.Core;

[ExcludeFromCodeCoverage]
public static class ImageLoaderCoreSetup
{
    public static IServiceCollection AddImageLoaderCore(this IServiceCollection services)
    {
        return services.AddSingleton<IImageLoader, ImageLoaderFactory>();
    }
}