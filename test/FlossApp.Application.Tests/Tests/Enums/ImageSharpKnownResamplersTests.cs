using System.Reflection;
using FlossApp.Application.Enums;
using FlossApp.Application.Extensions.FlossApp.Application.Enums;
using FlossApp.Application.Tests.Generators;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

namespace FlossApp.Application.Tests.Tests.Enums;

[TestClass]
public class ImageSharpKnownResamplersTests
{
    [TestMethod]
    [DynamicData(nameof(ImageSharpKnownResamplersGenerator.GenerateImageSharpKnownResamplers),
        typeof(ImageSharpKnownResamplersGenerator), DynamicDataSourceType.Method)]
    public void EnumMapsToClass(ImageSharpKnownResamplers val)
    {
        _ = val.AsKnownResampler();
    }

    [TestMethod]
    [DynamicData(nameof(KnownResamplersGenerator.GenerateKnownResamplers),
        typeof(KnownResamplersGenerator), DynamicDataSourceType.Method)]
    public void ClassMapsToEnum((IResampler Resampler, string Name) resampler)
    {
        var fromEnum = Enum.GetValues<ImageSharpKnownResamplers>()
            .Select(x => x.AsKnownResampler());

        Assert.IsTrue(fromEnum.Any(x => ReferenceEquals(x, resampler.Resampler)));
    }
}
