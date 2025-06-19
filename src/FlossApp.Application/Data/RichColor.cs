using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlossApp.Application.Data;

public readonly record struct RichColor 
{
    public byte Red { get; init; }
    public byte Green { get; init; }
    public byte Blue { get; init; }
    public string Name { get; init; }
    public string Number { get; init; }

    public string AsHex()
    {
        return $"{Red:X}{Green:X}{Blue:X}";
    }

    public System.Drawing.Color AsSysDrawingColor()
    {
        return System.Drawing.Color.FromArgb(255, Red, Green, Blue);
    }
}
