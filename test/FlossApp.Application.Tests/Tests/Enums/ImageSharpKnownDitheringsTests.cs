using FlossApp.Application.Enums;
using FlossApp.Application.Extensions.FlossApp.Application.Enums;
using FlossApp.Application.Tests.Generators;
using FlossApp.Application.Tests.Generators.Generic;
using SixLabors.ImageSharp.Processing.Processors.Dithering;

namespace FlossApp.Application.Tests.Tests.Enums;

[TestClass]
public class ImageSharpKnownDitheringsTests
{
    [TestMethod]
    [DynamicData(nameof(EnumGenerator<ImageSharpKnownDitherings>.Generate), typeof(EnumGenerator<ImageSharpKnownDitherings>), DynamicDataSourceType.Method)]
    public void EnumMapsToClass(ImageSharpKnownDitherings val)
    {
        ImageSharpKnownDitherings? nullable = val;
        _ = nullable.AsKnownDithering();
    }

    [TestMethod]
    [DynamicData(nameof(KnownDitheringsGenerator.GenerateKnownDitherings),
        typeof(KnownDitheringsGenerator), DynamicDataSourceType.Method)]
    public void ClassMapsToEnum((IDither Dither, string Name) dither)
    {
        var fromEnum = Enum.GetValues<ImageSharpKnownDitherings>()
            .Cast<ImageSharpKnownDitherings?>()
            .Select(x => x.AsKnownDithering());

        Assert.IsTrue(fromEnum.Any(x => ReferenceEquals(x, dither.Dither)));
    }
}
