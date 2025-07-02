using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlossApp.Application.Enums;

public enum HoopSizeCalculationMode
{
    [Display(Name = "Width or Height (px, st)")]
    WidthOrHeightPx,

    [Display(Name = "Thread Count (/in)")]
    ThreadCountPerInch,

    [Display(Name = "Hoop Diameter (in)")]
    HoopDiameterInches
}
