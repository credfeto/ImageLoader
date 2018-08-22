using System;
using ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ImageLoader.Photoshop.UnitTests
{
    public class PhotoshopTests
    {
        [Fact]
        public void PsdExtensionSupported()
        {
            IServiceCollection services = new ServiceCollection();

            ImageLoaderPhotoshopSetup.Configure(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var converter = serviceProvider.GetService<IImageConverter>();
            Assert.NotNull(converter);

            Assert.Contains(converter.SupportedExtensions, x => x == @"psd");
        }
    }
}