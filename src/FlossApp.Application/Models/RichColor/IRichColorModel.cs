using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlossApp.Application.Models.RichColor;

public interface IRichColorModel
{
    public byte Red { get; }
    public byte Green { get; }
    public byte Blue { get; }

    public string Number { get; }
    public string Name { get; }
}