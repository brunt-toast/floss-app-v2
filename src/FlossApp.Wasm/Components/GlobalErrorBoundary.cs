using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace FlossApp.Wasm.Components;

public class GlobalErrorBoundary : ErrorBoundary
{
    [Inject] private ILogger<GlobalErrorBoundary> Logger { get; set; } = null!;

    protected override Task OnErrorAsync(Exception ex)
    {
        Logger.LogCritical(ex, "An unhandled exception reached the global error boundary. " +
                               "Stack trace: {newline}{trace}", Environment.NewLine, Environment.StackTrace);
        return Task.CompletedTask;
    }
}
