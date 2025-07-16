using FlossApp.Application.Enums;
using FlossApp.Application.Utils;

namespace FlossApp.Application.Tests.Utils;

[TestClass]
public class ColorComparisonFuncsTests
{
    [TestMethod]
    public void AllColorComparisonAlgorithmsImplemented()
    {
        foreach (var value in Enum.GetValues<ColorComparisonAlgorithms>())
        {
            _ = ColorComparisonFuncs.GetComparisonAlgorithm(value);
        }
    }
}
