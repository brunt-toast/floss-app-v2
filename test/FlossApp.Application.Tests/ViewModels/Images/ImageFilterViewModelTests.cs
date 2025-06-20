using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Mock;
using FlossApp.Application.Services.ImageAnalysis;
using FlossApp.Application.Services.ImageFiltering;
using FlossApp.Application.ViewModels.Colors;
using FlossApp.Application.ViewModels.Images;
using Microsoft.Extensions.DependencyInjection;
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

        var imageIn = ImageMockup.GetRandomNoise(initWidth, initHeight);
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

    [TestMethod]
    public async Task OutputImage_ShouldMatch_RequestedFidelity()
    {
        var services = new MockServiceProvider();
        IImageAnalysisService imageAnalysisService = services.GetRequiredService<IImageAnalysisService>();
        IImageFilterViewModel viewModel = new ImageFilterViewModel(services);

        const int targetFidelity = 10;

        var imageIn = ImageMockup.GetRandomNoise();

        int nInputColors = imageAnalysisService.GetDistinctColors(imageIn).Count();
        if (nInputColors <= targetFidelity)
        {
            Assert.Inconclusive("Generated an image whose default fidelity was less than or equal to our test case");
        }

        await using var inStream = new MemoryStream();
        await imageIn.SaveAsPngAsync(inStream);
        inStream.Position = 0;

        await viewModel.LoadFileStreamAsync(inStream);
        viewModel.TargetDistinctColours = targetFidelity;
        await viewModel.ProcessImageAsync();
        var imageOut = viewModel.ImageOut;

        int nOutputColors = imageAnalysisService.GetDistinctColors(imageOut).Count();
        Assert.IsTrue(nOutputColors <= targetFidelity);
    }
}
