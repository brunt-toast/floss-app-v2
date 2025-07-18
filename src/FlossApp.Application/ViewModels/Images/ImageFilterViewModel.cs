﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using FlossApp.Application.Data;
using FlossApp.Application.Enums;
using FlossApp.Application.Extensions.System.Collections.ObjectModel;
using FlossApp.Application.Models.RichColor;
using FlossApp.Application.Services.ImageAnalysis;
using FlossApp.Application.Services.ImageFiltering;
using FlossApp.Application.Utils;
using FlossApp.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Application.ViewModels.Images;

public partial class ImageFilterViewModel : ViewModelBase, IImageFilterViewModel
{
    private readonly ILogger _logger;
    private readonly IImageFilteringService _imageFilteringService;
    private readonly IImageAnalysisService _imageAnalysisService;

    [ObservableProperty]
    public partial Image<Rgba32> ImageIn { get; private set; }
    [ObservableProperty]
    public partial Image<Rgba32> ImageOut { get; private set; }

    [ObservableProperty] public partial ColorComparisonAlgorithms ComparisonAlgorithm { get; set; }
    [ObservableProperty] public partial string ImageOutBase64 { get; private set; } = "";
    [ObservableProperty] public partial ImageSharpKnownDitherings? DitherKind { get; set; }
    [ObservableProperty] public partial ImageSharpKnownResamplers ResamplerKind { get; set; }
    [ObservableProperty] public partial ColorSchema TargetSchema { get; set; }
    [ObservableProperty] public partial byte TransparencyThreshold { get; set; }
    [ObservableProperty] public partial IDictionary<RichColorModel, int> Palette { get; private set; } = new Dictionary<RichColorModel, int>();

    [NotifyPropertyChangedFor(nameof(TargetHeight))]
    [NotifyPropertyChangedFor(nameof(TargetWidth))]
    [ObservableProperty]
    public partial float PixelRatio { get; set; } = (float)0.01;

    public int TargetWidth
    {
        get => (int)Math.Floor(ImageIn.Width * PixelRatio);
        set
        {
            PixelRatio = (float)value / ImageIn.Width;
        }
    }
    public int TargetHeight
    {
        get => (int)Math.Floor(ImageIn.Height * PixelRatio);
        set
        {
            PixelRatio = (float)value / ImageIn.Height;
        }
    }

    [ObservableProperty] public partial int TargetDistinctColours { get; set; } = 100;

    public ImageFilterViewModel(IServiceProvider services) : base(services)
    {
        _imageFilteringService = services.GetRequiredService<IImageFilteringService>();
        _imageAnalysisService = services.GetRequiredService<IImageAnalysisService>();
        _logger = services.GetRequiredService<ILoggerFactory>().CreateLogger<ImageFilterViewModel>();

        ImageIn = new Image<Rgba32>(1, 1, new Rgba32(255, 255, 255));
        ImageOut = new Image<Rgba32>(1, 1, new Rgba32(255, 255, 255));
        ResamplerKind = ImageSharpKnownResamplers.NearestNeighbor;
    }

    public async Task LoadFileStreamAsync(Stream stream)
    {
        try
        {
            ImageIn = await Image.LoadAsync<Rgba32>(stream);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected exception while loading a file stream");
        }
    }

    public async Task ProcessImageAsync()
    {
        try
        {
            var pixelated = _imageFilteringService.PixelateImage(ImageIn, PixelRatio, ResamplerKind);
            var reduced = _imageFilteringService.ReduceColors(pixelated, TargetDistinctColours, DitherKind);
            var colored = await _imageFilteringService.ReduceToSchemaColorsAsync(reduced, TargetSchema, TransparencyThreshold, ComparisonAlgorithm);

            ImageOut = colored;
            await using var stream = new MemoryStream();
            await colored.SaveAsPngAsync(stream);
            stream.Position = 0;
            byte[] bytes = stream.ToArray();
            ImageOutBase64 = Convert.ToBase64String(bytes);

            Palette = await _imageAnalysisService.GetPaletteAsync(ImageOut, TargetSchema);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected exception while processing an image");
        }
    }
}

public interface IImageFilterViewModel
{
    public Task LoadFileStreamAsync(Stream stream);
    public Task ProcessImageAsync();

    public string ImageOutBase64 { get; }
    public Image<Rgba32> ImageIn { get; }
    public Image<Rgba32> ImageOut { get; }
    public IDictionary<RichColorModel, int> Palette { get; }

    public int TargetDistinctColours { get; set; }

    public ColorComparisonAlgorithms ComparisonAlgorithm { get; set; }
    public float PixelRatio { get; set; }
    public int TargetWidth { get; set; }
    public int TargetHeight { get; set; }
    public ColorSchema TargetSchema { get; set; }
    public ImageSharpKnownResamplers ResamplerKind { get; set; }
    public ImageSharpKnownDitherings? DitherKind { get; set; }
    public byte TransparencyThreshold { get; set; }
}
