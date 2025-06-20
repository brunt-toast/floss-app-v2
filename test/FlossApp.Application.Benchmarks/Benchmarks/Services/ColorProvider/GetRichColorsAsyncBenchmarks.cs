using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using FlossApp.Application.Benchmarks.Config;
using FlossApp.Application.Benchmarks.Generators;
using FlossApp.Application.Enums;
using FlossApp.Application.Services.ColorProvider;

namespace FlossApp.Application.Benchmarks.Benchmarks.Services.ColorProvider;


[Config(typeof(WindowsDefenderFriendlyConfig))]
[MemoryDiagnoser]
public class GetRichColorsAsyncBenchmarks
{
    private static IColorProviderService s_colorProviderService = null!;

    [ParamsSource(typeof(ColorSchemaGenerator), nameof(ColorSchemaGenerator.GetColorSchemas))]
    public ColorSchema Schema;

    [GlobalSetup]
    public async Task InitAsync()
    {
        s_colorProviderService = new ColorProviderService();
        await s_colorProviderService.PopulateCacheAsync();
    }

    [Benchmark]
    public async Task GetRichColors()
    {
        var colors = await s_colorProviderService.GetRichColorsAsync(Schema);
        _ = colors.ToList();
    }
}
