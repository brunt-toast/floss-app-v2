using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using FlossApp.Application.Benchmarks.Config;
using FlossApp.Application.Services.ColorProvider;

namespace FlossApp.Application.Benchmarks.Benchmarks.Services.ColorProvider;


[Config(typeof(WindowsDefenderFriendlyConfig))]
[MemoryDiagnoser]
public class PopulateCacheAsyncBenchmarks
{
    [Benchmark]
    public async Task FullCachePopulation()
    {
        var colorProviderService = new ColorProviderService();
        await colorProviderService.PopulateCacheAsync();
    }
}
