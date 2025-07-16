using System.Reflection;

namespace FlossApp.Application.Services.I18n;

// ReSharper disable once InconsistentNaming
public interface II18nService
{
    public string Language { get; set; }
    public Dictionary<string, object> GetResources(string identifier);
    public string GetResource(string identifier);
}
