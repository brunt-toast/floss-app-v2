using FlossApp.Application.Enums;
using FlossApp.Application.Models.RichColor;
using FlossApp.Application.Services.ColorProvider;
using Microsoft.Extensions.DependencyInjection;

namespace FlossApp.Application.Services.ColorMatching;

internal class ColorMatchingService : IColorMatchingService
{
    private readonly IColorProviderService _colorProviderService;

    public ColorMatchingService(IServiceProvider services)
    {
        _colorProviderService = services.GetRequiredService<IColorProviderService>();
    }

    public async Task<Dictionary<ColorSchema, IEnumerable<RichColorModel>>> GetMostSimilarColorsAsync(RichColorModel targetColor, int numberOfMatches, ColorComparisonAlgorithms comparisonAlgorithm)
    {
        Dictionary<ColorSchema, IEnumerable<RichColorModel>> matches = [];

        foreach (var schema in Enum.GetValues<ColorSchema>())
        {
            var similarColors = await GetMostSimilarColorsAsync(targetColor, schema, numberOfMatches, comparisonAlgorithm);
            matches.TryAdd(schema, [.. similarColors]);
        }

        return matches;
    }

    public async Task<IEnumerable<RichColorModel>> GetMostSimilarColorsAsync(RichColorModel targetColor, ColorSchema targetSchema, int numberOfMatches, ColorComparisonAlgorithms comparisonAlgorithm)
    {
        var schemaColors = await _colorProviderService.GetRichColorsAsync(targetSchema);
        return targetColor.GetMostSimilarColors(schemaColors.ToList(), numberOfMatches, comparisonAlgorithm);
    }
}
