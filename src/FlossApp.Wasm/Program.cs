using FlossApp.Application.Services;
using FlossApp.Application.Services.ColorNaming;
using FlossApp.Application.Services.ColorNumbering;
using FlossApp.Application.Services.ColorProvider;
using FlossApp.Application.Services.Cookies;
using FlossApp.Application.Services.ImageAnalysis;
using FlossApp.Application.Services.ImageFiltering;
using FlossApp.Application.Services.Snackbar;
using FlossApp.Application.Telemetry;
using FlossApp.Application.ViewModels.Colors;
using FlossApp.Application.ViewModels.Images;
using FlossApp.Application.ViewModels.Pickers;
using FlossApp.Application.ViewModels.Scaling;
using FlossApp.Wasm;
using FlossApp.Wasm.Services.Cookies;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddSingleton<ICookieService, CookieService>();

IServiceCollection sc = ServiceHelper.GetInternalServiceDescriptors();
foreach (var s in sc)
{
    builder.Services.Add(s);
}


await builder.Build().RunAsync();
