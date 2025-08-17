using Microsoft.Extensions.Logging;

namespace FlossApp.Application.Telemetry;

public class FlossAppLogger : ILogger
{
    private readonly string _categoryName;

    public FlossAppLogger(string categoryName)
    {
        _categoryName = categoryName;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        Console.WriteLine($"{DateTime.Now:O} {_categoryName} [{logLevel}] {formatter(state, exception)}");
        if (exception is not null)
        {
            Console.WriteLine($"{exception.Message}{Environment.NewLine}{exception.StackTrace}");
        }
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel > LogLevel.Debug;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return new NullDisposable();
    }

    private class NullDisposable : IDisposable
    {
        public void Dispose()
        {
        }
    }
}
