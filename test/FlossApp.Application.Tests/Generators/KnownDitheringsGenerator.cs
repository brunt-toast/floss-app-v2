using System.Reflection;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Dithering;

namespace FlossApp.Application.Tests.Generators;

internal static class KnownDitheringsGenerator
{
    public static IEnumerable<object[]> GenerateKnownDitherings()
    {
        return typeof(KnownDitherings)
             .GetProperties(BindingFlags.Public | BindingFlags.Static)
             .Where(p => typeof(IDither).IsAssignableFrom(p.PropertyType))
             .Select(p => ((IDither)p.GetValue(null)!, p.Name))
             .Select(x => new object[] { x });
    }
}
