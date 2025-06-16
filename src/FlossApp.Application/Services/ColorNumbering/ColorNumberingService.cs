using System.Drawing;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Interfaces;

namespace FlossApp.Application.Services.ColorNumbering;

public class ColorNumberingService : IColorNumberingService
{
    public async Task<string> GetNumberAsync(Color color, ColorSchema schema)
    {
        return schema switch
        {
            ColorSchema.Rgb => $"{color.R:X}{color.G:X}{color.B:X}",
            ColorSchema.Dmc => await GetDmcNumberAsync(color),
            _ => throw new ArgumentOutOfRangeException(nameof(schema), schema, null)
        };
    }

    private static async Task<string> GetDmcNumberAsync(Color color)
    {
        var dmcColors = await DmcColor.GetAllAsync();
        IRichColor target = dmcColors.FirstOrDefault(x => x.Red == color.R && x.Green == color.G && x.Blue == color.B) ?? new RichColor();
        return target.Number;
    }
}
