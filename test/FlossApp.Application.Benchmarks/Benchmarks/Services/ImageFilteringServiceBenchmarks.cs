using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using FlossApp.Application.Benchmarks.Config;
using FlossApp.Application.Benchmarks.Generators;
using FlossApp.Application.Enums;
using FlossApp.Application.Mock;
using FlossApp.Application.Services.ImageFiltering;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Application.Benchmarks.Benchmarks.Services;

[Config(typeof(WindowsDefenderFriendlyConfig))]
[MemoryDiagnoser]
public class ImageFilteringServiceBenchmarks
{
    private static IImageFilteringService s_imageFilteringService = null!;
    private static Image<Rgba32> s_image = null!;

    [ParamsSource(typeof(ColorSchemaGenerator), nameof(ColorSchemaGenerator.GetColorSchemas))]
    public ColorSchema Schema;

    [GlobalSetup]
    public void Init()
    {
        s_imageFilteringService = new MockServiceProvider().GetRequiredService<IImageFilteringService>();
        s_image = ImageMockup.GetRandomNoise();
    }

    [Benchmark]
    public async Task ReduceToSchemaColorsAsync()
    {
        _ = await s_imageFilteringService.ReduceToSchemaColorsAsync(s_image, Schema);
    }

    [Benchmark]
    [ArgumentsSource(nameof(PixelateImageScaleValues))]
    public void PixelateImageVariesByScale(float scale)
    {
        _ = s_imageFilteringService.PixelateImage(s_image, scale);
    }

    //[Benchmark]
    //[ArgumentsSource(nameof(ImageSharpKnownResamplersValues))]
    //public void PixelateImageVariesByResampler(ImageSharpKnownResamplers resampler)
    //{
    //    _ = s_imageFilteringService.PixelateImage(s_image, 0.01f, resampler);
    //}

    [Benchmark]
    [ArgumentsSource(nameof(ReduceColorsMaxColorsValues))]
    public void ReduceColorsVariesByMaxColors(int maxColors)
    {
        _ = s_imageFilteringService.ReduceColors(s_image, maxColors);
    }

    [Benchmark]
    [ArgumentsSource(nameof(ImageSharpKnownDitheringsNullableValues))]
    public void ReduceColorsVariesByDither(ImageSharpKnownDitherings? dither)
    {
        _ = s_imageFilteringService.ReduceColors(s_image, 10, dither);
    }

    public IEnumerable<ImageSharpKnownDitherings?> ImageSharpKnownDitheringsNullableValues()
    {
        return Enum.GetValues<ImageSharpKnownDitherings>()
            .Select(x =>
            {
                ImageSharpKnownDitherings? ret = x;
                return ret;
            }).Concat([null]);
    }

    public IEnumerable<int> ReduceColorsMaxColorsValues() => [5, 10, 20, 50, 100];
    public IEnumerable<float> PixelateImageScaleValues() => [0.01f, 0.1f, 0.5f];
}
