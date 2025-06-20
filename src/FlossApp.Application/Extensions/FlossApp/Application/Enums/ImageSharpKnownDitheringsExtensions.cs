using FlossApp.Application.Enums;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Dithering;

namespace FlossApp.Application.Extensions.FlossApp.Application.Enums;

internal static class ImageSharpKnownDitheringsExtensions
{
    public static IDither? AsKnownDithering(this ImageSharpKnownDitherings? dither)
    {
        return dither switch
        {
            ImageSharpKnownDitherings.Bayer2x2 => KnownDitherings.Bayer2x2,
            ImageSharpKnownDitherings.Ordered3x3 => KnownDitherings.Ordered3x3,
            ImageSharpKnownDitherings.Bayer4x4 => KnownDitherings.Bayer4x4,
            ImageSharpKnownDitherings.Bayer8x8 => KnownDitherings.Bayer8x8,
            ImageSharpKnownDitherings.Bayer16x16 => KnownDitherings.Bayer16x16,
            ImageSharpKnownDitherings.Atkinson => KnownDitherings.Atkinson,
            ImageSharpKnownDitherings.Burks => KnownDitherings.Burks,
            ImageSharpKnownDitherings.FloydSteinberg => KnownDitherings.FloydSteinberg,
            ImageSharpKnownDitherings.JarvisJudiceNinke => KnownDitherings.JarvisJudiceNinke,
            ImageSharpKnownDitherings.Sierra2 => KnownDitherings.Sierra2,
            ImageSharpKnownDitherings.Sierra3 => KnownDitherings.Sierra3,
            ImageSharpKnownDitherings.SierraLite => KnownDitherings.SierraLite,
            ImageSharpKnownDitherings.StevensonArce => KnownDitherings.StevensonArce,
            ImageSharpKnownDitherings.Stucki => KnownDitherings.Stucki,
            null => null,
            _ => throw new ArgumentOutOfRangeException(nameof(dither), dither, null)
        };
    }
}
