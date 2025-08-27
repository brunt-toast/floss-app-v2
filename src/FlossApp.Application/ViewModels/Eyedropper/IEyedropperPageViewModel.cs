using FlossApp.Application.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Application.ViewModels.Eyedropper;

public interface IEyedropperPageViewModel
{
    ColorSchema TargetSchema { get; set; }
    ColorComparisonAlgorithms ComparisonAlgorithm { get; set; }
    Image<Rgba32> ImageIn { get; }

    Task LoadFileStreamAsync(Stream stream);
}
