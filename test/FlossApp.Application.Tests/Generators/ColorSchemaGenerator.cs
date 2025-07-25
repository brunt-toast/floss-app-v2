using FlossApp.Application.Enums;

namespace FlossApp.Application.Tests.Generators;

public static class ColorSchemaGenerator
{
    public static IEnumerable<object[]> GenerateColorSchemas()
    {
        return Enum.GetValues<ColorSchema>().Select<ColorSchema, object[]>(x => [x]);
    }
}
