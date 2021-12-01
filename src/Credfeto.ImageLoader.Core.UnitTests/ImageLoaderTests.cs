using System;
using Credfeto.ImageLoader.Interfaces;
using FunFair.Test.Common;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Credfeto.ImageLoader.Core.UnitTests
{
    public sealed class ImageLoaderTests : TestBase
    {
        [Fact]
        public void DoesNotThrowIfMultipleConvertersRegistered()
        {
            IServiceCollection services = new ServiceCollection();

            ImageLoaderCoreSetup.Configure(services);

            IImageConverter ic1 = Substitute.For<IImageConverter>();
            ic1.SupportedExtensions.Returns(new[] { ".jpg", ".jpeg" });

            services = services.AddSingleton(ic1);

            IImageConverter ic2 = Substitute.For<IImageConverter>();
            ic2.SupportedExtensions.Returns(new[] { ".png" });

            services = services.AddSingleton(ic2);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            IImageLoader il = serviceProvider.GetRequiredService<IImageLoader>();
            Assert.NotNull(il);
        }

        [Fact]
        public void ThrowsExceptionWhenNoConvertersRegistered()
        {
            IServiceCollection services = new ServiceCollection();

            ImageLoaderCoreSetup.Configure(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            Assert.Throws<ArgumentOutOfRangeException>(testCode: () => serviceProvider.GetService<IImageLoader>());
        }
    }
}