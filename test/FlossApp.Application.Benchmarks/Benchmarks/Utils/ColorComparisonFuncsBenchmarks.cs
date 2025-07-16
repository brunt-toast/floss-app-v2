using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using FlossApp.Application.Benchmarks.Config;
using FlossApp.Application.Benchmarks.Generators;
using FlossApp.Application.Benchmarks.Utils;
using FlossApp.Application.Enums;
using FlossApp.Application.Mock;
using FlossApp.Application.Models.RichColor;
using FlossApp.Application.Services.ColorProvider;
using FlossApp.Application.Services.ImageAnalysis;
using FlossApp.Application.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace FlossApp.Application.Benchmarks.Benchmarks.Utils;

[Config(typeof(WindowsDefenderFriendlyConfig))]
[MemoryDiagnoser]
public class ColorComparisonFuncsBenchmarks
{
    [ParamsSource(typeof(ColorComparisonAlgorithmsGenerator), nameof(ColorComparisonAlgorithmsGenerator.GetColorComparisonAlgorithms))]
    public Named<Func<RichColorModel, RichColorModel, double>> ComparisonFunc;

    private static RichColorModel s_left;
    private static RichColorModel s_right;

    [GlobalSetup]
    public void Init()
    {
        var svc = new MockServiceProvider().GetRequiredService<IColorProviderService>();
        Random random = new();
        var anchorColors = svc.GetRichColorsAsync(ColorSchema.Anchor).GetAwaiter().GetResult().ToList();
        s_left = anchorColors[random.Next(anchorColors.Count)];
        s_right = anchorColors[random.Next(anchorColors.Count)];
    }

    [Benchmark]
    public void DoComparison()
    {
        _ = ComparisonFunc.Value(s_left, s_right);
    }
}
