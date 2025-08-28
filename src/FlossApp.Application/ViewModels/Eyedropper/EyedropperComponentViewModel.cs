using System.Drawing;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FlossApp.Application.Enums;
using FlossApp.Application.Extensions.System.Drawing;
using FlossApp.Application.Messages;
using FlossApp.Application.Models.RichColor;
using FlossApp.Application.Services.ColorMatching;
using FlossApp.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlossApp.Application.ViewModels.Eyedropper;

internal partial class EyedropperComponentViewModel : ObservableObject, IEyedropperComponentViewModel
{
    private readonly IColorMatchingService _colorMatchingService;
    private readonly IMessenger _messenger;
    private readonly ILogger _logger;

    [ObservableProperty] public partial ColorSchema TargetSchema { get; set; }
    [ObservableProperty] public partial ColorComparisonAlgorithms ComparisonAlgorithm { get; set; }

    public EyedropperComponentViewModel(IServiceProvider services)
    {
        _colorMatchingService = services.GetRequiredService<IColorMatchingService>();
        _messenger = services.GetRequiredService<IMessenger>();
        _logger = services.GetRequiredService<ILoggerFactory>().CreateLogger<EyedropperComponentViewModel>();
    }

    public async Task<RichColorModel> GetSimilar(System.Drawing.Color color)
    {
        var richColor = new RichColorModel(new RichColor
        {
            Red = color.R,
            Green = color.G,
            Blue = color.B,
            Name = "",
            Number = ""
        });

        var result = await _colorMatchingService.GetMostSimilarColorsAsync(richColor, TargetSchema, 1, ComparisonAlgorithm);
        return result.FirstOrDefault();
    }

    public void Select(Color col)
    {
        _logger.LogInformation("Send select {col}", col.AsHex());
        _messenger.Send(new ColorSelectedMessage(col), nameof(EyedropperChannel));
    }
}
