using System.Drawing;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Utils;
using MethodTimer;
using Newtonsoft.Json;

namespace FlossApp.Application.Services.ColorProvider;

public class ColorProviderService : IColorProviderService
{
    private readonly Dictionary<ColorSchema, IColorFromJson[]> _cache = [];

    [Time]
    public async Task<IEnumerable<Color>> GetColorsAsync(ColorSchema schema)
    {
        var clrs = await GetRichColorsAsync(schema);
        return clrs.Select(x =>
        {
            var c = x.AsRichColor();
            return Color.FromArgb(255, c.Red, c.Green, c.Blue);
        });
    }

    [Time]
    public async Task<IEnumerable<IColorFromJson>> GetRichColorsAsync(ColorSchema schema)
    {
        if (_cache.ContainsKey(schema))
        {
            return _cache.GetValueOrDefault(schema) ?? [];
        }

        IColorFromJson[] fromFile = schema switch
        {
            ColorSchema.Rgb => [],
            ColorSchema.Dmc => (await GetFromFileAsync<DmcColor>("Dmc.json")).Cast<IColorFromJson>().ToArray(),
            ColorSchema.Html => (await GetFromFileAsync<HtmlColor>("Html.json")).Cast<IColorFromJson>().ToArray(),
            ColorSchema.Copic => (await GetFromFileAsync<CopicColor>("Copic.json")).Cast<IColorFromJson>().ToArray(),
            _ => throw new ArgumentOutOfRangeException(nameof(schema), schema, null)
        };

        _cache.Add(schema, fromFile);
        return fromFile;
    }

    private static async Task<T[]> GetFromFileAsync<T>(string resourceName)
    {
        string json = await AsyncEmbeddedResourceReader.ReadEmbeddedResourceAsync(typeof(T).Assembly, resourceName);
        return JsonConvert.DeserializeObject<T[]>(json) ?? [];
    }
}
