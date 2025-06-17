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
    }

    public bool IsEnabled(LogLevel logLevel) => true;

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
