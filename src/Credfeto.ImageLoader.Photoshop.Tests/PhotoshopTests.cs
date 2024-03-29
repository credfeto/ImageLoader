using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Credfeto.ImageLoader.Interfaces;
using FunFair.Test.Common;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace Credfeto.ImageLoader.Photoshop.Tests;

public sealed class PhotoshopTests : TestBase
{
    private readonly IImageConverter _converter;

    public PhotoshopTests()
    {
        IServiceProvider serviceProvider = new ServiceCollection().AddImageLoaderPhotoshop()
                                                                  .BuildServiceProvider();

        this._converter = serviceProvider.GetRequiredService<IImageConverter>();
        Assert.NotNull(this._converter);
    }

    [Fact]
    public async Task LoadPsdAsync()
    {
        Image<Rgba32> image = await this._converter.LoadImageAsync(fileName: @"test.psd");

        Assert.NotNull(image);

        await using (FileStream fs = File.Create(path: "test.jpg"))
        {
            await image.SaveAsJpegAsync(stream: fs, cancellationToken: CancellationToken.None);
        }
    }

    [Fact]
    public void PsdExtensionSupported()
    {
        Assert.Contains(collection: this._converter.SupportedExtensions, filter: x => x == @"psd");
    }
}