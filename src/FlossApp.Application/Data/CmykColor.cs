using System.Drawing;
using FlossApp.Core;

namespace FlossApp.Application.Data;

public readonly struct CmykColor : IColorFromJson
{
    public double Cyan { get; init; }
    public double Magenta { get; init; }
    public double Yellow { get; init; }
    public double Black { get; init; }

    public RichColor AsRichColor()
    {
        string stringRepresentation = $"cmyk({Cyan * 100}%, {Magenta * 100}%, {Yellow * 100}%, {Black * 100}%)";
        Color sdc = ToSysDrawingColor();

        return new RichColor
        {
            Red = sdc.R,
            Green = sdc.G,
            Blue = sdc.B,
            Name = stringRepresentation,
            Number = stringRepresentation
        };
    }

    private Color ToSysDrawingColor()
    {
        double r = 1 - Math.Min(1, Cyan * (1 - Black) + Black);
        double g = 1 - Math.Min(1, Magenta * (1 - Black) + Black);
        double b = 1 - Math.Min(1, Yellow * (1 - Black) + Black);

        return Color.FromArgb(
            255,
            (int)Math.Round(r * 255),
            (int)Math.Round(g * 255),
            (int)Math.Round(b * 255)
        );
    }
}
