using System.Diagnostics;
using System.Text;
using FlossApp.I18n;
using Newtonsoft.Json;

namespace FlossApp.Application.Services.I18n;

// ReSharper disable once InconsistentNaming
public class I18nService : II18nService
{
    public string Language { get; set; } = "en-GB";
    public Type AnchorType { get; init; } = typeof(I18nLibTypeAnchor);

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
        string folderPrefix = AnchorType.Namespace! + ".Resources." + Language.Replace('-', '_');
        string resourceName = folderPrefix + "." + fileIdentifier + ".json";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream, Encoding.UTF8);
        return reader.ReadToEnd();
    }

    private IEnumerable<string> GetResourceNamesForCurrentCulture()
    {
        var assembly = AnchorType.Assembly;
        string folderPrefix = AnchorType.Namespace! + ".Resources." + Language.Replace('-', '_');
        string[] rNames = assembly.GetManifestResourceNames();

        return rNames.Where(name => name.StartsWith(folderPrefix))
        .Select(name => name.Replace(folderPrefix + ".", "").Replace(".json", ""));
    }
}
