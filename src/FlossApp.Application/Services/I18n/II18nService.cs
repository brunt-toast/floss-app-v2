using System.Reflection;
using FlossApp.Application.Enums;

namespace FlossApp.Application.Services.I18n;

// ReSharper disable once InconsistentNaming
public interface II18nService
{
    public Task SetLanguageAsync(SupportedLanguage value);
    public Task<SupportedLanguage> GetLanguageAsync();
    public Dictionary<string, object> GetResources(string identifier);
    public string GetResource(string identifier);
}
