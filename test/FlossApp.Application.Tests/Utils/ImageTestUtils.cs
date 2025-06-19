using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Application.Tests.Utils;

internal static class ImageTestUtils
{
    public static Image<Rgba32> GetRandomNoise(int width = 255, int height = 255)
    {
        var image = new Image<Rgba32>(width, height, new Rgba32());

        var random = new Random();
        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < accessor.Height; y++)
            {
                var rowSpan = accessor.GetRowSpan(y);
                for (int x = 0; x < rowSpan.Length; x++)
                {
                    byte[] buf = new byte[3];
                    random.NextBytes(buf);
                    rowSpan[x] = Color.FromRgb(buf[0], buf[1], buf[2]);
                }
            }
        });

        return image;
    }
}
