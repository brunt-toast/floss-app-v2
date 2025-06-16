using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Data;

namespace FlossApp.Application.Utils;

internal static class AsyncEmbeddedResourceReader
{
    public static async Task<string> ReadEmbeddedResourceAsync(Assembly assembly, string resourceName)
    {
        string? fullName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(resourceName));

        if (fullName is null)
        {
            throw new FileNotFoundException($"Could not load embedded resource {resourceName}");
        }

        await using var stream = assembly.GetManifestResourceStream(fullName);
        if (stream is null)
        {
            throw new IOException($"Failed to open a stream for the embedded resource {fullName}");
        }

        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }
}
