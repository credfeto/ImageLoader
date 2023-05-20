using System.Diagnostics.CodeAnalysis;
using Credfeto.ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Credfeto.ImageLoader.Photoshop;

[ExcludeFromCodeCoverage]
public static class ImageLoaderPhotoshopSetup
{
    public static IServiceCollection AddImageLoaderPhotoshop(this IServiceCollection services)
    {
        return services.AddSingleton<IImageConverter, PhotoshopImageConverter>();
    }
}