using FlossApp.Application.Enums;
using FlossApp.Application.Models.RichColor;

namespace FlossApp.Application.Utils;
internal static class ColorComparisonFuncs
{
    public static Func<RichColorModel, RichColorModel, double> GetComparisonAlgorithm(ColorComparisonAlgorithms comparisonAlgorithms = default)
    {
        return comparisonAlgorithms switch
        {
            ColorComparisonAlgorithms.EuclideanWeightedRgb => RankSimilarityEuclideanWeightedRgb,
            ColorComparisonAlgorithms.EuclideanRgb => RankSimilarityEuclideanRgb,
            ColorComparisonAlgorithms.ManhattanDistanceRgb => RankSimilarityManhattanDistanceRgb,
            ColorComparisonAlgorithms.CIE76 => RankSimilarityCie76Distance,
            ColorComparisonAlgorithms.CIE94Graphics => RankSimilarityCie94DistanceGraphicArts,
            ColorComparisonAlgorithms.CIE94Textiles => RankSimilarityCie94DistanceTextiles,
            ColorComparisonAlgorithms.CIEDE2000 => RankSimilarityCieDe2000Distance,
            _ => throw new ArgumentOutOfRangeException(nameof(comparisonAlgorithms), comparisonAlgorithms, null)
        };
    }

    private static double RankSimilarityEuclideanWeightedRgb(RichColorModel left, RichColorModel right)
    {
        const double weightR = 0.299;
        const double weightG = 0.587;
        const double weightB = 0.114;

        return Math.Sqrt(
            weightR * Math.Pow(left.Red - right.Red, 2) +
            weightG * Math.Pow(left.Green - right.Green, 2) +
            weightB * Math.Pow(left.Blue - right.Blue, 2));
    }

    private static double RankSimilarityEuclideanRgb(RichColorModel left, RichColorModel right)
    {
        int distRed = left.Red - right.Red;
        int distGreen = left.Green - right.Green;
        int distBlue = left.Blue - right.Blue;
        return Math.Sqrt(
            Math.Pow(distRed, 2)+
            Math.Pow(distGreen, 2)+
            Math.Pow(distBlue, 2));
    }

    private static double RankSimilarityManhattanDistanceRgb(RichColorModel left, RichColorModel right)
    {
        int dr = Math.Abs(left.Red - right.Red);
        int dg = Math.Abs(left.Green - right.Green);
        int db = Math.Abs(left.Blue - right.Blue);

        return dr + dg + db;
    }

    private static double RankSimilarityCie76Distance(RichColorModel left, RichColorModel right)
    {
        var lab1 = left.AsLabColor();
        var lab2 = right.AsLabColor();

        double deltaL = lab1.L - lab2.L;
        double deltaA = lab1.A - lab2.A;
        double deltaB = lab1.B - lab2.B;

        return Math.Sqrt(deltaL * deltaL + deltaA * deltaA + deltaB * deltaB);
    }

    private static double RankSimilarityCie94DistanceGraphicArts(RichColorModel left, RichColorModel right)
    {
        return _RankSimilarityCie94Distance(left, right, 1.0);
    }

    private static double RankSimilarityCie94DistanceTextiles(RichColorModel left, RichColorModel right)
    {
        return _RankSimilarityCie94Distance(left, right, 2.0);
    }

    private static double _RankSimilarityCie94Distance(
        RichColorModel left,
        RichColorModel right,
        double kL)
    {
        const double kC = 1.0;
        const double kH = 1.0;
        const double k1 = 0.045;
        const double k2 = 0.015;

        var lab1 = left.AsLabColor();
        var lab2 = right.AsLabColor();

        double deltaL = lab1.L - lab2.L;

        double c1 = Math.Sqrt(lab1.A * lab1.A + lab1.B * lab1.B);
        double c2 = Math.Sqrt(lab2.A * lab2.A + lab2.B * lab2.B);
        double deltaC = c1 - c2;

        double deltaA = lab1.A - lab2.A;
        double deltaB = lab1.B - lab2.B;
        double deltaHsq = deltaA * deltaA + deltaB * deltaB - deltaC * deltaC;
        double deltaH = Math.Sqrt(Math.Max(0, deltaHsq));

        const double sl = 1.0;
        double sc = 1.0 + k1 * c1;
        double sh = 1.0 + k2 * c1;

        double dE94 = Math.Sqrt(
            Math.Pow(deltaL / (kL * sl), 2) +
            Math.Pow(deltaC / (kC * sc), 2) +
            Math.Pow(deltaH / (kH * sh), 2)
        );

        return dE94;
    }

    private static double RankSimilarityCieDe2000Distance(RichColorModel left, RichColorModel right)
    {
        const double kL = 1;
        const double kC = 1;
        const double kH = 1;

        (double l1, double a1, double b1) = left.AsLabColor();
        (double l2, double a2, double b2) = right.AsLabColor();

        double avgL = (l1 + l2) / 2.0;

        double c1 = Math.Sqrt(a1 * a1 + b1 * b1);
        double c2 = Math.Sqrt(a2 * a2 + b2 * b2);
        double avgC = (c1 + c2) / 2.0;

        double g = 0.5 * (1 - Math.Sqrt(Math.Pow(avgC, 7) / (Math.Pow(avgC, 7) + Math.Pow(25.0, 7))));
        double a1Prime = a1 * (1 + g);
        double a2Prime = a2 * (1 + g);

        double c1Prime = Math.Sqrt(a1Prime * a1Prime + b1 * b1);
        double c2Prime = Math.Sqrt(a2Prime * a2Prime + b2 * b2);
        double avgCPrime = (c1Prime + c2Prime) / 2.0;

        double h1Prime = Math.Atan2(b1, a1Prime);
        double h2Prime = Math.Atan2(b2, a2Prime);

        if (h1Prime < 0) h1Prime += 2 * Math.PI;
        if (h2Prime < 0) h2Prime += 2 * Math.PI;

        double deltaLPrime = l2 - l1;
        double deltaCPrime = c2Prime - c1Prime;

        double deltahPrime;
        if (Math.Abs(h1Prime - h2Prime) <= Math.PI)
        {
            deltahPrime = h2Prime - h1Prime;
        }
        else
        {
            deltahPrime = h2Prime <= h1Prime
                ? h2Prime - h1Prime + 2 * Math.PI
                : h2Prime - h1Prime - 2 * Math.PI;
        }

        double deltaHPrime = 2 * Math.Sqrt(c1Prime * c2Prime) * Math.Sin(deltahPrime / 2);

        double avgLPrime = (l1 + l2) / 2.0;
        double avghPrime;
        if (Math.Abs(h1Prime - h2Prime) > Math.PI)
        {
            avghPrime = (h1Prime + h2Prime + 2 * Math.PI) / 2.0;
        }
        else
        {
            avghPrime = (h1Prime + h2Prime) / 2.0;
        }

        double T =
            1 - 0.17 * Math.Cos(avghPrime - Math.PI / 6) +
            0.24 * Math.Cos(2 * avghPrime) +
            0.32 * Math.Cos(3 * avghPrime + Math.PI / 30) -
            0.20 * Math.Cos(4 * avghPrime - 63 * Math.PI / 180);

        double deltaTheta = 30 * Math.Exp(-Math.Pow((180 / Math.PI * avghPrime * 180 / Math.PI - 275) / 25, 2));
        double rc = 2 * Math.Sqrt(Math.Pow(avgCPrime, 7) / (Math.Pow(avgCPrime, 7) + Math.Pow(25.0, 7)));
        double sl = 1 + ((0.015 * Math.Pow(avgLPrime - 50, 2)) / Math.Sqrt(20 + Math.Pow(avgLPrime - 50, 2)));
        double sc = 1 + 0.045 * avgCPrime;
        double sh = 1 + 0.015 * avgCPrime * T;
        double rt = -Math.Sin(2 * deltaTheta * Math.PI / 180) * rc;

        double deltaE = Math.Sqrt(
            Math.Pow(deltaLPrime / (kL * sl), 2) +
            Math.Pow(deltaCPrime / (kC * sc), 2) +
            Math.Pow(deltaHPrime / (kH * sh), 2) +
            rt * (deltaCPrime / (kC * sc)) * (deltaHPrime / (kH * sh))
        );

        return deltaE;
    }

}
