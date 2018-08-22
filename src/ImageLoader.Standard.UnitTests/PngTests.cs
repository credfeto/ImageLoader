using System;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ImageLoader.Standard.UnitTests
{
    public class PngTests
    {
        public PngTests()
        {
            IServiceCollection services = new ServiceCollection();

            ImageLoaderStandardSetup.Configure(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            converter = serviceProvider.GetService<IImageConverter>();
            Assert.NotNull(converter);
        }

        public readonly IImageConverter converter;

        [Fact]
        public async Task LoadPng()
        {
            var image = await converter.LoadImageAsync(@"test.png").ConfigureAwait(false);
            
            Assert.NotNull(image);
        }

        [Fact]
        public void PngExtensionSupported()
        {
            Assert.Contains(converter.SupportedExtensions, x => x == @"jpg");
        }
    }
}