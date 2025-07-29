using FlossApp.Application.Tests.Generators.Generic;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Dithering;

namespace FlossApp.Application.Tests.Generators;

internal static class KnownDitheringsGenerator
{
    public static IEnumerable<object[]> GenerateKnownDitherings()
    {
        return RegistryGenerator.Generate<IDither>(typeof(KnownDitherings));
    }
}
