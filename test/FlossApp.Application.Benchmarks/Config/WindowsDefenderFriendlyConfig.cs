using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;

namespace FlossApp.Application.Benchmarks.Config;

public class WindowsDefenderFriendlyConfig : ManualConfig
{
    public WindowsDefenderFriendlyConfig()
    {
        AddJob(Job.ShortRun.WithToolchain(InProcessEmitToolchain.Instance));
        AddExporter(CsvMeasurementsExporter.Default);
        AddExporter(RPlotExporter.Default);
    }
}
