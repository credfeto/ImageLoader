using System;
using System.IO;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;
using Xunit.Sdk;

namespace ImageLoader.Raw.UnitTests
{
    public sealed class CanonRawTests
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

        private static string FindTestFile(string fileName)
        {
            string? location = Path.GetDirectoryName(typeof(CanonRawTests).Assembly.Location);

            if (location == null)
            {
                throw new NullException($"Could not fine {fileName}");
            }

            string file = Path.Combine(location, fileName);

            Assert.True(File.Exists(file), $"Could not fine {fileName} at {file}");

            return file;
        }

        [Fact]
        public void Cr2ExtensionSupported()
        {
            Assert.Contains(this._converter.SupportedExtensions, filter: x => x == @"cr2");
        }

        [Fact]
        public async Task LoadCr2Async()
        {
            string fileName = FindTestFile(fileName: @"test.CR2");

            Image<Rgba32> image = await this._converter.LoadImageAsync(fileName);

            Assert.NotNull(image);
            Assert.Equal(expected: 3870, image.Width);
            Assert.Equal(expected: 5796, image.Height);
            Assert.Equal(expected: 32, image.PixelType.BitsPerPixel);
        }

        [Fact]
        public async Task LoadRw2Async()
        {
            string fileName = FindTestFile(fileName: @"test.RW2");

            Image<Rgba32> image = await this._converter.LoadImageAsync(fileName);

            Assert.NotNull(image);
            Assert.Equal(expected: 4608, image.Width);
            Assert.Equal(expected: 3464, image.Height);
            Assert.Equal(expected: 32, image.PixelType.BitsPerPixel);
        }

        [Fact]
        public void Rw2ExtensionSupported()
        {
            Assert.Contains(this._converter.SupportedExtensions, filter: x => x == @"rw2");
        }
    }
}