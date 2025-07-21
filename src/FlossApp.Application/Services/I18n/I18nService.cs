using System.Diagnostics;
using System.Text;
using CommunityToolkit.Mvvm.Messaging;
using FlossApp.Application.Enums;
using FlossApp.Application.Messages;
using FlossApp.Application.Services.Cookies;
using FlossApp.I18n;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace FlossApp.Application.Services.I18n;

// ReSharper disable once InconsistentNaming
public class I18nService : II18nService
{
    private readonly ICookieService _cookieService;

    private string _language = I18nConsts.DefaultLanguage;

    public Type AnchorType { get; init; } = typeof(I18nLibTypeAnchor);

    public I18nService(IServiceProvider services)
    {
        _cookieService = services.GetRequiredService<ICookieService>();
    }

    public async Task InitAsync()
    {
        await RefreshLanguage();
    }

    private async Task RefreshLanguage()
    {
        _language = (await GetLanguageAsync()).ToString().Replace('_', '-');
    }

    public async Task SetLanguageAsync(SupportedLanguage value)
    {
        await _cookieService.SetCookieAsync(NamedCookies.Language, value.ToString());
        await RefreshLanguage();
        WeakReferenceMessenger.Default.Send<LanguageChangedMessage>();
    }

    public async Task<SupportedLanguage> GetLanguageAsync()
    {
        return StringToSupportedLanguage(await _cookieService.GetCookieAsync(NamedCookies.Language));
    }

    private SupportedLanguage StringToSupportedLanguage(string? s)
    {
        s ??= I18nConsts.DefaultLanguage;
        return Enum.GetValues<SupportedLanguage>().First(x => x.ToString() == s);
    }

    public Dictionary<string, object> GetResources(string identifier) => GetResources(identifier, true);

    private Dictionary<string, object> GetResources(string identifier, bool doFallback)
    {
        string targetResourceFinalPiece = identifier.Split('.').First();

        IEnumerable<string> resourceFiles = GetResourceNamesForCurrentCulture();
        string? targetFile = resourceFiles
            .FirstOrDefault(x => x.Split('.').Last().Equals(targetResourceFinalPiece, StringComparison.OrdinalIgnoreCase));
        if (targetFile is null)
        {
            if (doFallback)
            {
                string newIdentifier = new StackTrace().GetFrame(2)?.GetMethod()?.DeclaringType?.Name + "." + identifier;
                return GetResources(newIdentifier, false);
            }

            return [];
        }

        string json = ReadResourceFile(targetFile);
        var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        string pathInJson = identifier.Replace(targetResourceFinalPiece + ".", "");
        return dict?
            .Where(kvp => kvp.Key.StartsWith(pathInJson, StringComparison.OrdinalIgnoreCase))
            .Select(kvp => new KeyValuePair<string, object>(kvp.Key.Split('.').Last(), kvp.Value))
            .ToDictionary() ?? [];
    }

    public string GetResource(string identifier) => GetResource(identifier, true);
    private string GetResource(string identifier, bool doFallback)
    {
        string targetResourceFinalPiece = identifier.Split('.').First();

        IEnumerable<string> resourceFiles = GetResourceNamesForCurrentCulture();
        string? targetFile = resourceFiles
            .FirstOrDefault(x =>
            {
                string resNameLastPiece = x.Split('.').Last();
                return resNameLastPiece.Equals(targetResourceFinalPiece, StringComparison.OrdinalIgnoreCase);
            });
        if (targetFile is null)
        {
            if (doFallback)
            {
                string newIdentifier = new StackTrace().GetFrame(2)?.GetMethod()?.DeclaringType?.Name + "." + identifier;
                return GetResource(newIdentifier, false);
            }

            return string.Empty;
        }

        string json = ReadResourceFile(targetFile);
        var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        string pathInJson = identifier.Replace(targetResourceFinalPiece + ".", "");
        return dict?.First(x => x.Key.Equals(pathInJson, StringComparison.OrdinalIgnoreCase)).Value.ToString()
            ?? string.Empty;
    }

    private string ReadResourceFile(string fileIdentifier)
    {
        var assembly = AnchorType.Assembly;
        string folderPrefix = AnchorType.Namespace! + ".Resources." + _language.Replace('-', '_');
        string resourceName = folderPrefix + "." + fileIdentifier + ".json";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream, Encoding.UTF8);
        return reader.ReadToEnd();
    }

    private IEnumerable<string> GetResourceNamesForCurrentCulture()
    {
        var assembly = AnchorType.Assembly;
        string folderPrefix = AnchorType.Namespace! + ".Resources." + _language.Replace('-', '_');
        string[] rNames = assembly.GetManifestResourceNames();

        return rNames.Where(name => name.StartsWith(folderPrefix))
        .Select(name => name.Replace(folderPrefix + ".", "").Replace(".json", ""));
    }
}
