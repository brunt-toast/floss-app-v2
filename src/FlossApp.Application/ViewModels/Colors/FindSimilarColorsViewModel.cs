using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using CommunityToolkit.Mvvm.ComponentModel;
using FlossApp.Application.Data;
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
            var schemaColors = await _colorProviderService.GetColorsAsync(schema);
            var similarColors = TargetColor.GetMostSimilarColors(schemaColors.ToList(), NumberOfMatches);

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

            Matches.TryAdd(schema, new ObservableCollection<ColorModel>(similarColorModels));
        }
    }

    public string TargetColorString
    {
        get => TargetColor.AsHex();
        set
        {
            SetProperty(ref field, value);
            TargetColor = ColorUtils.FromHexCode(value);
        }
    } = "";

    public RichColor TargetColorRich
    {
        get
        {
            var clrsTask = _colorProviderService.GetRichColorsAsync(InputSchema);
            clrsTask.Wait();
            var clrs = clrsTask.Result;
            return clrs.FirstOrDefault(x => ColorUtils.ColorEquals(x, TargetColor));
        }
        set
        {
            
            SetProperty(ref field, value);
            TargetColor = value.AsSysDrawingColor();
        }
    }

    [ObservableProperty] public partial Color TargetColor { get; set; }
    [ObservableProperty] public partial int NumberOfMatches { get; set; } = 5;
    [ObservableProperty] public partial ColorSchema InputSchema { get; set; }
    public Dictionary<ColorSchema, ObservableCollection<ColorModel>> Matches { get; } = [];
    public ObservableCollection<ColorModel> ColorsForInputSchema { get; } = [];

    private async Task OnInputSchemaChangedAsync()
    {
        IEnumerable<RichColor> colors = (await _colorProviderService.GetRichColorsAsync(InputSchema));

        foreach (var x in colors)
        {
            ColorsForInputSchema.Add(new ColorModel(InputSchema, x.Name, x.Number, Color.FromArgb(255, x.Red, x.Green, x.Blue)));
            await Task.Yield();
        }
    }

    public bool IsExactMatch(Color c)
    {
        return c.R == TargetColor.R
            && c.G == TargetColor.G
            && c.B == TargetColor.B;
    }
}

public interface IFindSimilarColorsViewModel
{
    public Color TargetColor { get; set; }
    public RichColor TargetColorRich { get; set; }
    public string TargetColorString { get; set; }
    public int NumberOfMatches { get; set; }
    public ColorSchema InputSchema { get; set; }
    public Dictionary<ColorSchema, ObservableCollection<ColorModel>> Matches { get; }
    public ObservableCollection<ColorModel> ColorsForInputSchema { get; }

    public bool IsExactMatch(Color c);
}
