using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using CommunityToolkit.Mvvm.ComponentModel;
using FlossApp.Application.Enums;
using FlossApp.Application.Services.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlossApp.Application.ViewModels.Colors;

public partial class LanguagePickerViewModel : ViewModelBase, ILanguagePickerViewModel
{
    private readonly ICookieService _cookieService;
    private readonly ILogger _logger;

    [ObservableProperty] public partial SupportedLanguage Language { get; set; } 

    public LanguagePickerViewModel(IServiceProvider services) : base(services)
    {
        _cookieService = services.GetRequiredService<ICookieService>();
        _logger = services.GetRequiredService<ILoggerFactory>().CreateLogger<LanguagePickerViewModel>();
        PropertyChanged += LanguagePickerViewModel_OnPropertyChanged;
    }

    public async Task InitAsync()
    {
        try
        {
            Language = StringToSupportedLanguage(await _cookieService.GetCookieAsync("Language") ?? "");
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
            await _cookieService.SetCookieAsync("Language", Language.ToString());
        }
    }

    private SupportedLanguage StringToSupportedLanguage(string s)
    {
        return Enum.GetValues<SupportedLanguage>().First(x => x.ToString() == s);
    }
}

public interface ILanguagePickerViewModel
{
    public Task InitAsync();
    public SupportedLanguage Language { get; set; }
}
