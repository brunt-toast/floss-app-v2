using System.Reflection;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

namespace FlossApp.Application.Tests.Generators;
internal class KnownResamplersGenerator
{
    public static IEnumerable<object[]> GenerateKnownResamplers()
    {
        return typeof(KnownResamplers)
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(p => typeof(IResampler).IsAssignableFrom(p.PropertyType))
            .Select(p => ((IResampler)p.GetValue(null)!, p.Name))
            .Select(x => new object[] { x });
    }
}
