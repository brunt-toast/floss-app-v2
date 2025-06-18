using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Tests.Mock;
using FlossApp.Application.ViewModels.Colors;
using FlossApp.Application.ViewModels.Images;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Application.Tests.ViewModels.Images;

[TestClass]
public class ImageFilterViewModelTests
{
    [TestMethod]
    public async Task OutputImage_ShouldMatch_RequestedSize()
    {
        var services = new MockServiceProvider();
        IImageFilterViewModel viewModel = new ImageFilterViewModel(services);

        const int initWidth = 100;
        const int initHeight = 100;
        const float pixelRatio = 0.5f;

        var imageIn = new Image<Rgba32>(initWidth, initHeight, new Rgba32(255, 255, 255));
        await using var inStream = new MemoryStream();
        await imageIn.SaveAsPngAsync(inStream);
        inStream.Position = 0;

        await viewModel.LoadFileStreamAsync(inStream);
        viewModel.PixelRatio = pixelRatio;
        await viewModel.ProcessImageAsync();
        var imageOut = viewModel.ImageOut;

        Assert.AreEqual(initWidth * pixelRatio, imageOut.Width);
        Assert.AreEqual(initHeight * pixelRatio, imageOut.Height);
    }
}
