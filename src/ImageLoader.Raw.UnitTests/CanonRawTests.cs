using System;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace ImageLoader.Raw.UnitTests
{
    public class CanonRawTests
    {
        public CanonRawTests()
        {
            IServiceCollection services = new ServiceCollection();

            ImageLoaderRawSetup.Configure(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            this._converter = serviceProvider.GetService<IImageConverter>();
            Assert.NotNull(this._converter);
        }

        private readonly IImageConverter _converter;

        [Fact]
        public void Cr2ExtensionSupported()
        {
            Assert.Contains(this._converter.SupportedExtensions, filter: x => x == @"cr2");
        }

        [Fact]
        public async Task LoadCr2Async()
        {
            Image<Rgba32> image = await this._converter.LoadImageAsync(fileName: @"test.cr2");

            Assert.NotNull(image);
        }
    }
}