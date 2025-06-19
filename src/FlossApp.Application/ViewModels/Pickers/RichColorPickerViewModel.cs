using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using FlossApp.Application.Enums;
using FlossApp.Application.Models.RichColor;
using FlossApp.Application.Services.ColorProvider;
using FlossApp.Application.Utils;
using FlossApp.Core;
using Microsoft.Extensions.DependencyInjection;

namespace FlossApp.Application.ViewModels.Pickers;

public partial class RichColorPickerViewModel : ViewModelBase, IRichColorPickerViewModel
{
    private readonly IColorProviderService _colorProviderService;

    public RichColorPickerViewModel(IServiceProvider services) : base(services)
    {
        _colorProviderService = services.GetRequiredService<IColorProviderService>();
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedColorString))]
    public partial RichColorModel SelectedColor { get; set; }

    [ObservableProperty]
    public partial ColorSchema Schema { get; set; }

    public string SelectedColorString
    {
        get => SelectedColor.AsHex();
        set
        {
            var color = ColorUtils.FromHexCode(value);
            SelectedColor = new RichColorModel(new RichColor()
            {
                Red = color.R,
                Green = color.G,
                Blue = color.B,
                Name = "",
                Number = ""
            });
        }
    }

    public async Task<IEnumerable<RichColorModel>> RichColorSearch(string? arg1, CancellationToken arg2)
    {
        if (arg1 is null)
        {
            return [];
        }

        var allColors = await _colorProviderService.GetRichColorsAsync(Schema);
        return allColors
            .Select(x => new { SearchString = RichColorToString(x), Value = x })
            .Where(x => x.SearchString.Contains(arg1, StringComparison.InvariantCultureIgnoreCase))
            .OrderBy(x => x.SearchString.IndexOf(arg1, StringComparison.InvariantCultureIgnoreCase))
            .ThenBy(x => x.SearchString)
            .Select(x => x.Value);
    }

    public string RichColorToString(RichColorModel arg)
    {
        return $"{arg.Number} {arg.Name}";
    }
}

public interface IRichColorPickerViewModel
{
    public RichColorModel SelectedColor { get; set; }
    public string SelectedColorString { get; set; }
    public ColorSchema Schema { get; set; }

    public Task<IEnumerable<RichColorModel>> RichColorSearch(string? query, CancellationToken ct);
    public string RichColorToString(RichColorModel model);
}
