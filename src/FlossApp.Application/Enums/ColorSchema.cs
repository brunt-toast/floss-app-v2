using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlossApp.Application.Enums;

public enum ColorSchema
{
    [Display(Name= "RGB", Description="Red/Green/Blue")]
    Rgb,

    [Display(Name = "Hex", Description="Hexadecimal RGB")]
    RgbHex,

    [Display(Name = "HSL", Description="Hue/Saturation/Lightness")]
    Hsl,

    [Display(Name= "DMC", Description="Dollfus-Mieg et Compagnie")]
    Dmc,

    [Display(Name = "HTML", Description="HyperText Markup Language")]
    Html,

    [Display(Name = "COPIC", Description="\u30b3\u30d4\u30c3\u30af")]
    Copic,

    [Display(Name = "Anchor")]
    Anchor,

    [Display(Name = "Pantone")]
    Pantone,

    [Display(Name="CMYK", Description="Cyan/Magenta/Yellow/Black")]
    Cmyk,

    [Display(Name = "CIE L*a*b*", Description = "Commission Internationale de l'\u00c9clairage: Lightness, Green-Red, Blue-Yellow")]
    CieLab,
}
