using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FlossApp.Wasm.Extensions.System;

public static class EnumExtensions
{
    public static string ToDisplayName(this Enum source)
    {
        string sourceString = source.ToString();
        return source.GetType()
            .GetMember(sourceString)
            .FirstOrDefault()?
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName() ?? sourceString;
    }
}
