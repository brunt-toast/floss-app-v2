using FlossApp.Application.Enums;
using FlossApp.Application.Extensions.System.Drawing;
using FlossApp.Application.Utils;

namespace FlossApp.Application.Models.RichColor;

public readonly record struct RichColorModel : IRichColorModel
{
    private readonly Core.RichColor _poco;

    public RichColorModel(Core.RichColor poco)
    {
        _poco = poco;
    }

    public byte Red => _poco.Red;
    public byte Green => _poco.Green;
    public byte Blue => _poco.Blue;
    public string Number => _poco.Number;
    public string Name => _poco.Name;

    public IEnumerable<RichColorModel> GetMostSimilarColors(IList<RichColorModel> set, int nMatches = 5, ColorComparisonAlgorithms comparisonAlgorithm = default)
    {
        Func<RichColorModel, RichColorModel, double> compareFunc = ColorComparisonFuncs.GetComparisonAlgorithm(comparisonAlgorithm);

        RichColorModel model = this;
        return set
            .Select(color => new
            {
                Color = color,
                Distance = compareFunc(model, color)
            })
            .OrderBy(x => x.Distance)
            .Take(nMatches)
            .Select(x => x.Color);
    }

    public string AsHex()
    {
        return AsSysDrawingColor().AsHex();
    }

    public System.Drawing.Color AsSysDrawingColor()
    {
        return System.Drawing.Color.FromArgb(255, Red, Green, Blue);
    }

    public (double L, double A, double B) AsLabColor()
    {
        double red = Red / 255.0;
        double green = Green / 255.0;
        double blue = Blue / 255.0;

        red = red > 0.04045 ? Math.Pow((red + 0.055) / 1.055, 2.4) : red / 12.92;
        green = green > 0.04045 ? Math.Pow((green + 0.055) / 1.055, 2.4) : green / 12.92;
        blue = blue > 0.04045 ? Math.Pow((blue + 0.055) / 1.055, 2.4) : blue / 12.92;

        double x = red * 0.4124 + green * 0.3576 + blue * 0.1805;
        double y = red * 0.2126 + green * 0.7152 + blue * 0.0722;
        double z = red * 0.0193 + green * 0.1192 + blue * 0.9505;

        x /= 0.95047;
        y /= 1.00000;
        z /= 1.08883;

        x = Fxyz(x);
        y = Fxyz(y);
        z = Fxyz(z);

        double l = 116 * y - 16;
        double a = 500 * (x - y);
        double b = 200 * (y - z);

        return (l, a, b);

        static double Fxyz(double t)
        {
            return t > 0.008856 ? Math.Pow(t, 1.0 / 3.0) : (7.787 * t) + (16.0 / 116.0);
        }
    }
}
