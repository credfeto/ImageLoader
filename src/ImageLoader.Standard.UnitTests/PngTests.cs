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

            _converter = serviceProvider.GetService<IImageConverter>();
            Assert.NotNull(_converter);
        }

        private readonly IImageConverter _converter;

        [Fact]
        public async Task LoadPng()
        {
            var image = await _converter.LoadImageAsync(@"test.png").ConfigureAwait(false);
            
            Assert.NotNull(image);
        }

        [Fact]
        public void PngExtensionSupported()
        {
            Assert.Contains(_converter.SupportedExtensions, x => x == @"jpg");
        }
    }
}