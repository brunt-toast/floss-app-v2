using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlossApp.Application.Utils;

public static class ColorUtils
{
    public static Color FromHexCode(string hexCode)
    {
        hexCode = hexCode.TrimStart('#');

        Exception badLengthException = new ArgumentException($"After trimming leading '#', expected a length of 3, 6, or 8, but got {hexCode.Length}", nameof(hexCode));

        if (hexCode.Length != 3 && hexCode.Length != 6 && hexCode.Length != 8)
        {
            throw badLengthException;
        }

        try
        {
            _ = int.Parse(hexCode, NumberStyles.HexNumber);
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Could not parse {hexCode} as a hexadecimal number", nameof(hexCode), ex);
        }

        switch (hexCode.Length)
        {
            case 3:
                return FromHexCode_3Chars(hexCode);
            case 6:
                return FromHexCode_6Chars(hexCode);
            case 8:
                return FromHexCode_8Chars(hexCode);
            default:
                throw badLengthException;
        }
    }

    private static Color FromHexCode_3Chars(string hexCode)
    {
        return Color.FromArgb(
            255,
            byte.Parse(hexCode.Substring(0, 1), NumberStyles.HexNumber),
            byte.Parse(hexCode.Substring(1, 1), NumberStyles.HexNumber),
            byte.Parse(hexCode.Substring(2, 1), NumberStyles.HexNumber)
        );
    }

    private static Color FromHexCode_6Chars(string hexCode)
    {
        return Color.FromArgb(
            255,
            byte.Parse(hexCode.Substring(0, 2), NumberStyles.HexNumber),
            byte.Parse(hexCode.Substring(2, 2), NumberStyles.HexNumber),
            byte.Parse(hexCode.Substring(4, 2), NumberStyles.HexNumber)
        );
    }

    private static Color FromHexCode_8Chars(string hexCode)
    {
        return Color.FromArgb(
            byte.Parse(hexCode.Substring(6, 2), NumberStyles.HexNumber),
            byte.Parse(hexCode.Substring(0, 2), NumberStyles.HexNumber),
            byte.Parse(hexCode.Substring(2, 2), NumberStyles.HexNumber),
            byte.Parse(hexCode.Substring(4, 2), NumberStyles.HexNumber)
        );
    }
}
