using FlossApp.Application.Enums;
using FlossApp.Application.Models.RichColor;
using FlossApp.Application.Utils;

namespace FlossApp.Application.Tests.Generators;

internal static class ColorComparisonAlgorithmsGenerator
{
    public static IEnumerable<object[]> GetColorComparisonAlgorithms()
    {
        return Enum
            .GetValues<ColorComparisonAlgorithms>()
            .Select<ColorComparisonAlgorithms, object[]>(x => [x]);
    }
}
