using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Extensions.FlossApp.Application.Data;
using FlossApp.Application.Services.ColorProvider;
using FlossApp.Application.Tests.Mock;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;

namespace FlossApp.Application.Tests.Services.ColorProvider;

[TestClass]
public class ColorProviderServiceTests
{
    [TestMethod]
    public async Task ColorsUniqueByFloss()
    {
        IServiceProvider services = new MockServiceProvider();
        var colorProviderService = services.GetRequiredService<IColorProviderService>();

        Dictionary<ColorSchema, List<string>> duplicates = [];
        foreach (var colorSchema in Enum.GetValues<ColorSchema>())
        {
            var allColors = (await colorProviderService.GetRichColorsAsync(colorSchema)).ToList();
            duplicates.Add(colorSchema, allColors.Where(x => allColors.Count(y => y.Number == x.Number) > 1).Select(x => x.Number).ToList());
        }

        foreach (KeyValuePair<ColorSchema, List<string>> duplicate in duplicates)
        {
            foreach (string s in duplicate.Value)
            {
                Console.WriteLine($"Duplicate values found in {duplicate.Key} for {s}");
            }
        }

        if (duplicates.SelectMany(x => x.Value).Any())
        {
            Assert.Fail();
        }
    }

    [TestMethod]
    public async Task ColorsUniqueByRgb()
    {
        IServiceProvider services = new MockServiceProvider();
        var colorProviderService = services.GetRequiredService<IColorProviderService>();

        Dictionary<ColorSchema, List<string>> duplicates = [];
        foreach (var colorSchema in Enum.GetValues<ColorSchema>())
        {
            var allColors = (await colorProviderService.GetRichColorsAsync(colorSchema)).ToList();
            duplicates.Add(colorSchema, allColors.Where(x => allColors.Count(y => y.Red == x.Red && y.Green == x.Green && y.Blue == x.Blue) > 1)
                .Select(x => x.AsHex())
                .ToList());
        }

        foreach (KeyValuePair<ColorSchema, List<string>> duplicate in duplicates)
        {
            foreach (string s in duplicate.Value)
            {
                Console.WriteLine($"Duplicate values found in {duplicate.Key} for {s}");
            }
        }

        if (duplicates.SelectMany(x => x.Value).Any())
        {
            Assert.Fail();
        }
    }

    [TestMethod]
    public async Task ColorsHaveValidHex()
    {
        IServiceProvider services = new MockServiceProvider();
        var colorProviderService = services.GetRequiredService<IColorProviderService>();

        Dictionary<ColorSchema, List<RichColor>> offenders = [];
        foreach (var colorSchema in Enum.GetValues<ColorSchema>())
        {
            offenders.Add(colorSchema, []);
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
                    offenders[colorSchema].Add(color);
                    continue;
                }

                if (numericValue > 0xffffff)
                {
                    offenders[colorSchema].Add(color);
                }
            }
        }

        foreach (KeyValuePair<ColorSchema, List<RichColor>> duplicate in offenders)
        {
            foreach (RichColor s in duplicate.Value)
            {
                Console.WriteLine($"{duplicate.Key}: #{s.AsHex()} is not a valid hex code");
            }
        }

    }
}
