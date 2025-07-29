namespace FlossApp.Application.Tests.Generators.Generic;

internal static class EnumGenerator<T> where T : struct, Enum
{
    public static IEnumerable<object[]> Generate()
    {
        return Enum.GetValues<T>().Select<T, object[]>(x => [x]);
    }
}
