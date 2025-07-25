using FlossApp.Application.Enums;
using FlossApp.Application.Services.Cookies;

namespace FlossApp.Application.Mock;

internal class MockCookieService : ICookieService
{
    private readonly Dictionary<NamedCookies, string> _data = [];

    public Task SetCookieAsync(NamedCookies name, string value, int days = 365)
    {
        _data[name] = value;
        return Task.CompletedTask;
    }

    public Task<string?> GetCookieAsync(NamedCookies name)
    {
        return (_data.TryGetValue(name, out string? val)
            ? Task.FromResult(val)!
            : Task.FromResult<string?>(null)!)!;
    }
}
