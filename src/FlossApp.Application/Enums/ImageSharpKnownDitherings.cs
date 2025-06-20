using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Processing.Processors.Dithering;

namespace FlossApp.Application.Enums;

public enum ImageSharpKnownDitherings
{
    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownDitherings.Bayer2x2" />
    [Display(Name="Bayer 2x2")]
    Bayer2x2,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownDitherings.Ordered3x3" />
    [Display(Name="Ordered 3x3")]
    Ordered3x3,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownDitherings.Bayer4x4" />
    [Display(Name="Bayer 4x4")]
    Bayer4x4,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownDitherings.Bayer8x8" />
    [Display(Name="Bayer 8x8")]
    Bayer8x8,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownDitherings.Bayer16x16" />
    [Display(Name="Bayer 16x16")]
    Bayer16x16,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownDitherings.Atkinson" />
    Atkinson,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownDitherings.Burks" />
    Burks,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownDitherings.FloydSteinberg" />
    [Display(Name="Floyd-Steinberg")]
    FloydSteinberg,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownDitherings.JarvisJudiceNinke" />
    [Display(Name="Jarvis-Judice-Ninke")]
    JarvisJudiceNinke,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownDitherings.Sierra2" />
    [Display(Name="Sierra-2")]
    Sierra2,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownDitherings.Sierra3" />
    [Display(Name="Sierra-3")]
    Sierra3,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownDitherings.SierraLite" />
    [Display(Name="Sierra-Lite")]
    SierraLite,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownDitherings.StevensonArce" />
    [Display(Name="Stevenson-Acre")]
    StevensonArce,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownDitherings.Stucki" />
    Stucki,
}
