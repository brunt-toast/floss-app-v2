using System.Drawing;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Services.ColorProvider;
using MethodTimer;

namespace FlossApp.Application.Services.ColorNumbering;

public class ColorNumberingService : IColorNumberingService
{
    private readonly IColorProviderService _colorProviderService;

    public ColorNumberingService(IColorProviderService colorProviderService)
    {
        _colorProviderService = colorProviderService;
    }

    [Time]
    public async Task<string> GetNumberAsync(Color color, ColorSchema schema)
    {
        if (schema is ColorSchema.Rgb)
        {
            return $"{color.R:X}{color.G:X}{color.B:X}";
        }

        var colors = await _colorProviderService.GetRichColorsAsync(schema);
        return colors.Select(c => c.AsRichColor())
            .FirstOrDefault(c => c.Red == color.R && c.Green == color.G && c.Blue == color.B).Number;
    }
}
