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
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Application.Benchmarks.Benchmarks.Services;

[Config(typeof(WindowsDefenderFriendlyConfig))]
[MemoryDiagnoser]
public class ImageFilteringServiceBenchmarks
{
    private static IImageFilteringService s_imageFilteringService = null!;
    private static Image<Rgba32> s_image = null!;

    [GlobalSetup]
    public void Init()
    {
        s_imageFilteringService = new MockServiceProvider().GetRequiredService<IImageFilteringService>();
        s_image = ImageMockup.GetRandomNoise();
    }

    [Benchmark]
    [ArgumentsSource(nameof(ColorSchemaValues))]
    public async Task ReduceToSchemaColorsAsync(ColorSchema schema)
    {
        _ = await s_imageFilteringService.ReduceToSchemaColorsAsync(s_image, schema);
    }

    public IEnumerable<ColorSchema> ColorSchemaValues() => Enum.GetValues<ColorSchema>();
}
