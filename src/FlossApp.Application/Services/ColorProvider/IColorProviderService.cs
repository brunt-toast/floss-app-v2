using System.Drawing;
using FlossApp.Application.Enums;

namespace FlossApp.Application.Services.ColorProvider;

public interface IColorProviderService
{
    public Task<IEnumerable<Color>> GetColorsAsync(ColorSchema schema);
}
