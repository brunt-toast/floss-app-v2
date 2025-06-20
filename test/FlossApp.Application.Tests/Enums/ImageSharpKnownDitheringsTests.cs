using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Enums;
using FlossApp.Application.Extensions.FlossApp.Application.Enums;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Dithering;

namespace FlossApp.Application.Tests.Enums;

[TestClass]
public class ImageSharpKnownDitheringsTests
{
    [TestMethod]
    public void AllEnumsConvertToClasses()
    {
        foreach (var val in Enum.GetValues<ImageSharpKnownDitherings>())
        {
            ImageSharpKnownDitherings? nullable = val;
            _ = nullable.AsKnownDithering();
        }
    }

    [TestMethod]
    public void AllClassesConvertToEnums()
    {
        IList<IDither?> dithersFromEnum = Enum.GetValues<ImageSharpKnownDitherings>()
            .Select(x =>
            {
                ImageSharpKnownDitherings? nullable = x;
                return nullable.AsKnownDithering();
            }).ToList();

        var dithersFromClass = typeof(KnownDitherings)
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(p => typeof(IDither).IsAssignableFrom(p.PropertyType))
            .Select(p => (p.Name, Value: p.GetValue(null)));

        foreach (var d in dithersFromClass)
        {
            if (d.Value is not IDither dither)
            {
                Assert.Fail($"Found member {d.Name}, which was not of type {nameof(IDither)}");
                continue;
            }

            Assert.IsTrue(dithersFromEnum.Any(x => ReferenceEquals(x, dither)));
        }
    }
}
