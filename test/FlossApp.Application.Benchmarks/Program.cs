﻿using BenchmarkDotNet.Running;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(["--filter", "*ColorComparisonFuncsBenchmarks*"]);
