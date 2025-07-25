using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Enums;
using FlossApp.Application.Mock;
using FlossApp.Application.ViewModels.Colors;

namespace FlossApp.Application.Tests.Tests.ViewModels.Colors;

[TestClass]
public class FindSimilarColorsViewModelTests
{
    [TestMethod]
    public void TargetColor_ShouldMatch_TargetColorString()
    {
        var services = new MockServiceProvider();
        IFindSimilarColorsViewModel viewModel = new FindSimilarColorsViewModel(services);

        viewModel.TargetColorString = "#010101";
        Assert.AreEqual(viewModel.TargetColor.AsSysDrawingColor(), Color.FromArgb(255, 1, 1, 1));
    }
}
