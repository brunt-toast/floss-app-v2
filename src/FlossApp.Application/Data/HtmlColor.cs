using FlossApp.Application.Utils;
using Newtonsoft.Json;

namespace FlossApp.Application.Data;

public struct HtmlColor : IColorFromJson
{
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("hex")] public string Hex { get; set; }

    public static async Task<IEnumerable<RichColor>> GetAllAsync()
    {
        string json = await AsyncEmbeddedResourceReader.ReadEmbeddedResourceAsync(typeof(DmcColor).Assembly, "Html.json");
        return JsonConvert.DeserializeObject<HtmlColor[]>(json)?.Select(x => x.AsRichColor()) ?? [];
    }

    public RichColor AsRichColor()
    {
        var color = ColorUtils.FromHexCode(Hex);
        return new RichColor
        {
            Red = color.R,
            Green = color.G,
            Blue = color.B,
            Name = Name,
            Number = Hex
        };
    }
}
