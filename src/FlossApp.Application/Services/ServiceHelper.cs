using FlossApp.Application.Services.ColorNaming;
using FlossApp.Application.Services.ColorNumbering;
using FlossApp.Application.Services.ColorProvider;
using FlossApp.Application.Services.I18n;
using FlossApp.Application.Services.ImageAnalysis;
using FlossApp.Application.Services.ImageFiltering;
using FlossApp.Application.Services.Snackbar;
using FlossApp.Application.Telemetry;
using FlossApp.Application.ViewModels.Colors;
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

        services.AddScoped<IFindSimilarColorsViewModel, FindSimilarColorsViewModel>();
        services.AddScoped<IImageFilterViewModel, ImageFilterViewModel>();
        services.AddScoped<IImageUpscalerViewModel, ImageUpscalerViewModel>();
        services.AddScoped<IRichColorPickerViewModel, RichColorPickerViewModel>();
        services.AddScoped<ICmykPickerViewModel, CmykPickerViewModel>();
        services.AddScoped<ICieLabPickerViewModel, CieLabPickerViewModel>();
        services.AddScoped<IHoopSizerViewModel, HoopSizerViewModel>();
        services.AddScoped<ILanguagePickerViewModel, LanguagePickerViewModel>();
        services.AddScoped<IAppInfoViewModel, AppInfoViewModel>();

        services.AddSingleton<II18nService, I18nService>(s =>
        {
            var ret = new I18nService(s);
            ret.InitAsync();
            return ret;
        });
        services.AddSingleton<ILoggerFactory, FlossAppLoggerFactory>();
        services.AddSingleton<IColorNamingService, ColorNamingService>();
        services.AddSingleton<IColorNumberingService, ColorNumberingService>();
        services.AddSingleton<IColorProviderService, ColorProviderService>();
        services.AddSingleton<IImageFilteringService, ImageFilteringService>();
        services.AddSingleton<IImageAnalysisService, ImageAnalysisService>();
        services.AddSingleton<ISnackbarService, SnackbarService>();

        return services;
    }
}
