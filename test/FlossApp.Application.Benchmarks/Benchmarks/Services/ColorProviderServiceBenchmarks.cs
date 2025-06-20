using BenchmarkDotNet.Attributes;
using FlossApp.Application.Benchmarks.Config;
using FlossApp.Application.Enums;
using FlossApp.Application.Services.ColorProvider;

namespace FlossApp.Application.Benchmarks.Benchmarks.Services;

[Config(typeof(WindowsDefenderFriendlyConfig))]
[MemoryDiagnoser]
public class ColorProviderServiceBenchmarks
{
    private static IColorProviderService s_colorProviderService = null!;

    [GlobalSetup]
    public async Task InitAsync()
    {
        s_colorProviderService = new ColorProviderService();
        await s_colorProviderService.PopulateCacheAsync();
    }

    [Benchmark]
    public async Task FullCachePopulation()
    {
        var colorProviderService = new ColorProviderService();
        await colorProviderService.PopulateCacheAsync();
    }

    [Benchmark]
    [ArgumentsSource(nameof(ColorSchemaValues))]
    public async Task GetColors(ColorSchema schema)
    {
        var colors = await s_colorProviderService.GetColorsAsync(schema);
        _ = colors.ToList();
    }

    [Benchmark]
    [ArgumentsSource(nameof(ColorSchemaValues))]
    public async Task GetRichColors(ColorSchema schema)
    {
        var colors = await s_colorProviderService.GetRichColorsAsync(schema);
        _ = colors.ToList();
    }

    public IEnumerable<ColorSchema> ColorSchemaValues() => Enum.GetValues<ColorSchema>();
}
