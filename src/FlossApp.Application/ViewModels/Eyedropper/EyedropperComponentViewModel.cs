using CommunityToolkit.Mvvm.ComponentModel;
using FlossApp.Application.Enums;
using FlossApp.Application.Models.RichColor;
using FlossApp.Application.Services.ColorMatching;
using FlossApp.Core;
using Microsoft.Extensions.DependencyInjection;

namespace FlossApp.Application.ViewModels.Eyedropper;

internal partial class EyedropperComponentViewModel : ObservableObject, IEyedropperComponentViewModel
{
    private readonly IColorMatchingService _colorMatchingService;

    [ObservableProperty] public partial ColorSchema TargetSchema { get; set; }
    [ObservableProperty] public partial ColorComparisonAlgorithms ComparisonAlgorithm { get; set; }

    public EyedropperComponentViewModel(IServiceProvider services)
    {
        _colorMatchingService = services.GetRequiredService<IColorMatchingService>();
    }

    public async Task<RichColorModel> GetSimilar(System.Drawing.Color color)
    {
        var richColor = new RichColorModel(new RichColor
        {
            Red = color.R,
            Green = color.G,
            Blue = color.B,
            Name = "",
            Number = ""
        });

        var result = await _colorMatchingService.GetMostSimilarColorsAsync(richColor, TargetSchema, 1, ComparisonAlgorithm);
        return result.First();
    }
}