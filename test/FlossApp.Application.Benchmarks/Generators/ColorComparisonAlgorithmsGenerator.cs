using FlossApp.Application.Benchmarks.Utils;
using FlossApp.Application.Enums;
using FlossApp.Application.Models.RichColor;
using FlossApp.Application.Utils;

namespace FlossApp.Application.Benchmarks.Generators;

internal static class ColorComparisonAlgorithmsGenerator
{
    public static IEnumerable<Named<Func<RichColorModel, RichColorModel, double>>> GetColorComparisonAlgorithms()
    {
        return Enum
            .GetValues<ColorComparisonAlgorithms>()
            .Select(x => new Named<Func<RichColorModel, RichColorModel, double>>(x, ColorComparisonFuncs.GetComparisonAlgorithm(x)));
    }
}
