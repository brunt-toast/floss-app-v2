using FlossApp.Application.Models.RichColor;
using Microsoft.AspNetCore.Components;

namespace FlossApp.Wasm.Components;

public partial class CieLabPicker
{
    [Parameter]
    public RichColorModel Value
    {
        get => ViewModel.RichColor;
        set
        {
            if (EqualityComparer<RichColorModel>.Default.Equals(ViewModel.RichColor, value))
            {
                return;
            }

            ViewModel.RichColor = value;
            ValueChanged.InvokeAsync(value);
        }
    }

    [Parameter] public EventCallback<RichColorModel> ValueChanged { get; set; }
}
