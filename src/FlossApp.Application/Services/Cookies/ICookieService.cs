using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlossApp.Application.Services.Cookies;

public interface ICookieService
{
    public Task SetCookieAsync(string name, string value, int days = 365);
    public Task<string?> GetCookieAsync(string name);
}
