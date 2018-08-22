using System;
using ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ImageLoader.Raw.UnitTests
{
    public class CannonRawTests
    {
        [Fact]
        public void Cr2ExtensionSupported()
        {
            IServiceCollection services = new ServiceCollection();

            ImageLoaderRawSetup.Configure(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var converter = serviceProvider.GetService<IImageConverter>();
            Assert.NotNull(converter);

            Assert.Contains(converter.SupportedExtensions, x => x == @"cr2");
        }
    }
}