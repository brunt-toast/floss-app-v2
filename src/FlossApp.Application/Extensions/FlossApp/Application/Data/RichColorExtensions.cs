using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Data;

namespace FlossApp.Application.Extensions.FlossApp.Application.Data;

public static class RichColorExtensions
{
    public static IEnumerable<RichColor> GetMostSimilarColors(this RichColor targetColor, IList<RichColor> set, int nMatches = 5)
    {
        const double weightR = 0.299;
        const double weightG = 0.587;
        const double weightB = 0.114;

        return set
            .Select(color => new
            {
                Color = color,
                Distance = Math.Sqrt(
                    weightR * Math.Pow(color.Red - targetColor.Red, 2) +
                    weightG * Math.Pow(color.Green - targetColor.Green, 2) +
                    weightB * Math.Pow(color.Blue - targetColor.Blue, 2))
            })
            .OrderBy(x => x.Distance)
            .Take(nMatches)
            .Select(x => x.Color);
    }
}
