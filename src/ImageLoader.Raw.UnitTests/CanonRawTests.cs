using System;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
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

            _converter = serviceProvider.GetService<IImageConverter>();
            Assert.NotNull(_converter);
        }

        private readonly IImageConverter _converter;


        [Fact]
        public void Cr2ExtensionSupported()
        {
            Assert.Contains(_converter.SupportedExtensions, x => x == @"cr2");
        }
        
        [Fact]
        public async Task LoadCr2()
        {
            var image = await _converter.LoadImageAsync(@"test.cr2").ConfigureAwait(false);
            
            Assert.NotNull(image);
        }
    }
}