using System.Diagnostics.CodeAnalysis;
using Credfeto.ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Credfeto.ImageLoader.Standard;

[ExcludeFromCodeCoverage]
public static class ImageLoaderStandardSetup
{
    public static IServiceCollection AddImageLoaderStandard(this IServiceCollection services)
    {
        return services.AddSingleton<IImageConverter, StandardImageConverter>();
    }
}