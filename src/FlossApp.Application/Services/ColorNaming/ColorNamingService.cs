using System.Drawing;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Interfaces;
using FlossApp.Application.Services.ColorProvider;
using FlossApp.Application.Utils;
using Newtonsoft.Json;

namespace FlossApp.Application.Services.ColorNaming;

public class ColorNamingService : IColorNamingService
{
    public async Task<string> GetNameAsync(Color color, ColorSchema schema)
    {
        return schema switch
        {
            ColorSchema.Rgb => $"{color.R:X}{color.G:X}{color.B:X}",
            ColorSchema.Dmc => await GetDmcNameAsync(color),
            _ => throw new ArgumentOutOfRangeException(nameof(schema), schema, null)
        };
    }

    private static async Task<string> GetDmcNameAsync(Color color)
    {
        var dmcColors = await DmcColor.GetAllAsync();
        IRichColor target = dmcColors.FirstOrDefault(x => x.Red == color.R && x.Green == color.G && x.Blue == color.B) ?? new RichColor();
        return target.Name;
    }
}
