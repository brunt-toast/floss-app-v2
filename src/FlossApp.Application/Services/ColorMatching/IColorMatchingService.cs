using FlossApp.Application.Enums;
using FlossApp.Application.Models.RichColor;

namespace FlossApp.Application.Services.ColorMatching;

public interface IColorMatchingService
{
    Task<Dictionary<ColorSchema, IEnumerable<RichColorModel>>> GetMostSimilarColorsAsync(RichColorModel targetColor, int numberOfMatches, ColorComparisonAlgorithms comparisonAlgorithm);

    Task<IEnumerable<RichColorModel>> GetMostSimilarColorsAsync(RichColorModel targetColor, ColorSchema targetSchema, int numberOfMatches, ColorComparisonAlgorithms comparisonAlgorithm);
}
