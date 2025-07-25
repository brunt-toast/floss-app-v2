using FlossApp.Application.Services.ColorNaming;
using FlossApp.Application.Services.ColorNumbering;
using FlossApp.Application.Services.ColorProvider;
using FlossApp.Application.Services.Cookies;
using FlossApp.Application.Services.I18n;
using FlossApp.Application.Services.ImageAnalysis;
using FlossApp.Application.Services.ImageFiltering;
using FlossApp.Application.ViewModels.Images;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace FlossApp.Application.Mock;

public class MockServiceProvider : IServiceProvider
{
    private readonly IServiceProvider _services;

    public MockServiceProvider()
    {
        ServiceCollection builder = new();
        builder.AddSingleton<IColorProviderService, ColorProviderService>();
        builder.AddSingleton<IColorNamingService, ColorNamingService>();
        builder.AddSingleton<IColorNumberingService, ColorNumberingService>();
        builder.AddSingleton<IImageFilteringService, ImageFilteringService>();
        builder.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        builder.AddSingleton<IImageAnalysisService, ImageAnalysisService>();
        builder.AddSingleton<II18nService, I18nService>();
        builder.AddSingleton<ICookieService, MockCookieService>();

        builder.AddSingleton<IImageFilterViewModel, ImageFilterViewModel>();

        _services = builder.BuildServiceProvider();
    }

    public object? GetService(Type serviceType)
    {
        return _services.GetService(serviceType);
    }
}
