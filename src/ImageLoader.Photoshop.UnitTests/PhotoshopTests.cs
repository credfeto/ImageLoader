using System;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ImageLoader.Photoshop.UnitTests
{
    public class PhotoshopTests
    {
        public PhotoshopTests()
        {
            IServiceCollection services = new ServiceCollection();

            ImageLoaderPhotoshopSetup.Configure(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            _converter = serviceProvider.GetService<IImageConverter>();
            Assert.NotNull(_converter);
        }

        private readonly IImageConverter _converter;

        [Fact]
        public async Task LoadPsd()
        {
            var image = await _converter.LoadImageAsync(@"test.psd");

            Assert.NotNull(image);
        }

        [Fact]
        public void PsdExtensionSupported()
        {
            Assert.Contains(_converter.SupportedExtensions, x => x == @"psd");
        }
    }
}