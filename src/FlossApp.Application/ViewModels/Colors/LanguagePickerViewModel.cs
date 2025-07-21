using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using CommunityToolkit.Mvvm.ComponentModel;
using FlossApp.Application.Enums;
using FlossApp.Application.Services.Cookies;
using FlossApp.Application.Services.I18n;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlossApp.Application.ViewModels.Colors;

public partial class LanguagePickerViewModel : ViewModelBase, ILanguagePickerViewModel
{
    private readonly II18nService _i18nService;
    private readonly ILogger _logger;

    [ObservableProperty] public partial SupportedLanguage Language { get; set; } 

    public LanguagePickerViewModel(IServiceProvider services) : base(services)
    {
        _i18nService = services.GetRequiredService<II18nService>();
        _logger = services.GetRequiredService<ILoggerFactory>().CreateLogger<LanguagePickerViewModel>();
        PropertyChanged += LanguagePickerViewModel_OnPropertyChanged;
    }

    public async Task InitAsync()
    {
        try
        {
            Language = await _i18nService.GetLanguageAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get Language cookie");
        }
    }

    private async void LanguagePickerViewModel_OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Language))
        {
            await _i18nService.SetLanguageAsync(Language);
        }
    }
}

public interface ILanguagePickerViewModel
{
    public Task InitAsync();
    public SupportedLanguage Language { get; set; }
}
