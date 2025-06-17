using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FlossApp.Application.Telemetry;

public class FlossAppLoggerFactory : ILoggerFactory
{
    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FlossAppLogger(categoryName);
    }

    public void AddProvider(ILoggerProvider provider)
    {
    }
}
