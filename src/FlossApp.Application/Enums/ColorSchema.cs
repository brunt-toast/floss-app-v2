using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlossApp.Application.Enums;

public enum ColorSchema
{
    [Display(Name = "RGB", Description = "Red/Green/Blue. Widely used among computer scientists.")]
    Rgb,

    [Display(Name = "Hex", Description = "Hexadecimal RGB. Alternative form of expression for RGB.")]
    RgbHex,

    [Display(Name = "HSL", Description = "Hue/Saturation/Lightness. Popular among design tools.")]
    Hsl,

    [Display(Name = "DMC", Description = "Dollfus-Mieg et Compagnie. Alsatian textile company.")]
    Dmc,

    [Display(Name = "HTML", Description = "HyperText Markup Language. Used by web developers.")]
    Html,

    [Display(Name = "COPIC", Description = "\u30b3\u30d4\u30c3\u30af. Japanese marker brand.")]
    Copic,

    [Display(Name = "Anchor", Description = "Sewing thread schema.")]
    Anchor,

    [Display(Name = "Pantone", Description = "Widely-used paint colors.")]
    Pantone,

    [Display(Name = "CMYK", Description = "Cyan/Magenta/Yellow/Black. Used by printers, photographers, and artists.")]
    Cmyk,

    [Display(Name = "CIE L*a*b*", Description = "Commission Internationale de l'\u00c9clairage: Lightness, Green-Red, Blue-Yellow")]
    CieLab,

    [Display(Name = "WPlace Basic", Description = "The default colors available on https://wplace.live//")]
    WplaceBasic,
}
