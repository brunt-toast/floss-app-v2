using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlossApp.Application.Utils;
using FlossApp.Core;
using Newtonsoft.Json;

namespace FlossApp.Application.Data;

public struct DmcColor : IColorFromJson
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
