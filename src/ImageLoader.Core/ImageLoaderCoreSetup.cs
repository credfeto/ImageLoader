using System;
using System.Diagnostics.CodeAnalysis;
using ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ImageLoader.Core
{
    [ExcludeFromCodeCoverage]
    public static class ImageLoaderCoreSetup
    {
        public static void Configure(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // register the retry logger (used where Polly.NET is managing transient errors)
            services.AddSingleton<IImageLoader, ImageLoaderFactory>();
        }
    }
}