﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlossApp.Application.Enums;

public enum ColorSchema
{
    [Display(Name= "RGB (Red/Green/Blue)")]
    Rgb,

    [Display(Name = "Hex (Hexadecimal RGB)")]
    RgbHex,

    [Display(Name = "HSL (Hue/Saturation/Lightness)")]
    Hsl,

    [Display(Name= "DMC (Dollfus-Mieg et Compagnie)")]
    Dmc,

    [Display(Name = "HTML (HyperText Markup Language)")]
    Html,

    [Display(Name = "COPIC (\u30b3\u30d4\u30c3\u30af)")]
    Copic,

    [Display(Name = "Anchor")]
    Anchor,

    [Display(Name = "Pantone")]
    Pantone,

    [Display(Name="CMYK (Cyan/Magenta/Yellow/Black)")]
    Cmyk,
}
