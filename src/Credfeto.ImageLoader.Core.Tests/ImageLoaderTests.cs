using System;
using Credfeto.ImageLoader.Interfaces;
using FunFair.Test.Common;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Credfeto.ImageLoader.Core.Tests;

public sealed class ImageLoaderTests : TestBase
{
    [Fact]
    public void DoesNotThrowIfMultipleConvertersRegistered()
    {
        string[] converters =
        {
            ".jpg",
            ".jpeg"
        };

        string[] expected =
        {
            ".png"
        };

        IServiceCollection services = new ServiceCollection();

        services = services.AddImageLoaderCore();

        IImageConverter ic1 = Substitute.For<IImageConverter>();
        ic1.SupportedExtensions.Returns(converters);

        services = services.AddSingleton(ic1);

        IImageConverter ic2 = Substitute.For<IImageConverter>();
        ic2.SupportedExtensions.Returns(expected);

        services = services.AddSingleton(ic2);

        IServiceProvider serviceProvider = services.BuildServiceProvider();

        IImageLoader il = serviceProvider.GetRequiredService<IImageLoader>();
        Assert.NotNull(il);
    }

    [Fact]
    public void ThrowsExceptionWhenNoConvertersRegistered()
    {
        IServiceProvider serviceProvider = BuildServiceProvider();

        Assert.Throws<ArgumentOutOfRangeException>(testCode: () => serviceProvider.GetService<IImageLoader>());
    }

    private static IServiceProvider BuildServiceProvider()
    {
        IServiceProvider serviceProvider = new ServiceCollection().AddImageLoaderCore()
                                                                  .BuildServiceProvider();

        return serviceProvider;
    }
}