using System.Drawing;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Models.RichColor;
using FlossApp.Core;

namespace FlossApp.Application.Services.ColorProvider;

public interface IColorProviderService
{
    public Task<IEnumerable<Color>> GetColorsAsync(ColorSchema schema);
    public Task<IEnumerable<RichColorModel>> GetRichColorsAsync(ColorSchema schema);
    public Task PopulateCacheAsync();
}
