using FlossApp.Application.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Application.Services.ImageFiltering;

public interface IImageFilteringService
{
    public Task<Image<Rgba32>> ReduceToSchemaColorsAsync(Image<Rgba32> image, ColorSchema schema);
    public Image<Rgba32> PixelateImage(Image<Rgba32> input, float scale);
}
