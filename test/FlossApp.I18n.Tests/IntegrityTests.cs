using System.Text;
using Newtonsoft.Json;

namespace FlossApp.I18n.Tests;

[TestClass]
public sealed class IntegrityTests
{
    private readonly Type _anchorType = typeof(I18nLibTypeAnchor);

    [TestMethod]
    public void AllFolders_Have_SameFiles()
    {
        var defaultLangResources = GetAllResourceFileNamesForLanguage(I18nConsts.DefaultLanguage).ToList();

        Dictionary<string, string> extras = [];
        Dictionary<string, string> missing = [];
        foreach (string languageName in GetLanguageNames())
        {
            var thisLangResources = GetAllResourceFileNamesForLanguage(languageName).ToList();

            foreach (string resName in thisLangResources)
            {
                if (!defaultLangResources.Contains(resName))
                {
                    extras.Add(languageName, resName);
                }
            }

            foreach (string resName in defaultLangResources)
            {
                if (!thisLangResources.Contains(resName))
                {
                    missing.Add(languageName, resName);
                }
            }
        }

        foreach (var kvp in extras)
        {
            Console.Error.WriteLine($"Default language {I18nConsts.DefaultLanguage} contains no definition for {kvp.Value}, but {kvp.Key} does.");
        }

        foreach (var kvp in missing)
        {
            Console.Error.WriteLine($"Default language {I18nConsts.DefaultLanguage} contains a definition for {kvp.Value}, but {kvp.Key} does not.");
        }

        Assert.AreEqual(0, missing.Count);
        Assert.AreEqual(0, extras.Count);
    }

    [TestMethod]
    public void AllFiles_Have_SameKeys()
    {
        var defaultLangResources = GetAllResourceFileNamesForLanguage(I18nConsts.DefaultLanguage).ToList();

        List<string> keys = [];
        foreach (var file in defaultLangResources)
        {
            string ident = _anchorType.Namespace! + ".Resources." + I18nConsts.DefaultLanguage.Replace("-", "_") + file;
            string content = ReadResourceFile(ident);
            Dictionary<string, object> dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(content) ?? throw new InvalidOperationException();
            keys.AddRange(dict.Keys);
        }

        Dictionary<string, string> extras = [];
        Dictionary<string, string> missing = [];
        foreach (var langName in GetLanguageNames())
        {
            var langResources = GetAllResourceFileNamesForLanguage(langName).ToList();
            foreach (var file in langResources)
            {
                string ident = _anchorType.Namespace! + ".Resources." + langName.Replace("-", "_") + file;
                string content = ReadResourceFile(ident);
                Dictionary<string, object> dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(content) ?? throw new InvalidOperationException();

                foreach (var extraKey in dict.Keys.Where(x => !keys.Contains(x)))
                {
                    extras.Add(ident, extraKey);
                }

                foreach (var extraKey in keys.Where(x => !dict.Keys.Contains(x)))
                {
                    missing.Add(ident, extraKey);
                }
            }
        }

        foreach (var kvp in extras)
        {
            Console.Error.WriteLine($"Default language {I18nConsts.DefaultLanguage} contains no definition for {kvp.Value}, but {kvp.Key} does.");
        }

        foreach (var kvp in missing)
        {
            Console.Error.WriteLine($"Default language {I18nConsts.DefaultLanguage} contains a definition for {kvp.Value}, but {kvp.Key} does not.");
        }

        Assert.AreEqual(0, missing.Count);
        Assert.AreEqual(0, extras.Count);
    }

    private string ReadResourceFile(string fileIdentifier)
    {
        var assembly = _anchorType.Assembly;
        using var stream = assembly.GetManifestResourceStream(fileIdentifier);
        ArgumentNullException.ThrowIfNull(stream);
        using var reader = new StreamReader(stream, Encoding.UTF8);
        return reader.ReadToEnd();
    }

    private IEnumerable<string> GetAllResourceFileNamesForLanguage(string langName)
    {
        var assembly = _anchorType.Assembly;
        string folderPrefix = _anchorType.Namespace! + ".Resources." + langName.Replace('-', '_');
        return assembly.GetManifestResourceNames()
            .Where(x => x.StartsWith(folderPrefix))
            .Select(x => x.Replace(folderPrefix, ""));
    }

    private IEnumerable<string> GetLanguageNames()
    {
        var assembly = _anchorType.Assembly;
        string folderPrefix = _anchorType.Namespace! + ".Resources.";
        IEnumerable<string> rNames = assembly.GetManifestResourceNames().Where(x => x.StartsWith(folderPrefix));
        int index = folderPrefix.Count(x => x == '.');
        return rNames.Select(resource => resource.Split('.')[index]);
    }
}
