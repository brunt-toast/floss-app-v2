using Microsoft.AspNetCore.Components;

namespace FlossApp.Wasm.Components;

public partial class EnumPicker<T> where T : struct, Enum
{
    [Parameter]
    public T Value
    {
        get;
        set
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return;
            }
            field = value;
            ValueChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<T> ValueChanged { get; set; }

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

    [Parameter]
    public EventCallback<string> LabelChanged { get; set; }
}
