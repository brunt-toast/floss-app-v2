using FlossApp.Application.Services.ColorNaming;
using FlossApp.Application.Services.ColorNumbering;
using FlossApp.Application.Services.ColorProvider;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace FlossApp.Application.Tests.Mock;

internal class MockServiceProvider : IServiceProvider
{
    private readonly IServiceProvider _services;

    public MockServiceProvider()
    {
        ServiceCollection builder = new();
        builder.AddSingleton<IColorProviderService, ColorProviderService>();
        builder.AddSingleton<IColorNamingService, ColorNamingService>();
        builder.AddSingleton<IColorNumberingService, ColorNumberingService>();
        _services = builder.BuildServiceProvider();
    }

    public object? GetService(Type serviceType)
    {
        return _services.GetService(serviceType);
    }
}
