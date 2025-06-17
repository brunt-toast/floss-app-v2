using FlossApp.Application.Services.ColorNaming;
using FlossApp.Application.Services.ColorNumbering;
using FlossApp.Application.Services.ColorProvider;
using FlossApp.Application.ViewModels.Colors;
using FlossApp.Application.ViewModels.Images;
using FlossApp.Wasm;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddSingleton<IColorNamingService, ColorNamingService>();
builder.Services.AddSingleton<IColorNumberingService, ColorNumberingService>();
builder.Services.AddSingleton<IColorProviderService, ColorProviderService>();

builder.Services.AddScoped<IFindSimilarColorsViewModel, FindSimilarColorsViewModel>();
builder.Services.AddScoped<IImageFilterViewModel, ImageFilterViewModel>();

await builder.Build().RunAsync();

// DMC seanockert
// HTML jennyknuth
// Copic meodai
