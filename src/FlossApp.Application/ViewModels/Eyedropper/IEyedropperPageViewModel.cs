using System.ComponentModel;
using FlossApp.Application.Enums;
using FlossApp.Application.Models.RichColor;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Application.ViewModels.Eyedropper;

public interface IEyedropperPageViewModel : INotifyPropertyChanged
{
    ColorSchema TargetSchema { get; set; }
    ColorComparisonAlgorithms ComparisonAlgorithm { get; set; }
    RichColorModel SelectedColor { get; }
    Image<Rgba32> ImageIn { get; }

    void Init();
    Task LoadFileStreamAsync(Stream stream);
    bool ValidSchemaFilter(ColorSchema schema);
}
