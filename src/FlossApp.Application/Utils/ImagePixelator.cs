using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace FlossApp.Application.Utils;

public static class ImagePixelator
{
    public static Image<Rgba32> Pixelate(Image<Rgba32> input, float scale)
    {
        if (scale is <= 0 or > 1)
        {
            return input;
        }

        int newWidth = Math.Max(1, (int)(input.Width * scale));
        int newHeight = Math.Max(1, (int)(input.Height * scale));

        var resized = input.Clone(ctx => ctx.Resize(newWidth, newHeight));

        return resized;
    }
}
