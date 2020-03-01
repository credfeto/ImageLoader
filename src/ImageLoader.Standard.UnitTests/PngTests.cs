using System;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
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

            this._converter = serviceProvider.GetService<IImageConverter>();
            Assert.NotNull(this._converter);
        }

        private readonly IImageConverter _converter;

        [Fact]
        public async Task LoadPngAsync()
        {
            Image<Rgba32> image = await this._converter.LoadImageAsync(fileName: @"test.png");

            Assert.NotNull(image);
        }

        [Fact]
        public void PngExtensionSupported()
        {
            Assert.Contains(this._converter.SupportedExtensions, filter: x => x == @"jpg");
        }
    }
}