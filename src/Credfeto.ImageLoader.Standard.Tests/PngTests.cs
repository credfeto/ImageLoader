using System;
using System.Threading.Tasks;
using Credfeto.ImageLoader.Interfaces;
using FunFair.Test.Common;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace Credfeto.ImageLoader.Standard.Tests;

public sealed class PngTests : TestBase
{
    private readonly IImageConverter _converter;

    public PngTests()
    {
        IServiceCollection services = new ServiceCollection();

        ImageLoaderStandardSetup.Configure(services);

        IServiceProvider serviceProvider = services.BuildServiceProvider();

        this._converter = serviceProvider.GetRequiredService<IImageConverter>();
        Assert.NotNull(this._converter);
    }

    [Fact]
    public async Task LoadPngAsync()
    {
        Image<Rgba32> image = await this._converter.LoadImageAsync(fileName: @"test.png");

        Assert.NotNull(image);
    }

    [Fact]
    public void PngExtensionSupported()
    {
        Assert.Contains(collection: this._converter.SupportedExtensions, filter: x => x == @"jpg");
    }
}