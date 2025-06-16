using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using CommunityToolkit.Mvvm.ComponentModel;
using FlossApp.Application.Enums;
using FlossApp.Application.Extensions.System.Collections.ObjectModel;
using FlossApp.Application.Extensions.System.Drawing;
using FlossApp.Application.Models;
using FlossApp.Application.Services.ColorNaming;
using FlossApp.Application.Services.ColorNumbering;
using FlossApp.Application.Services.ColorProvider;
using FlossApp.Application.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace FlossApp.Application.ViewModels.Colors;

public partial class FindSimilarColorsViewModel : ViewModelBase, IFindSimilarColorsViewModel
{
    private readonly IColorProviderService _colorProviderService;
    private readonly IColorNamingService _colorNamingService;
    private readonly IColorNumberingService _colorNumberingService;

    private Color _targetColor;

    public FindSimilarColorsViewModel(IServiceProvider services) : base(services)
    {
        _colorProviderService = services.GetRequiredService<IColorProviderService>();
        _colorNamingService = services.GetRequiredService<IColorNamingService>();
        _colorNumberingService = services.GetRequiredService<IColorNumberingService>();

        PropertyChanged += FindSimilarColorsViewModel_PropertyChanged;
    }

    private async void FindSimilarColorsViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Matches.Clear();
        foreach (var schema in Enum.GetValues<ColorSchema>())
        {

            var schemaColors = await _colorProviderService.GetColorsAsync(schema);
            var similarColors = _targetColor.GetMostSimilarColors(schemaColors.ToList(), NumberOfMatches);

            List<ColorModel> similarColorModels = [];
            foreach (var x in similarColors)
            {
                string name = await _colorNamingService.GetNameAsync(x, schema);
                string number = await _colorNumberingService.GetNumberAsync(x, schema);
                similarColorModels.Add(new ColorModel(schema, name, number, x));
            }

            if (!similarColorModels.Any())
            {
                continue;
            }

            Matches.Add(schema, []);
            await Matches[schema].AddRangeAsync(similarColorModels);
        }
    }

    public string TargetColor
    {
        get;
        set
        {
            SetProperty(ref field, value);
            _targetColor = ColorUtils.FromHexCode(value);
        }
    } = "";

    [ObservableProperty] public partial int NumberOfMatches { get; set; } = 5;
    [ObservableProperty] public partial ColorSchema InputSchema { get; set; }
    public Dictionary<ColorSchema, ObservableCollection<ColorModel>> Matches { get; } = [];
}

public interface IFindSimilarColorsViewModel
{
    public string TargetColor { get; set; }
    public int NumberOfMatches { get; set; }
    public ColorSchema InputSchema { get; set; }
    public Dictionary<ColorSchema, ObservableCollection<ColorModel>> Matches { get; }
}
