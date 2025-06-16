using System.Drawing;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Utils;
using Newtonsoft.Json;

namespace FlossApp.Application.Services.ColorProvider;

public class ColorProviderService : IColorProviderService
{
    public async Task<IEnumerable<Color>> GetColorsAsync(ColorSchema schema)
    {
        return schema switch
        {
            ColorSchema.Rgb => [],
            ColorSchema.Dmc => await GetDmcColorsAsync(),
            ColorSchema.Html => await GetHtmlColorsAsync(),
            ColorSchema.Copic => await GetCopicColorsAsync(),
            _ => throw new ArgumentOutOfRangeException(nameof(schema), schema, null)
        };
    }

    private static async Task<IEnumerable<Color>> GetDmcColorsAsync()
    {
        string json = await AsyncEmbeddedResourceReader.ReadEmbeddedResourceAsync(typeof(DmcColor).Assembly, "Dmc.json");
        DmcColor[] dmcColors = JsonConvert.DeserializeObject<DmcColor[]>(json) ?? [];
        return dmcColors.Select(x => Color.FromArgb(255, x.Red, x.Green, x.Blue));
    }

    private static async Task<IEnumerable<Color>> GetHtmlColorsAsync()
    {
        string json = await AsyncEmbeddedResourceReader.ReadEmbeddedResourceAsync(typeof(HtmlColor).Assembly, "Html.json");
        HtmlColor[] dmcColors = JsonConvert.DeserializeObject<HtmlColor[]>(json) ?? [];
        return dmcColors.Select(x => ColorUtils.FromHexCode(x.Hex));
    }

    private static async Task<IEnumerable<Color>> GetCopicColorsAsync()
    {
        string json = await AsyncEmbeddedResourceReader.ReadEmbeddedResourceAsync(typeof(CopicColor).Assembly, "Copic.json");
        CopicColor[] dmcColors = JsonConvert.DeserializeObject<CopicColor[]>(json) ?? [];
        foreach (var color in dmcColors) Console.WriteLine(color);
        return dmcColors.Select(x => ColorUtils.FromHexCode(x.Hex));
    }
}
