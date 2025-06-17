using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlossApp.Application.Enums;

public enum ColorSchema
{
    [Display(Name="RGB")]
    Rgb,

    [Display(Name= "DMC (Dollfus-Mieg et Compagnie)")]
    Dmc,

    [Display(Name = "HTML (HyperText Markup Language)")]
    Html,

    [Display(Name = "COPIC (\u30b3\u30d4\u30c3\u30af)")]
    Copic
}
