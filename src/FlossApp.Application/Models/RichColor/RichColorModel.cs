using FlossApp.Application.Extensions.System.Drawing;

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

    public IEnumerable<RichColorModel> GetMostSimilarColors(IList<RichColorModel> set, int nMatches = 5)
    {
        const double weightR = 0.299;
        const double weightG = 0.587;
        const double weightB = 0.114;

        RichColorModel model = this;
        return set
            .Select(color => new
            {
                Color = color,
                Distance = Math.Sqrt(
                    weightR * Math.Pow(color.Red - model.Red, 2) +
                    weightG * Math.Pow(color.Green - model.Green, 2) +
                    weightB * Math.Pow(color.Blue - model.Blue, 2))
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
}
