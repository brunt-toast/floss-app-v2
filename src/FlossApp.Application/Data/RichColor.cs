using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;

namespace FlossApp.Application.Data;

public record struct RichColor 
{
    public byte Red { get; set; }
    public byte Green { get; set; }
    public byte Blue { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }

    public string AsHex()
    {
        return $"{Red:X}{Green:X}{Blue:X}";
    }

    public System.Drawing.Color AsSysDrawingColor()
    {
        return System.Drawing.Color.FromArgb(255, Red, Green, Blue);
    }
}
