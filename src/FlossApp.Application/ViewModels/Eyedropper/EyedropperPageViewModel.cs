using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FlossApp.Application.Enums;
using FlossApp.Application.Extensions.FlossApp.Application.Enums;
using FlossApp.Application.Extensions.System.Drawing;
using FlossApp.Application.Messages;
using FlossApp.Application.Models.RichColor;
using FlossApp.Application.Services.ColorMatching;
using FlossApp.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Application.ViewModels.Eyedropper;

public partial class EyedropperPageViewModel : ObservableObject, IEyedropperPageViewModel
{
    private readonly ILogger<EyedropperPageViewModel> _logger;
    private readonly IMessenger _messenger;
    private readonly IColorMatchingService _colorMatchingService;

    [ObservableProperty] public partial ColorSchema TargetSchema { get; set; } 
    [ObservableProperty] public partial ColorComparisonAlgorithms ComparisonAlgorithm { get; set; }
    [ObservableProperty] public partial RichColorModel SelectedColor { get; private set; } 

    public Image<Rgba32> ImageIn { get; private set; } = new(1, 1);

    public EyedropperPageViewModel(IServiceProvider services)
    {
        TargetSchema = Enum.GetValues<ColorSchema>().First(ValidSchemaFilter);

        _logger = services.GetRequiredService<ILoggerFactory>().CreateLogger<EyedropperPageViewModel>();
        _messenger = services.GetRequiredService<IMessenger>();
        _colorMatchingService = services.GetRequiredService<IColorMatchingService>();
    }

    public void Init()
    {
        _messenger.Register<ColorSelectedMessage>(this, OnColorSelectedMessageReceived);
    }

    private async void OnColorSelectedMessageReceived(object recipient, ColorSelectedMessage message)
    {
        _logger.LogInformation("Color selected: {c}", message.Color.AsHex());
        try
        {
            var color = message.Color;
            var richColor = new RichColorModel(new RichColor
            {
                Red = color.R,
                Green = color.G,
                Blue = color.B,
                Name = "",
                Number = ""
            });

            var result = await _colorMatchingService.GetMostSimilarColorsAsync(richColor, TargetSchema, 1, ComparisonAlgorithm);
            SelectedColor = result.First();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected exception");
        }
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

    public bool ValidSchemaFilter(ColorSchema schema)
    {
        return !schema.IsRgbSuperset();
    }
}
