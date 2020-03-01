using System;
using System.Diagnostics.CodeAnalysis;
using ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ImageLoader.Photoshop
{
    [ExcludeFromCodeCoverage]
    public static class ImageLoaderPhotoshopSetup
    {
        public static void Configure(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IImageConverter, PhotoshopImageConverter>();
        }
    }
}