﻿using FlossApp.Application.Utils;
using FlossApp.Core;
using Newtonsoft.Json;

namespace FlossApp.Application.Data;

public struct PantoneColor : IColorFromJson
{
    [JsonProperty("code")] public string Code { get; set; }
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("hex")] public string Hex { get; set; }

    public RichColor AsRichColor()
    {
        var color = ColorUtils.FromHexCode(Hex);
        return new RichColor
        {
            Red = color.R,
            Green = color.G,
            Blue = color.B,
            Name = Name,
            Number = Code
        };
    }
}
