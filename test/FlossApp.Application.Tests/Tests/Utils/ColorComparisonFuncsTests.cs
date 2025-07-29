using FlossApp.Application.Enums;
using FlossApp.Application.Tests.Generators.Generic;
using FlossApp.Application.Utils;

namespace FlossApp.Application.Tests.Tests.Utils;

[TestClass]
public class ColorComparisonFuncsTests
{
    [TestMethod]
    [DynamicData(nameof(EnumGenerator<ColorComparisonAlgorithms>.Generate), typeof(EnumGenerator<ColorComparisonAlgorithms>), DynamicDataSourceType.Method)]

    public void IsAlgorithmImplemented(ColorComparisonAlgorithms algorithm)
    {
        _ = ColorComparisonFuncs.GetComparisonAlgorithm(algorithm);
    }
}
