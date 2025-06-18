using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Services.ColorProvider;
using Microsoft.AspNetCore.Components;

namespace FlossApp.Wasm.Components;

public partial class RichColorPicker
{
    [Inject] private IColorProviderService ColorProviderService { get; set; } = null!;

    private async Task<IEnumerable<RichColor>> Search(string value, CancellationToken token)
    {
        var colors = await ColorProviderService.GetRichColorsAsync(Schema);
        return colors.Select(x => new { Value = x, StringValue = ToStringFunc(x) })
            .Where(x => x.StringValue.Contains(value, StringComparison.InvariantCultureIgnoreCase))
            .OrderBy(x => x.StringValue.IndexOf(value, StringComparison.InvariantCultureIgnoreCase))
            .ThenBy(x => x.StringValue)
            .Select(x => x.Value);
    }

    private static string ToStringFunc(RichColor color)
    {
        return $"{color.Number} {color.Name}";
    }

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

    [Parameter]
    public RichColor Value
    {
        get;
        set
        {
            if (EqualityComparer<RichColor>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            ValueChanged.InvokeAsync(value);
        }
    }

    [Parameter] public EventCallback<RichColor> ValueChanged { get; set; }

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
}
