using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Application.Mock;

public static class ImageMockup
{
    public static Image<Rgba32> GetRandomNoise(int width = 255, int height = 255, int uniqueColors = 100)
    {
        var image = new Image<Rgba32>(width, height, new Rgba32());

        List<Color> randColorPool = [];
        var random = new Random();
        for (int i = 0; i < uniqueColors; i++)
        {
            byte[] buf = new byte[3];
            random.NextBytes(buf);
            var color = Color.FromRgb(buf[0], buf[1], buf[2]);
            randColorPool.Add(color);
        }

        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < accessor.Height; y++)
            {
                var rowSpan = accessor.GetRowSpan(y);
                for (int x = 0; x < rowSpan.Length; x++)
                {
                    rowSpan[x] = randColorPool[random.Next(0, randColorPool.Count - 1)];
                }
            }
        });

        return image;
    }
}
