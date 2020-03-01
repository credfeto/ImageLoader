using System;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
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

            this._converter = serviceProvider.GetService<IImageConverter>();
            Assert.NotNull(this._converter);
        }

        private readonly IImageConverter _converter;

        [Fact]
        public async Task LoadPsdAsync()
        {
            Image<Rgba32> image = await this._converter.LoadImageAsync(fileName: @"test.psd");

            Assert.NotNull(image);
        }

        [Fact]
        public void PsdExtensionSupported()
        {
            Assert.Contains(this._converter.SupportedExtensions, filter: x => x == @"psd");
        }
    }
}