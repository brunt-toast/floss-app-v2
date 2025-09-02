using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using FlossApp.Application.Enums;
using FlossApp.Application.Services.ImageFiltering;
using FlossApp.Application.Services.Snackbar;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Application.ViewModels.Images;

public partial class ImageUpscalerViewModel : ViewModelBase, IImageUpscalerViewModel
{
    private readonly IImageFilteringService _imageFilteringService;
    private readonly ISnackbarService _snackbarService;

    private Image<Rgba32>? _imageIn;

    public ImageUpscalerViewModel(IServiceProvider services) : base(services)
    {
        _imageFilteringService = services.GetRequiredService<IImageFilteringService>();
        _snackbarService = services.GetRequiredService<ISnackbarService>();

        ImageOutBase64 = "";
        ScaleFactor = 2;
    }

    public int ImageInWidth => _imageIn?.Width ?? 0;
    public int ImageInHeight => _imageIn?.Height ?? 0;
    public int TargetWidth => ScaleFactor * _imageIn?.Width ?? 0;
    public int TargetHeight => ScaleFactor * _imageIn?.Height ?? 0;

    [ObservableProperty] public partial string ImageOutBase64 { get; private set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TargetWidth))]
    [NotifyPropertyChangedFor(nameof(TargetHeight))]
    public partial int ScaleFactor { get; set; }

    public async Task LoadFileStreamAsync(Stream stream)
    {
        _imageIn = await Image.LoadAsync<Rgba32>(stream);
        OnPropertyChanged(nameof(ImageInWidth));
        OnPropertyChanged(nameof(ImageInHeight));
    }

    public async Task ProcessImageAsync()
    {
        if (_imageIn is null)
        {
            _snackbarService.ShowSnackbar("Select an image", SnackbarSeverity.Error);
            return;
        }

        var imageOut = _imageFilteringService.Upscale(_imageIn, ScaleFactor);

        await using var stream = new MemoryStream();
        await imageOut.SaveAsPngAsync(stream);
        stream.Position = 0;
        byte[] bytes = stream.ToArray();
        ImageOutBase64 = Convert.ToBase64String(bytes);
    }
}

public interface IImageUpscalerViewModel
{
    public int ImageInWidth { get; }
    public int ImageInHeight { get; }

    public int TargetWidth { get; }
    public int TargetHeight { get; }

    public string ImageOutBase64 { get; }

    public int ScaleFactor { get; set; }

    public Task LoadFileStreamAsync(Stream stream);
    public Task ProcessImageAsync();
}
