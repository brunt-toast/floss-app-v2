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
        get => ViewModel.SelectedColor;
        set
        {
            if (EqualityComparer<RichColorModel>.Default.Equals(ViewModel.SelectedColor, value))
            {
                return;
            }

            ViewModel.SelectedColor = value;
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
        get => ViewModel.Schema;
        set
        {
            if (EqualityComparer<ColorSchema>.Default.Equals(ViewModel.Schema, value))
            {
                return;
            }

            ViewModel.Schema = value;
            SchemaChanged.InvokeAsync(value);
        }
    }

    [Parameter] public EventCallback<ColorSchema> SchemaChanged { get; set; }
}
