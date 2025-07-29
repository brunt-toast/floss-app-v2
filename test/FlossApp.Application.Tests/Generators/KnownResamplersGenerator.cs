using FlossApp.Application.Tests.Generators.Generic;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

namespace FlossApp.Application.Tests.Generators;
internal class KnownResamplersGenerator
{
    public static IEnumerable<object[]> GenerateKnownResamplers()
    {
        return RegistryGenerator.Generate<IResampler>(typeof(KnownResamplers));
    }
}
