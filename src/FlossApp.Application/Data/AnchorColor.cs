using Newtonsoft.Json;

namespace FlossApp.Application.Data;

public struct AnchorColor : IColorFromJson
{
    [JsonProperty("r")] public byte Red { get; set; }
    [JsonProperty("g")] public byte Green { get; set; }
    [JsonProperty("b")] public byte Blue { get; set; }
    [JsonProperty("description")] public string Name { get; set; }
    [JsonProperty("floss")] public string Number { get; set; }

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
