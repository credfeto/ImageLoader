﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ImageLoader.Interfaces;
using Ntreev.Library.Psd;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageLoader.Photoshop
{
    public sealed class PhotoshopImageConverter : IImageConverter
    {
        public IReadOnlyList<string> SupportedExtensions => new[] {@"psd"};

        public async Task<Image<Rgba32>> LoadImageAsync(string fileName)
        {
            byte[] source = await File.ReadAllBytesAsync(fileName);

            using (MemoryStream ms = new MemoryStream(source, writable: false))
            {
                using (PsdDocument psd = PsdDocument.Create(ms))
                {
                    IImageSource imageSource = psd;

                    byte[] data = imageSource.MergeChannels();

                    if (imageSource.Channels.Length == 4)
                    {
                        return Convert(Image.LoadPixelData<Bgra32>(data, imageSource.Width, imageSource.Height));
                    }

                    return Convert(Image.LoadPixelData<Bgr24>(data, imageSource.Width, imageSource.Height));
                }
            }
        }

        private static Image<Rgba32> Convert(Image<Bgra32> source)
        {
            return source.CloneAs<Rgba32>();
        }

        private static Image<Rgba32> Convert(Image<Bgr24> source)
        {
            return source.CloneAs<Rgba32>();
        }
    }
}