using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using FlossApp.Application.Enums;
using FlossApp.Application.Extensions.System.Drawing;
using FlossApp.Application.Services.ColorProvider;
using MethodTimer;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp.Processing.Processors.Quantization;
using FlossApp.Application.Extensions.FlossApp.Application.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace FlossApp.Application.Services.ImageFiltering;

internal class ImageFilteringService : IImageFilteringService
{
    private readonly IColorProviderService _colorProviderService;

    public ImageFilteringService(IServiceProvider services)
    {
        _colorProviderService = services.GetRequiredService<IColorProviderService>();
    }

    [Time]
    public Image<Rgba32> PixelateImage(Image<Rgba32> input, float scale, ImageSharpKnownResamplers resampler = ImageSharpKnownResamplers.NearestNeighbor)
    {
        if (scale is <= 0 or > 1)
        {
            return input;
        }

        int newWidth = Math.Max(1, (int)(input.Width * scale));
        int newHeight = Math.Max(1, (int)(input.Height * scale));

        var resized = input.Clone(ctx => ctx.Resize(new ResizeOptions()
        {
            Size = new Size(newWidth, newHeight),
            Mode = ResizeMode.Stretch,
            Sampler = resampler.AsKnownResampler()
        }));

        return resized;
    }

    [Time]
    public async Task<Image<Rgba32>> ReduceToSchemaColorsAsync(Image<Rgba32> image, ColorSchema schema)
    {
        if (schema.IsRgbSuperset())
        {
            return image;
        }

        int n = image.Width * image.Height;

        Dictionary<System.Drawing.Color, Color> cache = [];

        var set = (await _colorProviderService.GetColorsAsync(schema)).ToList();
        var newImage = image.Clone();

        newImage.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < accessor.Height; y++)
            {
                var rowSpan = accessor.GetRowSpan(y);
                for (int x = 0; x < rowSpan.Length; x++)
                {
                    var color = System.Drawing.Color.FromArgb(rowSpan[x].A, rowSpan[x].R, rowSpan[x].G, rowSpan[x].B);
                    if (!cache.TryGetValue(color, out Color newColor))
                    {
                        var similarColor = color.GetMostSimilarColors(set, 1).FirstOrDefault();
                        newColor = Color.FromRgba(similarColor.R, similarColor.G, similarColor.B, similarColor.A);
                        cache.Add(color, newColor);
                    }

                    rowSpan[x] = newColor;
                }
            }
        });

        return newImage;
    }

    [Time]
    public Image<Rgba32> ReduceColors(Image<Rgba32> input, int maxColors, ImageSharpKnownDitherings? dither = null)
    {
        IQuantizer quantizer = new WuQuantizer(new QuantizerOptions { MaxColors = maxColors, Dither = dither.AsKnownDithering() });
        var ret = input.Clone();
        ret.Mutate(c => c.Quantize(quantizer));
        return ret;
    }

    public Image<Rgba32> Upscale(Image<Rgba32> input, int scale)
    {
        var newImage = input.Clone();
        newImage.Mutate(x => x.Resize(input.Width * scale, input.Height * scale));
        return newImage;
    }
}
