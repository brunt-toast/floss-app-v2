using FlossApp.Application.Enums;

namespace FlossApp.Application.Benchmarks.Generators;

internal static class ColorSchemaGenerator
{
    public static IEnumerable<ColorSchema> GetColorSchemas() => Enum.GetValues<ColorSchema>();
}
