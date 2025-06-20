using System.Drawing;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Extensions.FlossApp.Application.Data;
using FlossApp.Application.Models.RichColor;
using FlossApp.Application.Utils;
using FlossApp.Core;
using MethodTimer;
using Newtonsoft.Json;

namespace FlossApp.Application.Services.ColorProvider;

internal class ColorProviderService : IColorProviderService
{
    private readonly Dictionary<ColorSchema, RichColorModel[]> _cache = [];

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
        return clrs.Select(x => x.AsSysDrawingColor());
    }

    [Time]
    public async Task<IEnumerable<RichColorModel>> GetRichColorsAsync(ColorSchema schema)
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
            ColorSchema.RgbHex => [],
            ColorSchema.Hsl => [],
            ColorSchema.Cmyk => [],
            ColorSchema.Dmc => (await GetFromFileAsync<DmcColor>("Dmc.json")).Cast<IColorFromJson>().ToArray(),
            ColorSchema.Html => (await GetFromFileAsync<HtmlColor>("Html.json")).Cast<IColorFromJson>().ToArray(),
            ColorSchema.Copic => (await GetFromFileAsync<CopicColor>("Copic.json")).Cast<IColorFromJson>().ToArray(),
            ColorSchema.Anchor => (await GetFromFileAsync<AnchorColor>("Anchor.json")).Cast<IColorFromJson>().ToArray(),
            ColorSchema.Pantone => (await GetFromFileAsync<PantoneColor>("Pantone.json")).Cast<IColorFromJson>().ToArray(),
            _ => throw new ArgumentOutOfRangeException(nameof(schema), schema, null)
        };
        var richColors = fromFile
            .Select(x => x.AsRichColor())
            .ToArray()
            .GroupBy(x => (x.Red, x.Green, x.Blue))
            .Select(r => new RichColor
                {
                    Name = string.Join(", ", r.Select(x => x.Name).Distinct()),
                    Number = string.Join(", ", r.Select(x => x.Number).Distinct()),
                    Red = r.First().Red,
                    Green = r.First().Green,
                    Blue = r.First().Blue
                })
            .Select(x => new RichColorModel(x))
            .ToArray();

        _cache.Add(schema, richColors);
    }

    private static async Task<T[]> GetFromFileAsync<T>(string resourceName)
    {
        string json = await AsyncEmbeddedResourceReader.ReadEmbeddedResourceAsync(typeof(T).Assembly, resourceName);
        return JsonConvert.DeserializeObject<T[]>(json) ?? [];
    }
}
