using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Color = System.Drawing.Color;

namespace FlossApp.Application.Services.ImageAnalysis;

public interface IImageAnalysisService
{
    public Task<Dictionary<RichColor, int>> GetPaletteAsync(Image<Rgba32> image, ColorSchema schema);
    public IEnumerable<System.Drawing.Color> GetDistinctColors(Image<Rgba32> image);
}
