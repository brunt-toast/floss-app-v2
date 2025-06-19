using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Data;

namespace FlossApp.Application.Extensions.System.Drawing;

public static class ColorExtensions
{
    public static IEnumerable<Color> GetMostSimilarColors(this Color targetColor, IList<Color> set, int nMatches = 5)
    {
        const double weightR = 0.299;
        const double weightG = 0.587;
        const double weightB = 0.114;

        return set
            .Select(color => new
            {
                Color = color,
                Distance = Math.Sqrt(
                    weightR * Math.Pow(color.R - targetColor.R, 2) +
                    weightG * Math.Pow(color.G - targetColor.G, 2) +
                    weightB * Math.Pow(color.B - targetColor.B, 2))
            })
            .OrderBy(x => x.Distance)
            .Take(nMatches)
            .Select(x => x.Color);
    }

    public static string AsHex(this Color color)
    {
        return $"{color.R:X2}{color.G:X2}{color.B:X2}";
    }
}
