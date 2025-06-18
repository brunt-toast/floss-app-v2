using System.Drawing;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;

namespace FlossApp.Application.Services.ColorProvider;

public interface IColorProviderService
{
    public Task<IEnumerable<Color>> GetColorsAsync(ColorSchema schema);
    public Task<IEnumerable<IColorFromJson>> GetRichColorsAsync(ColorSchema schema);
}
