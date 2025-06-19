using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Extensions.System.Collections.ObjectModel;
using FlossApp.Application.Extensions.System.Drawing;
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

    private readonly AsyncDelayedAction _onPropertyChangedDelayedAction;

    public FindSimilarColorsViewModel(IServiceProvider services) : base(services)
    {
        _colorProviderService = services.GetRequiredService<IColorProviderService>();
        _colorNamingService = services.GetRequiredService<IColorNamingService>();
        _colorNumberingService = services.GetRequiredService<IColorNumberingService>();

        _onPropertyChangedDelayedAction = new AsyncDelayedAction(RefreshMatches, 1000);

        PropertyChanged += FindSimilarColorsViewModel_PropertyChanged;
    }

    private async void FindSimilarColorsViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(InputSchema))
        {
            await OnInputSchemaChangedAsync();
        }

        await _onPropertyChangedDelayedAction.TriggerWithDelayAsync();
    }

    private async Task RefreshMatches()
    {
        Matches.Clear();
        foreach (var schema in Enum.GetValues<ColorSchema>())
        {
            var schemaColors = await _colorProviderService.GetRichColorsAsync(schema);
            var similarColors = TargetColor.GetMostSimilarColors(schemaColors.ToList(), NumberOfMatches);
            Matches.TryAdd(schema, [..similarColors]);
        }
    }

    public string TargetColorString
    {
        get => TargetColor.AsHex();
        set
        {
            var clr = ColorUtils.FromHexCode(value);
            string hex = clr.AsHex();
            TargetColor = new RichColor
            {
                Red = clr.R,
                Green = clr.G,
                Blue = clr.B,
                Name = hex,
                Number = hex
            };
        }
    } 

    [ObservableProperty] public partial RichColor TargetColor { get; set; }
    [ObservableProperty] public partial int NumberOfMatches { get; set; } = 5;
    [ObservableProperty] public partial ColorSchema InputSchema { get; set; }
    public Dictionary<ColorSchema, ObservableCollection<RichColor>> Matches { get; } = [];
    public ObservableCollection<RichColor> ColorsForInputSchema { get; } = [];

    private async Task OnInputSchemaChangedAsync()
    {
        IEnumerable<RichColor> colors = (await _colorProviderService.GetRichColorsAsync(InputSchema));
        await ColorsForInputSchema.ReplaceRangeAsync(colors);
    }

    public bool IsExactMatch(RichColor c)
    {
        return c.Red == TargetColor.Red
            && c.Green == TargetColor.Green
            && c.Blue == TargetColor.Blue;
    }
}

public interface IFindSimilarColorsViewModel
{
    public RichColor TargetColor { get; set; }
    public string TargetColorString { get; set; }
    public int NumberOfMatches { get; set; }
    public ColorSchema InputSchema { get; set; }
    public Dictionary<ColorSchema, ObservableCollection<RichColor>> Matches { get; }
    public ObservableCollection<RichColor> ColorsForInputSchema { get; }

    public bool IsExactMatch(RichColor c);
}
