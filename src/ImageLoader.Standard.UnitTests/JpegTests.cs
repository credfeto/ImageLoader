using System;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace ImageLoader.Standard.UnitTests
{
    public class JpegTests
    {
        public JpegTests()
        {
            IServiceCollection services = new ServiceCollection();

            ImageLoaderStandardSetup.Configure(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            this._converter = serviceProvider.GetService<IImageConverter>();
            Assert.NotNull(this._converter);
        }

        private readonly IImageConverter _converter;

        [Fact]
        public void JpegExtensionSupported()
        {
            Assert.Contains(this._converter.SupportedExtensions, filter: x => x == @"jpeg");
        }

        [Fact]
        public void JpgExtensionSupported()
        {
            Assert.Contains(this._converter.SupportedExtensions, filter: x => x == @"jpg");
        }

        [Fact]
        public async Task LoadJpgAsync()
        {
            Image<Rgba32> image = await this._converter.LoadImageAsync(fileName: @"test.jpg");

            Assert.NotNull(image);
        }
    }
}