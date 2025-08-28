using FlossApp.Application.Enums;

namespace FlossApp.Application.Messages;

public class SnackbarMessage
{
    public SnackbarMessage(string message, SnackbarSeverity? severity = null, string? key = null)
    {
        Message = message;
        Severity = severity;
        Key = key;
    }

    public string Message { get; }
    public SnackbarSeverity? Severity { get; }
    public string? Key { get; }
}
