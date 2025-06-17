using System.Drawing;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Services.ColorProvider;
using FlossApp.Application.Utils;
using MethodTimer;
using Newtonsoft.Json;

namespace FlossApp.Application.Services.ColorNaming;

public class ColorNamingService : IColorNamingService
{
    [Time]
    public async Task<string> GetNameAsync(Color color, ColorSchema schema)
    {
        return schema switch
        {
            ColorSchema.Rgb => $"{color.R:X}{color.G:X}{color.B:X}",
            ColorSchema.Dmc => await GetDmcNameAsync(color),
            ColorSchema.Html => await GetHtmlNameAsync(color),
            ColorSchema.Copic => await GetCopicNameAsync(color),
            _ => throw new ArgumentOutOfRangeException(nameof(schema), schema, null)
        };
    }

    private static async Task<string> GetDmcNameAsync(Color color)
    {
        var dmcColors = await DmcColor.GetAllAsync();
        RichColor target = dmcColors.FirstOrDefault(x => x.Red == color.R && x.Green == color.G && x.Blue == color.B);
        return target.Name;
    }

    private static async Task<string> GetHtmlNameAsync(Color color)
    {
        var dmcColors = await HtmlColor.GetAllAsync();
        RichColor target = dmcColors.FirstOrDefault(x => x.Red == color.R && x.Green == color.G && x.Blue == color.B);
        return target.Name;
    }

    private static async Task<string> GetCopicNameAsync(Color color)
    {
        var dmcColors = await CopicColor.GetAllAsync();
        RichColor target = dmcColors.FirstOrDefault(x => x.Red == color.R && x.Green == color.G && x.Blue == color.B);
        return target.Name;
    }
}
