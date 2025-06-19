using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Models.RichColor;
using FlossApp.Application.Services.ColorProvider;
using Microsoft.AspNetCore.Components;

namespace FlossApp.Wasm.Components;

public partial class RichColorPicker
{
    [Parameter]
    public RichColorModel Value
    {
        get;
        set
        {
            if (EqualityComparer<RichColorModel>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            ValueChanged.InvokeAsync(value);
        }
    }

    [Parameter] public EventCallback<RichColorModel> ValueChanged { get; set; }

    [Parameter]
    public string Label
    {
        get;
        set
        {
            if (EqualityComparer<string>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            LabelChanged.InvokeAsync(value);
        }
    } = "";

    [Parameter] public EventCallback<string> LabelChanged { get; set; }

    [Parameter]
    public ColorSchema Schema
    {
        get;
        set
        {
            if (EqualityComparer<ColorSchema>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            SchemaChanged.InvokeAsync(value);
        }
    }

    [Parameter] public EventCallback<ColorSchema> SchemaChanged { get; set; }

    [Inject] private IColorProviderService ColorProviderService { get; set; } = null!;

    private async Task<IEnumerable<RichColorModel>>? RichColorSearch(string? arg1, CancellationToken arg2)
    {
        if (arg1 is null)
        {
            return [];
        }

        var allColors = await ColorProviderService.GetRichColorsAsync(Schema);
        return allColors
            .Select(x => new { SearchString = RichColorToString(x), Value = x })
            .Where(x => x.SearchString.Contains(arg1, StringComparison.InvariantCultureIgnoreCase))
            .OrderBy(x => x.SearchString.IndexOf(arg1, StringComparison.InvariantCultureIgnoreCase))
            .ThenBy(x => x.SearchString)
            .Select(x => x.Value);
    }

    private static string RichColorToString(RichColorModel arg)
    {
        return $"{arg.Number} {arg.Name}";
    }
}
