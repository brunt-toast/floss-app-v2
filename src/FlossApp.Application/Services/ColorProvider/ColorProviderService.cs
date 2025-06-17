using System.Drawing;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Utils;
using MethodTimer;
using Newtonsoft.Json;

namespace FlossApp.Application.Services.ColorProvider;

public class ColorProviderService : IColorProviderService
{
    private DmcColor[]? _dmcColorCache;
    private HtmlColor[]? _htmlColorCache;
    private CopicColor[]? _copicColorCache;

    [Time]
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

    private async Task<IEnumerable<Color>> GetDmcColorsAsync()
    {
        _dmcColorCache ??= await GetFromFileAsync<DmcColor>("Dmc.json");
        return _dmcColorCache.Select(x =>
        {
            var c = x.AsRichColor();
            return Color.FromArgb(255, c.Red, c.Green, c.Blue);
        });
    }

    private async Task<IEnumerable<Color>> GetHtmlColorsAsync()
    {
        _htmlColorCache ??= await GetFromFileAsync<HtmlColor>("Html.json");
        return _htmlColorCache.Select(x =>
        {
            var c = x.AsRichColor();
            return Color.FromArgb(255, c.Red, c.Green, c.Blue);
        });
    }

    private async Task<IEnumerable<Color>> GetCopicColorsAsync()
    {
        _copicColorCache ??= await GetFromFileAsync<CopicColor>("Copic.json");
        return _copicColorCache.Select(x =>
        {
            var c = x.AsRichColor();
            return Color.FromArgb(255, c.Red, c.Green, c.Blue);
        });
    }

    private static async Task<T[]> GetFromFileAsync<T>(string resourceName)
    {
        string json = await AsyncEmbeddedResourceReader.ReadEmbeddedResourceAsync(typeof(T).Assembly, resourceName);
        return JsonConvert.DeserializeObject<T[]>(json) ?? [];
    }
}
