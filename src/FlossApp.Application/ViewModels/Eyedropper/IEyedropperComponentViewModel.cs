using FlossApp.Application.Enums;
using FlossApp.Application.Models.RichColor;

namespace FlossApp.Application.ViewModels.Eyedropper;

public interface IEyedropperComponentViewModel
{
    public ColorComparisonAlgorithms ComparisonAlgorithm { get; set; }
    public ColorSchema TargetSchema { get; set; }
    public Task<RichColorModel> GetSimilar(System.Drawing.Color color);
}
