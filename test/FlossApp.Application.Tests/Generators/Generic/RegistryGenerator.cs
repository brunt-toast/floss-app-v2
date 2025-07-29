using System.Reflection;

namespace FlossApp.Application.Tests.Generators.Generic;

internal static class RegistryGenerator
{
    public static IEnumerable<object[]> Generate<TMembers>(Type registryType)
    {
        return registryType
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(p => typeof(TMembers).IsAssignableFrom(p.PropertyType))
            .Select(p => ((TMembers)p.GetValue(null)!, p.Name))
            .Select(x => new object[] { x });
    }
}
