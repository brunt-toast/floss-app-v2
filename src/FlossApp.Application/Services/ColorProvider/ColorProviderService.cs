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
            _ => throw new ArgumentOutOfRangeException(nameof(schema), schema, null)
        };
    }

    private static async Task<IEnumerable<Color>> GetDmcColorsAsync()
    {
        string json = await AsyncEmbeddedResourceReader.ReadEmbeddedResourceAsync(typeof(DmcColor).Assembly, "Dmc.json");
        DmcColor[] dmcColors = JsonConvert.DeserializeObject<DmcColor[]>(json) ?? [];
        foreach (var c in dmcColors)
        {
            Console.WriteLine(c.Name);
        }
        return dmcColors.Select(x => Color.FromArgb(255, x.Red, x.Green, x.Blue));
    }
}
