using System.Diagnostics.CodeAnalysis;
using Credfeto.ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Credfeto.ImageLoader.Raw;

[ExcludeFromCodeCoverage]
public static class ImageLoaderRawSetup
{
    public static IServiceCollection AddImageLoaderRaw(this IServiceCollection services)
    {
        return services.AddSingleton<IImageConverter, RawImageConverter>();
    }
}