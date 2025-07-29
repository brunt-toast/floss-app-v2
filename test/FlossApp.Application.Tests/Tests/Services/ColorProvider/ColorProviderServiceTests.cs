using System.Globalization;
using FlossApp.Application.Enums;
using FlossApp.Application.Mock;
using FlossApp.Application.Models.RichColor;
using FlossApp.Application.Services.ColorProvider;
using FlossApp.Application.Tests.Generators.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace FlossApp.Application.Tests.Tests.Services.ColorProvider;

[TestClass]
public class ColorProviderServiceTests
{
    [TestMethod]
    [DynamicData(nameof(EnumGenerator<ColorSchema>.Generate), typeof(EnumGenerator<ColorSchema>), DynamicDataSourceType.Method)]
    public async Task ColorsUniqueByFloss(ColorSchema colorSchema)
    {
        IServiceProvider services = new MockServiceProvider();
        var colorProviderService = services.GetRequiredService<IColorProviderService>();

        var allColors = (await colorProviderService.GetRichColorsAsync(colorSchema)).ToList();
        List<string> duplicates = allColors.Where(x => allColors.Count(y => y.Number == x.Number) > 1).Select(x => x.Number).ToList();

        foreach (string duplicate in duplicates)
        {
            Console.WriteLine($"Duplicate values found for {duplicate}");
        }

        if (duplicates.Count > 0)
        {
            Assert.Fail();
        }
    }

    [TestMethod]
    [DynamicData(nameof(EnumGenerator<ColorSchema>.Generate), typeof(EnumGenerator<ColorSchema>), DynamicDataSourceType.Method)]
    public async Task ColorsUniqueByRgb(ColorSchema colorSchema)
    {
        IServiceProvider services = new MockServiceProvider();
        var colorProviderService = services.GetRequiredService<IColorProviderService>();

        var allColors = (await colorProviderService.GetRichColorsAsync(colorSchema)).ToList();
        var duplicates = allColors.Where(x => allColors.Count(y => y.Red == x.Red && y.Green == x.Green && y.Blue == x.Blue) > 1)
            .Select(x => x.AsHex())
            .ToList();

        foreach (string duplicate in duplicates)
        {
            Console.WriteLine($"Duplicate values found for {duplicate}");
        }

        if (duplicates.Count > 0)
        {
            Assert.Fail();
        }
    }

    [TestMethod]
    [DynamicData(nameof(EnumGenerator<ColorSchema>.Generate), typeof(EnumGenerator<ColorSchema>), DynamicDataSourceType.Method)]
    public async Task ColorsHaveValidHex(ColorSchema colorSchema)
    {
        IServiceProvider services = new MockServiceProvider();
        var colorProviderService = services.GetRequiredService<IColorProviderService>();

        List<RichColorModel> offenders = [];
        var allColors = (await colorProviderService.GetRichColorsAsync(colorSchema)).ToList();
        foreach (var color in allColors)
        {
            int numericValue;
            try
            {
                numericValue = int.Parse(color.AsHex(), NumberStyles.HexNumber);
            }
            catch
            {
                offenders.Add(color);
                continue;
            }

            if (numericValue > 0xffffff)
            {
                offenders.Add(color);
            }
        }

        foreach (var duplicate in offenders)
        {
            Console.WriteLine($"{duplicate.Name}: #{duplicate.AsHex()} is not a valid hex code");
        }

        if (offenders.Count > 0)
        {
            Assert.Fail();
        }
    }

    [TestMethod]
    [DynamicData(nameof(EnumGenerator<ColorSchema>.Generate), typeof(EnumGenerator<ColorSchema>), DynamicDataSourceType.Method)]
    public async Task CanGetRichColorsForSchema(ColorSchema schema)
    {
        IServiceProvider services = new MockServiceProvider();
        var colorProviderService = services.GetRequiredService<IColorProviderService>();
        _ = await colorProviderService.GetRichColorsAsync(schema);
    }

    [TestMethod]
    [DynamicData(nameof(EnumGenerator<ColorSchema>.Generate), typeof(EnumGenerator<ColorSchema>), DynamicDataSourceType.Method)]
    public async Task CanGetColorsForSchema(ColorSchema schema)
    {
        IServiceProvider services = new MockServiceProvider();
        var colorProviderService = services.GetRequiredService<IColorProviderService>();
        _ = await colorProviderService.GetColorsAsync(schema);
    }
}
