﻿using System;
using System.Diagnostics.CodeAnalysis;
using Credfeto.ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Credfeto.ImageLoader.Raw
{
    [ExcludeFromCodeCoverage]
    public static class ImageLoaderRawSetup
    {
        public static void Configure(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IImageConverter, RawImageConverter>();
        }
    }
}