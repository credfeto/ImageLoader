using System;
using System.Threading.Tasks;
using Credfeto.ImageLoader.Interfaces;
using FunFair.Test.Common;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace Credfeto.ImageLoader.Standard.UnitTests
{
    public sealed class JpegTests : TestBase
    {
        private readonly IImageConverter _converter;

        public JpegTests()
        {
            IServiceCollection services = new ServiceCollection();

            ImageLoaderStandardSetup.Configure(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            this._converter = serviceProvider.GetRequiredService<IImageConverter>();
            Assert.NotNull(this._converter);
        }

        [Fact]
        public void JpegExtensionSupported()
        {
            Assert.Contains(collection: this._converter.SupportedExtensions, filter: x => x == @"jpeg");
        }

        [Fact]
        public void JpgExtensionSupported()
        {
            Assert.Contains(collection: this._converter.SupportedExtensions, filter: x => x == @"jpg");
        }

        [Fact]
        public async Task LoadJpgAsync()
        {
            Image<Rgba32> image = await this._converter.LoadImageAsync(fileName: @"test.jpg");

            Assert.NotNull(image);
        }
    }
}