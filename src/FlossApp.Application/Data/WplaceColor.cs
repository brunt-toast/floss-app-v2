using FlossApp.Application.Utils;
using FlossApp.Core;
using Newtonsoft.Json;

namespace FlossApp.Application.Data;

public struct WplaceColor : IColorFromJson
{
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("hex")] public string Hex { get; set; }
    [JsonProperty("isPremium")] public bool IsPremium { get; set; }

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
