using System.Drawing;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Services.ColorProvider;
using FlossApp.Application.Utils;
using MethodTimer;
namespace FlossApp.Application.Services.ColorNaming;

public class ColorNamingService : IColorNamingService
{
    private readonly IColorProviderService _colorProviderService;

    public ColorNamingService(IColorProviderService colorProviderService)
    {
        _colorProviderService = colorProviderService;
    }

    [Time]
    public async Task<string> GetNameAsync(Color color, ColorSchema schema)
    {
        var colors = await _colorProviderService.GetRichColorsAsync(schema);
        RichColor target = colors.FirstOrDefault(x => x.Red == color.R && x.Green == color.G && x.Blue == color.B);
        return target.Name;
    }
}
