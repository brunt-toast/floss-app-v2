using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Models.RichColor;
using FlossApp.Application.Services.ColorProvider;
using FlossApp.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using static System.Net.Mime.MediaTypeNames;

namespace FlossApp.Application.Services.ImageAnalysis;

public class ImageAnalysisService : IImageAnalysisService
{
    private readonly IColorProviderService _colorProviderService;

    public ImageAnalysisService(IColorProviderService colorProviderService)
    {
        _colorProviderService = colorProviderService;
    }

    public async Task<Dictionary<RichColorModel, int>> GetPaletteAsync(Image<Rgba32> image, ColorSchema schema)
    {
        Dictionary<RichColorModel, int> ret = [];

        var set = (await _colorProviderService.GetRichColorsAsync(schema)).ToList();

        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < accessor.Height; y++)
            {
                var rowSpan = accessor.GetRowSpan(y);
                for (int x = 0; x < rowSpan.Length; x++)
                {
                    var color = System.Drawing.Color.FromArgb(rowSpan[x].A, rowSpan[x].R, rowSpan[x].G, rowSpan[x].B);
                    var match = set.FirstOrDefault(c => ColorEquals(color, c));
                    if (!ret.TryAdd(match, 1))
                    {
                        ret[match]++;
                    }
                }
            }
        });

        return ret;
    }

    public IEnumerable<System.Drawing.Color> GetDistinctColors(Image<Rgba32> image)
    {
        HashSet<System.Drawing.Color> ret = [];
        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < accessor.Height; y++)
            {
                var rowSpan = accessor.GetRowSpan(y);
                for (int x = 0; x < rowSpan.Length; x++)
                {
                    var color = System.Drawing.Color.FromArgb(rowSpan[x].A, rowSpan[x].R, rowSpan[x].G, rowSpan[x].B);
                    ret.Add(color);
                }
            }
        });
        return ret;
    }

    private static bool ColorEquals(System.Drawing.Color left, RichColorModel right)
    {
        return left.R == right.Red
               && left.G == right.Green
               && left.B == right.Blue;
    }
}
