using System.Reflection;
using FlossApp.Application.Enums;
using FlossApp.Application.Extensions.FlossApp.Application.Enums;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

namespace FlossApp.Application.Tests.Enums;

[TestClass]
public class ImageSharpKnownResamplersTests
{
    [TestMethod]
    public void AllEnumsConvertToClasses()
    {
        foreach (var val in Enum.GetValues<ImageSharpKnownResamplers>())
        {
            _ = val.AsKnownResampler();
        }
    }

    [TestMethod]
    public void AllClassesConvertToEnums()
    {
        IList<IResampler> dithersFromEnum = Enum.GetValues<ImageSharpKnownResamplers>()
            .Select(x => x.AsKnownResampler())
            .ToList();

        var dithersFromClass = typeof(KnownResamplers)
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(p => typeof(IResampler).IsAssignableFrom(p.PropertyType))
            .Select(p => (p.Name, Value: p.GetValue(null)));

        foreach (var d in dithersFromClass)
        {
            if (d.Value is not IResampler dither)
            {
                Assert.Fail($"Found member {d.Name}, which was not of type {nameof(IResampler)}");
                continue;
            }

            Assert.IsTrue(dithersFromEnum.Any(x => ReferenceEquals(x, dither)));
        }
    }
}
