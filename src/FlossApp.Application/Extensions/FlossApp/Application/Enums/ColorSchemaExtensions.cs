using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Enums;

namespace FlossApp.Application.Extensions.FlossApp.Application.Enums;

internal static class ColorSchemaExtensions
{
    public static bool IsRgbSuperset(this ColorSchema schema)
    {
        switch (schema)
        {
            case ColorSchema.Rgb:
            case ColorSchema.RgbHex:
            case ColorSchema.Hsl:
                return true;
            case ColorSchema.Html:
            case ColorSchema.Copic:
            case ColorSchema.Anchor:
            case ColorSchema.Pantone:
            case ColorSchema.Dmc:
                return false;
            default:
                throw new ArgumentOutOfRangeException(nameof(schema), schema, null);
        }
    }
}
