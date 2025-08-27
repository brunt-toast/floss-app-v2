using CommunityToolkit.Mvvm.ComponentModel;
using FlossApp.Application.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Application.ViewModels.Eyedropper;

public partial class EyedropperPageViewModel : ObservableObject, IEyedropperPageViewModel
{
    private readonly ILogger<EyedropperPageViewModel> _logger;

    [ObservableProperty] public partial ColorSchema TargetSchema { get; set; }
    [ObservableProperty] public partial ColorComparisonAlgorithms ComparisonAlgorithm { get; set; }

    public Image<Rgba32> ImageIn { get; private set; } = new(1,1);

    public EyedropperPageViewModel(IServiceProvider services)
    {
        _logger = services.GetRequiredService<ILoggerFactory>().CreateLogger<EyedropperPageViewModel>();
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
}
