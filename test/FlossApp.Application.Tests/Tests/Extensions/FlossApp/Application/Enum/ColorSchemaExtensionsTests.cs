using FlossApp.Application.Enums;
using FlossApp.Application.Extensions.FlossApp.Application.Enums;
using FlossApp.Application.Tests.Generators.Generic;

namespace FlossApp.Application.Tests.Tests.Extensions.FlossApp.Application.Enum;

[TestClass]
public class ColorSchemaExtensionsTests
{
    [TestMethod]
    [DynamicData(nameof(EnumGenerator<ColorSchema>.Generate), typeof(EnumGenerator<ColorSchema>), DynamicDataSourceType.Method)]
    public void IsRgbSuperset_Handles_Exhaustive_Input(ColorSchema schema)
    {
        // throws exception if out of range
        _ = schema.IsRgbSuperset();
    }
}
