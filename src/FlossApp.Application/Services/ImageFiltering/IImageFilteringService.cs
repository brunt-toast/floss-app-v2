﻿using FlossApp.Application.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Application.Services.ImageFiltering;

internal interface IImageFilteringService
{
    public Task<Image<Rgba32>> ReduceToSchemaColorsAsync(Image<Rgba32> image, ColorSchema schema, byte transparencyThreshold, ColorComparisonAlgorithms comparisonAlgorithm = default);
    public Image<Rgba32> PixelateImage(Image<Rgba32> input, float scale, ImageSharpKnownResamplers resampler = default);
    public Image<Rgba32> ReduceColors(Image<Rgba32> input, int maxColors, ImageSharpKnownDitherings? dither = null);
    public Image<Rgba32> Upscale(Image<Rgba32> input, int scale);
}
