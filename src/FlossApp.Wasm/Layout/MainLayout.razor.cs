using CommunityToolkit.Mvvm.Messaging;
using FlossApp.Application.Enums;
using FlossApp.Application.Messages;
using FlossApp.Wasm.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FlossApp.Wasm.Layout;

public partial class MainLayout
{
    private GlobalErrorBoundary? _errorBoundary;

    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private NavigationManager Navigation { get; set; } = null!;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        WeakReferenceMessenger.Default.Register<SnackbarMessage>(this, OnSnackbarMessage);
        WeakReferenceMessenger.Default.Register<LanguageChangedMessage>(this, OnLanguageChangedMessage);
    }

    private void OnLanguageChangedMessage(object recipient, LanguageChangedMessage message)
    {
        Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
    }

    private void AttemptRecovery()
    {
        _errorBoundary?.Recover();
    }

    private void CopyErrorAsJson()
    {
        throw new NotImplementedException();
    }

    private void OnSnackbarMessage(object recipient, SnackbarMessage message)
    {
        Severity severity = message.Severity switch
        {
            SnackbarSeverity.Normal => Severity.Normal,
            SnackbarSeverity.Info => Severity.Info,
            SnackbarSeverity.Success => Severity.Success,
            SnackbarSeverity.Warn => Severity.Warning,
            SnackbarSeverity.Error => Severity.Error,
            null => Severity.Normal,
            _ => throw new ArgumentOutOfRangeException()
        };

        Snackbar.Add(message.Message, severity, key: message.Key);
    }
}
