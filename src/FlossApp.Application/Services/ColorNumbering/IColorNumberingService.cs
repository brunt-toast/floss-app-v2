using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Enums;

namespace FlossApp.Application.Services.ColorNumbering;

internal interface IColorNumberingService
{
    public Task<string> GetNumberAsync(Color color, ColorSchema schema);
}
