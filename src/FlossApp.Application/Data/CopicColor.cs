using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Utils;
using Newtonsoft.Json;

namespace FlossApp.Application.Data;

public struct CopicColor : IColorFromJson
{
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("number")] public string Number { get; set; }
    [JsonProperty("hex")] public string Hex { get; set; }

    public static async Task<IEnumerable<RichColor>> GetAllAsync()
    {
        string json = await AsyncEmbeddedResourceReader.ReadEmbeddedResourceAsync(typeof(CopicColor).Assembly, "Copic.json");
        return JsonConvert.DeserializeObject<CopicColor[]>(json)?.Select(x => x.AsRichColor()) ?? [];
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
            Number = Number
        };
    }
}
