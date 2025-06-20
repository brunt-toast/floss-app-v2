using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using FlossApp.Application.Benchmarks.Config;
using FlossApp.Application.Enums;
using FlossApp.Application.Mock;
using FlossApp.Application.Services.ImageFiltering;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

namespace FlossApp.Application.Benchmarks.Benchmarks.Services.ImageFiltering;

[Config(typeof(WindowsDefenderFriendlyConfig))]
[MemoryDiagnoser]
public class PixelateImageBenchmarks
{
    private static IImageFilteringService s_imageFilteringService = null!;
    private static Image<Rgba32> s_image = null!;

    [Params(0.01f, 0.1f, 0.5f)]
    public float Scale;

    [ParamsSource(nameof(ImageSharpKnownResamplersValues))]
    public ImageSharpKnownResamplers Resampler;

    [GlobalSetup]
    public void Init()
    {
        s_imageFilteringService = new MockServiceProvider().GetRequiredService<IImageFilteringService>();
        s_image = ImageMockup.GetRandomNoise();
    }

    [Benchmark]
    public void PixelateImage()
    {
        _ = s_imageFilteringService.PixelateImage(s_image, Scale, Resampler);
    }

    public IEnumerable<ImageSharpKnownResamplers> ImageSharpKnownResamplersValues() => Enum.GetValues<ImageSharpKnownResamplers>();
}
