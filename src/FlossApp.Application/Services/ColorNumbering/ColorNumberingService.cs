using System.Drawing;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using MethodTimer;

namespace FlossApp.Application.Services.ColorNumbering;

public class ColorNumberingService : IColorNumberingService
{
    [Time]
    public async Task<string> GetNumberAsync(Color color, ColorSchema schema)
    {
        return schema switch
        {
            ColorSchema.Rgb => $"{color.R:X}{color.G:X}{color.B:X}",
            ColorSchema.Dmc => await GetDmcNumberAsync(color),
            ColorSchema.Html => await GetHtmlNumberAsync(color),
            ColorSchema.Copic => await GetCopicNumberAsync(color),
            _ => throw new ArgumentOutOfRangeException(nameof(schema), schema, null)
        };
    }

    private static async Task<string> GetDmcNumberAsync(Color color)
    {
        var dmcColors = await DmcColor.GetAllAsync();
        RichColor target = dmcColors.FirstOrDefault(x => x.Red == color.R && x.Green == color.G && x.Blue == color.B);
        return target.Number;
    }

    private static async Task<string> GetHtmlNumberAsync(Color color)
    {
        var dmcColors = await HtmlColor.GetAllAsync();
        RichColor target = dmcColors.FirstOrDefault(x => x.Red == color.R && x.Green == color.G && x.Blue == color.B);
        return target.Number;
    }

    private static async Task<string> GetCopicNumberAsync(Color color)
    {
        var dmcColors = await CopicColor.GetAllAsync();
        RichColor target = dmcColors.FirstOrDefault(x => x.Red == color.R && x.Green == color.G && x.Blue == color.B);
        return target.Number;
    }
}
