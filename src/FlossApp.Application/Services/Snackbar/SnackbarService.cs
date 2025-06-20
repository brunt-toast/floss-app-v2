using CommunityToolkit.Mvvm.Messaging;
using FlossApp.Application.Enums;
using FlossApp.Application.Messages;

namespace FlossApp.Application.Services.Snackbar;

public class SnackbarService : ISnackbarService
{
    public void ShowSnackbar(string message, SnackbarSeverity? severity = null, string? key = null)
    {
        WeakReferenceMessenger.Default.Send(new SnackbarMessage(message, severity, key));
    }
}
