using CommunityToolkit.Mvvm.Messaging;
using FlossApp.Application.Services.ColorMatching;
using FlossApp.Application.Services.ColorNaming;
using FlossApp.Application.Services.ColorNumbering;
using FlossApp.Application.Services.ColorProvider;
using FlossApp.Application.Services.I18n;
using FlossApp.Application.Services.ImageAnalysis;
using FlossApp.Application.Services.ImageFiltering;
using FlossApp.Application.Services.Snackbar;
using FlossApp.Application.Telemetry;
using FlossApp.Application.ViewModels.Colors;
using FlossApp.Application.ViewModels.Eyedropper;
using FlossApp.Application.ViewModels.Images;
using FlossApp.Application.ViewModels.Info;
using FlossApp.Application.ViewModels.Pickers;
using FlossApp.Application.ViewModels.Scaling;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlossApp.Application.Services;

public static class ServiceHelper
{
    public static IServiceCollection GetInternalServiceDescriptors()
    {
        var services = new ServiceCollection();

        services.AddTransient<IFindSimilarColorsViewModel, FindSimilarColorsViewModel>();
        services.AddTransient<IImageFilterViewModel, ImageFilterViewModel>();
        services.AddTransient<IImageUpscalerViewModel, ImageUpscalerViewModel>();
        services.AddTransient<IRichColorPickerViewModel, RichColorPickerViewModel>();
        services.AddTransient<ICmykPickerViewModel, CmykPickerViewModel>();
        services.AddTransient<ICieLabPickerViewModel, CieLabPickerViewModel>();
        services.AddTransient<IHoopSizerViewModel, HoopSizerViewModel>();
        services.AddTransient<ILanguagePickerViewModel, LanguagePickerViewModel>();
        services.AddTransient<IAppInfoViewModel, AppInfoViewModel>();
        services.AddTransient<IEyedropperPageViewModel, EyedropperPageViewModel>();
        services.AddTransient<IEyedropperComponentViewModel, EyedropperComponentViewModel>();

        services.AddScoped<IMessenger, WeakReferenceMessenger>();

        services.AddSingleton<II18nService, I18nService>(s =>
        {
            var ret = new I18nService(s);
            _ = ret.InitAsync();
            return ret;
        });
        services.AddSingleton<ILoggerFactory, FlossAppLoggerFactory>();
        services.AddSingleton<IColorNamingService, ColorNamingService>();
        services.AddSingleton<IColorNumberingService, ColorNumberingService>();
        services.AddSingleton<IColorProviderService, ColorProviderService>();
        services.AddSingleton<IImageFilteringService, ImageFilteringService>();
        services.AddSingleton<IImageAnalysisService, ImageAnalysisService>();
        services.AddSingleton<ISnackbarService, SnackbarService>();
        services.AddSingleton<IColorMatchingService, ColorMatchingService>();

        return services;
    }
}
