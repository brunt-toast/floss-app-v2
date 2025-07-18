using FlossApp.Application.Services.Cookies;
using Microsoft.JSInterop;

namespace FlossApp.Wasm.Services.Cookies;

public class CookieService : ICookieService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ILogger _logger;

    public CookieService(IServiceProvider services)
    {
        _jsRuntime = services.GetRequiredService<IJSRuntime>();
        _logger = services.GetRequiredService<ILoggerFactory>().CreateLogger<CookieService>();
    }

    public async Task SetCookieAsync(string name, string value, int days = 365)
    {
        _logger.LogInformation(@"Setting cookie ""{name}"" to ""{value}""", name, value);
        await _jsRuntime.InvokeVoidAsync("window.setCookie", name, value, days);
    }

    public async Task<string?> GetCookieAsync(string name)
    {
        string? value = await _jsRuntime.InvokeAsync<string>("window.getCookie", name);
        _logger.LogInformation(@"Getting cookie ""{name}"": ""{value}""", name, value);
        return value;
    }
}
