using System;
using System.IO;
using System.Threading.Tasks;
using Credfeto.ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;
using Xunit.Sdk;

namespace Credfeto.ImageLoader.Raw.UnitTests
{
    public sealed class CanonRawTests
    {
        private readonly IImageConverter _converter;

        public CanonRawTests()
        {
            IServiceCollection services = new ServiceCollection();

            ImageLoaderRawSetup.Configure(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            this._converter = serviceProvider.GetRequiredService<IImageConverter>();
            Assert.NotNull(this._converter);
        }

        private static string FindTestFile(string fileName)
        {
            string? location = Path.GetDirectoryName(typeof(CanonRawTests).Assembly.Location);

            if (location == null)
            {
                throw new NullException($"Could not fine {fileName}");
            }

            string file = Path.Combine(path1: location, path2: fileName);

            Assert.True(File.Exists(file), $"Could not fine {fileName} at {file}");

            return file;
        }

        [Fact]
        public void Cr2ExtensionSupported()
        {
            Assert.Contains(collection: this._converter.SupportedExtensions, filter: x => x == @"cr2");
        }

        [Fact]
        public async Task LoadCr2Async()
        {
            string fileName = FindTestFile(fileName: @"test.CR2");

            Image<Rgba32> image = await this._converter.LoadImageAsync(fileName);

            Assert.NotNull(image);
            Assert.Equal(expected: 3870, actual: image.Width);
            Assert.Equal(expected: 5796, actual: image.Height);
            Assert.Equal(expected: 32, actual: image.PixelType.BitsPerPixel);
        }

        [Fact]
        public async Task LoadRw2Async()
        {
            string fileName = FindTestFile(fileName: @"test.RW2");

            Image<Rgba32> image = await this._converter.LoadImageAsync(fileName);

            Assert.NotNull(image);
            Assert.Equal(expected: 4608, actual: image.Width);
            Assert.Equal(expected: 3464, actual: image.Height);
            Assert.Equal(expected: 32, actual: image.PixelType.BitsPerPixel);
        }

        [Fact]
        public void Rw2ExtensionSupported()
        {
            Assert.Contains(collection: this._converter.SupportedExtensions, filter: x => x == @"rw2");
        }
    }
}