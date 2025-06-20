using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Enums;

namespace FlossApp.Application.Services.ColorNaming;

internal interface IColorNamingService
{
    public Task<string> GetNameAsync(Color color, ColorSchema schema);
}
