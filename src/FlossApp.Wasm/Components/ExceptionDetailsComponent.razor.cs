using Microsoft.AspNetCore.Components;

namespace FlossApp.Wasm.Components;

public partial class ExceptionDetailsComponent
{
    [Parameter]
    public Exception Ex
    {
        get;
        set
        {
            if (EqualityComparer<Exception>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            ExChanged.InvokeAsync(value);
        }
    }

    [Parameter] public EventCallback<Exception> ExChanged { get; set; }
}
