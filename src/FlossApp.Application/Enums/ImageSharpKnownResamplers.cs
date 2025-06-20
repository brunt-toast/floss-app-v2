using System.ComponentModel.DataAnnotations;

namespace FlossApp.Application.Enums;

public enum ImageSharpKnownResamplers
{
    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownResamplers.Bicubic" />
    Bicubic,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownResamplers.Box" />
    Box,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownResamplers.CatmullRom" />
    [Display(Name="Catmull-Rom")]
    CatmullRom,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownResamplers.Hermite" />
    Hermite,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownResamplers.Lanczos2" />
    [Display(Name="Lanczos 2px")]
    Lanczos2,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownResamplers.Lanczos3" />
    [Display(Name="Lanczos 3px")]
    Lanczos3,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownResamplers.Lanczos5" />
    [Display(Name="Lanczos 5px")]
    Lanczos5,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownResamplers.Lanczos8" />
    [Display(Name="Lanczos 8px")]
    Lanczos8,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownResamplers.MitchellNetravali" />
    [Display(Name = "Mitchell-Netravali")]
    MitchellNetravali,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownResamplers.NearestNeighbor" />
    [Display(Name="Nearest neighbor")]
    NearestNeighbor,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownResamplers.Robidoux" />
    Robidoux,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownResamplers.RobidouxSharp" />
    [Display(Name="Robidoux Sharp")]
    RobidouxSharp,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownResamplers.Spline" />
    Spline,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownResamplers.Triangle" />
    Triangle,

    /// <inheritdoc cref="SixLabors.ImageSharp.Processing.KnownResamplers.Welch" />
    Welch
}
