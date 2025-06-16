using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Enums;

namespace FlossApp.Application.Models;

public struct ColorModel
{
    public ColorModel(ColorSchema schema, string name, string number, Color color)
    {
        Schema = schema;
        Name = name;
        Number = number;
        Color = color;
    }

    public ColorSchema Schema { get; }
    public Color Color { get; }
    public string Name { get; }
    public string Number { get; }
}
