using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Enums;
using FlossApp.Application.Tests.Mock;
using FlossApp.Application.ViewModels.Colors;

namespace FlossApp.Application.Tests.ViewModels.Colors;

[TestClass]
public class FinsSimilarColorsViewModelTests
{
    [TestMethod]
    public void Works()
    {
        var services = new MockServiceProvider();
        IFindSimilarColorsViewModel viewModel = new FindSimilarColorsViewModel(services);
        viewModel.TargetColor = "#555555";
        viewModel.InputSchema = ColorSchema.Rgb;
        viewModel.NumberOfMatches = 5;

        foreach (var match in viewModel.Matches)
        {
            Console.WriteLine(match.Key);
            foreach (var match2 in match.Value)
            {
                Console.WriteLine($"\t{match2.Name}");
            }
        }
    }
}
