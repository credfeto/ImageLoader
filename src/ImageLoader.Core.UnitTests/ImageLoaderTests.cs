using System;
using ImageLoader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace ImageLoader.Core.UnitTests
{
    public class ImageLoaderTests
    {
        [Fact]
        public void DoesNotThrowIfMultipleConvertersRegistered()
        {
            IServiceCollection services = new ServiceCollection();

            ImageLoaderCoreSetup.Configure(services);

            var ic1 = Substitute.For<IImageConverter>();
            ic1.SupportedExtensions.Returns(new[] {".jpg", ".jpeg"});

            services.AddSingleton(ic1);

            var ic2 = Substitute.For<IImageConverter>();
            ic2.SupportedExtensions.Returns(new[] {".png"});

            services.AddSingleton(ic2);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var il = serviceProvider.GetService<IImageLoader>();
            Assert.NotNull(il);
        }


        [Fact]
        public void ThrowsExceptionWhenNoConvertersRegistered()
        {
            IServiceCollection services = new ServiceCollection();

            ImageLoaderCoreSetup.Configure(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            Assert.Throws<ArgumentOutOfRangeException>(() => serviceProvider.GetService<IImageLoader>());
        }
    }
}