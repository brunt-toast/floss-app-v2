using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using FlossApp.Application.Enums;
using FlossApp.Application.Services.ImageFiltering;
using FlossApp.Application.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Application.ViewModels.Images;

public partial class ImageFilterViewModel : ViewModelBase, IImageFilterViewModel
{
    private readonly ILogger _logger;
    private readonly IImageFilteringService _imageFilteringService;

    [ObservableProperty]
    public partial Image<Rgba32> ImageIn { get; private set; }
    [ObservableProperty]
    public partial Image<Rgba32> ImageOut { get; private set; }

    [ObservableProperty] public partial string ImageOutBase64 { get; private set; } = "";
    [ObservableProperty] public partial ColorSchema TargetSchema { get; set; } 

    public float PixelRatio
    {
        get;
        set
        {
            SetProperty(ref field, value);
            OnPropertyChanged(nameof(TargetWidth));
            OnPropertyChanged(nameof(TargetHeight));
        }
    } = (float)0.01;

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

    public ImageFilterViewModel(IServiceProvider services) : base(services)
    {
        _imageFilteringService = services.GetRequiredService<IImageFilteringService>();
        _logger = services.GetRequiredService<ILoggerFactory>().CreateLogger<ImageFilterViewModel>();

        ImageIn = new Image<Rgba32>(1, 1, new Rgba32(255, 255, 255));
        ImageOut = new Image<Rgba32>(1, 1, new Rgba32(255, 255, 255));
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
            var pixelated = _imageFilteringService.PixelateImage(ImageIn, PixelRatio);
            var colored = await _imageFilteringService.ReduceToSchemaColorsAsync(pixelated, TargetSchema);

            ImageOut = colored;
            await using var stream = new MemoryStream();
            await pixelated.SaveAsPngAsync(stream);
            stream.Position = 0;
            byte[] bytes = stream.ToArray();
            ImageOutBase64 = Convert.ToBase64String(bytes);
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
    public float PixelRatio { get; set; }
    public Image<Rgba32> ImageIn { get; }
    public Image<Rgba32> ImageOut { get; }

    public int TargetWidth { get; set; }
    public int TargetHeight { get; set; }
    public ColorSchema TargetSchema { get; set; }
}
