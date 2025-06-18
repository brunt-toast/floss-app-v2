using System.Drawing;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Utils;
using MethodTimer;
using Newtonsoft.Json;

namespace FlossApp.Application.Services.ColorProvider;

public class ColorProviderService : IColorProviderService
{
    private readonly Dictionary<ColorSchema, RichColor[]> _cache = [];

    [Time]
    public async Task PopulateCacheAsync()
    {
        foreach (var schema in Enum.GetValues<ColorSchema>())
        {
            await PopulateCacheForSchemaAsync(schema);
        }
    }

    [Time]
    public async Task<IEnumerable<Color>> GetColorsAsync(ColorSchema schema)
    {
        var clrs = await GetRichColorsAsync(schema);
        return clrs.Select(x => Color.FromArgb(255, x.Red, x.Green, x.Blue));
    }

    [Time]
    public async Task<IEnumerable<RichColor>> GetRichColorsAsync(ColorSchema schema)
    {
        if (!_cache.ContainsKey(schema))
        {
            await PopulateCacheForSchemaAsync(schema);
        }

        return _cache.GetValueOrDefault(schema) ?? [];
    }

    private async Task PopulateCacheForSchemaAsync(ColorSchema schema)
    {
        IColorFromJson[] fromFile = schema switch
        {
            ColorSchema.Rgb => [],
            ColorSchema.Dmc => (await GetFromFileAsync<DmcColor>("Dmc.json")).Cast<IColorFromJson>().ToArray(),
            ColorSchema.Html => (await GetFromFileAsync<HtmlColor>("Html.json")).Cast<IColorFromJson>().ToArray(),
            ColorSchema.Copic => (await GetFromFileAsync<CopicColor>("Copic.json")).Cast<IColorFromJson>().ToArray(),
            _ => throw new ArgumentOutOfRangeException(nameof(schema), schema, null)
        };
        var richColors = fromFile.Select(x => x.AsRichColor()).ToArray();
        _cache.Add(schema, richColors);
    }

    private static async Task<T[]> GetFromFileAsync<T>(string resourceName)
    {
        string json = await AsyncEmbeddedResourceReader.ReadEmbeddedResourceAsync(typeof(T).Assembly, resourceName);
        return JsonConvert.DeserializeObject<T[]>(json) ?? [];
    }
}
