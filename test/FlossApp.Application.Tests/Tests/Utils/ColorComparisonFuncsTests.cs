using FlossApp.Application.Enums;
using FlossApp.Application.Tests.Generators;
using FlossApp.Application.Utils;

namespace FlossApp.Application.Tests.Tests.Utils;

[TestClass]
public class ColorComparisonFuncsTests
{
    [TestMethod]
    [DynamicData(nameof(ColorComparisonAlgorithmsGenerator.GetColorComparisonAlgorithms), typeof(ColorComparisonAlgorithmsGenerator), DynamicDataSourceType.Method)]

    public void IsAlgorithmImplemented(ColorComparisonAlgorithms algorithm)
    {
        _ = ColorComparisonFuncs.GetComparisonAlgorithm(algorithm);
    }
}
