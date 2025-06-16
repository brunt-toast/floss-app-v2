using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlossApp.Application.Utils;
using Newtonsoft.Json;

namespace FlossApp.Application.Data;

public struct DmcColor : IColorFromJson
{
    [JsonProperty("r")] public byte Red { get; set; }
    [JsonProperty("g")] public byte Green { get; set; }
    [JsonProperty("b")] public byte Blue { get; set; }
    [JsonProperty("description")] public string Name { get; set; } 
    [JsonProperty("floss")] public string Number { get; set; } 

    public static async Task<IEnumerable<RichColor>> GetAllAsync()
    {
        string json = await AsyncEmbeddedResourceReader.ReadEmbeddedResourceAsync(typeof(DmcColor).Assembly, "Dmc.json");
        return JsonConvert.DeserializeObject<DmcColor[]>(json)?.Select(x => x.AsRichColor()) ?? [];
    }

    public RichColor AsRichColor()
    {
        return new RichColor
        {
            Number = Number,
            Name = Name,
            Red = Red,
            Green = Green,
            Blue = Blue
        };
    }
}

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
