using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Models.RichColor;
using FlossApp.Application.Utils;
using FlossApp.Core;
using SixLabors.ImageSharp.ColorSpaces;

namespace FlossApp.Application.Extensions.System.Drawing;

public static class ColorExtensions
{
    public static IEnumerable<Color> GetMostSimilarColors(this Color targetColor, IList<Color> set, int nMatches = 5, ColorComparisonAlgorithms comparisonAlgorithm = default)
    {
        var func = ColorComparisonFuncs.GetComparisonAlgorithm(comparisonAlgorithm);

        var targetColorModel = new RichColorModel(new RichColor
        {
            Red = targetColor.R,
            Green = targetColor.G,
            Blue = targetColor.B,
        });

        return set
            .Select(color => new
            {
                Color = color,
                Distance = func(new RichColorModel(new RichColor
                {
                    Red = color.R,
                    Green = color.G,
                    Blue = color.B,
                }), targetColorModel)
            })
            .OrderBy(x => x.Distance)
            .Take(nMatches)
            .Select(x => x.Color);
    }

    public static string AsHex(this Color color)
    {
        return $"{color.R:X2}{color.G:X2}{color.B:X2}";
    }

    public static CmykColor AsCmyk(this Color color)
    {
        byte r = color.R;
        byte g = color.G;
        byte b = color.B;

        if (r == 0 && g == 0 && b == 0)
        {
            return new CmykColor
            {
                Cyan = 0,
                Magenta = 0,
                Yellow = 0,
                Black = 1
            };
        }

        double rNorm = r / 255.0;
        double gNorm = g / 255.0;
        double bNorm = b / 255.0;

        double k = 1 - Math.Max(Math.Max(rNorm, gNorm), bNorm);
        double c = (1 - rNorm - k) / (1 - k);
        double m = (1 - gNorm - k) / (1 - k);
        double y = (1 - bNorm - k) / (1 - k);

        return new CmykColor
        {
            Cyan = c,
            Magenta = m,
            Yellow = y,
            Black = k
        };
    }
}
