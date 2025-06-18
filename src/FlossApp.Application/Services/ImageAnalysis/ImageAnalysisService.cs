using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Services.ColorProvider;
using FlossApp.Application.Utils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Application.Services.ImageAnalysis;

public class ImageAnalysisService : IImageAnalysisService
{
    private readonly IColorProviderService _colorProviderService;

    public ImageAnalysisService(IColorProviderService colorProviderService)
    {
        _colorProviderService = colorProviderService;
    }

    public async Task<Dictionary<RichColor, int>> GetPaletteAsync(Image<Rgba32> image, ColorSchema schema)
    {
        Dictionary<RichColor, int> ret = [];

        var set = (await _colorProviderService.GetRichColorsAsync(schema)).ToList();

        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < accessor.Height; y++)
            {
                var rowSpan = accessor.GetRowSpan(y);
                for (int x = 0; x < rowSpan.Length; x++)
                {
                    var color = System.Drawing.Color.FromArgb(rowSpan[x].A, rowSpan[x].R, rowSpan[x].G, rowSpan[x].B);
                    var match = set.FirstOrDefault(c => ColorUtils.ColorEquals(c, color));
                    if (!ret.TryAdd(match, 1))
                    {
                        ret[match]++;
                    }
                }
            }
        });

        return ret;
    }
}
