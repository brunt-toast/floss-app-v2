using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using FlossApp.Application.Benchmarks.Config;
using FlossApp.Application.Enums;
using FlossApp.Application.Mock;
using FlossApp.Application.Services.ImageAnalysis;
using FlossApp.Application.Services.ImageFiltering;
using Microsoft.Extensions.DependencyInjection;

namespace FlossApp.Application.Benchmarks.Benchmarks.Services;

[Config(typeof(WindowsDefenderFriendlyConfig))]
[MemoryDiagnoser]
public class ImageAnalysisServiceBenchmarks
{
    private static IImageAnalysisService s_analysisService = null!;

    [GlobalSetup]
    public void Init()
    {
        s_analysisService = new MockServiceProvider().GetRequiredService<IImageAnalysisService>();
    }

    [Benchmark]
    [ArgumentsSource(nameof(ColorSchemaValues))]
    public async Task GetPalette(ColorSchema schema)
    {
        var image = ImageMockup.GetRandomNoise();
        await s_analysisService.GetPaletteAsync(image, schema);
    }

    public IEnumerable<ColorSchema> ColorSchemaValues() => Enum.GetValues<ColorSchema>();

    [Benchmark]
    public void GetDistinctColors()
    {
        var image = ImageMockup.GetRandomNoise();
        s_analysisService.GetDistinctColors(image);
    }
}
