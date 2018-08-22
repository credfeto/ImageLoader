using System;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
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

            _converter = serviceProvider.GetService<IImageConverter>();
            Assert.NotNull(_converter);
        }

        private readonly IImageConverter _converter;

        [Fact]
        public void JpegExtensionSupported()
        {
            Assert.Contains(_converter.SupportedExtensions, x => x == @"jpeg");
        }


        [Fact]
        public void JpgExtensionSupported()
        {
            Assert.Contains(_converter.SupportedExtensions, x => x == @"jpg");
        }
        
        [Fact]
        public async Task LoadJpg()
        {
            var image = await _converter.LoadImageAsync(@"test.jpg").ConfigureAwait(false);
            
            Assert.NotNull(image);
        }
    }
}