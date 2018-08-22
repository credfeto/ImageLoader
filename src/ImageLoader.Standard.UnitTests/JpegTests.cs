using System;
using ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ImageLoader.Standard.UnitTests
{
    public class JpegTests
    {
        [Fact]
        public void JpgExtensionSupported()
        {
            IServiceCollection services = new ServiceCollection();

            ImageLoaderStandardSetup.Configure(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var converter = serviceProvider.GetService<IImageConverter>();
            Assert.NotNull(converter);

            Assert.Contains(converter.SupportedExtensions, x => x == @"jpg");
        }
        
        [Fact]
        public void JpegExtensionSupported()
        {
            IServiceCollection services = new ServiceCollection();

            ImageLoaderStandardSetup.Configure(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var converter = serviceProvider.GetService<IImageConverter>();
            Assert.NotNull(converter);

            Assert.Contains(converter.SupportedExtensions, x => x == @"jpeg");
        }
    }
}