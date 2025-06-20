using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Services.ColorNaming;
using FlossApp.Application.Services.ColorNumbering;
using FlossApp.Application.Services.ColorProvider;
using FlossApp.Application.Services.ImageAnalysis;
using FlossApp.Application.Services.ImageFiltering;
using FlossApp.Application.Services.Snackbar;
using FlossApp.Application.Telemetry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlossApp.Application.Services;

public static class ServiceHelper
{
    public static IServiceCollection GetInternalServiceDescriptors()
    {
        var services = new ServiceCollection();

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
