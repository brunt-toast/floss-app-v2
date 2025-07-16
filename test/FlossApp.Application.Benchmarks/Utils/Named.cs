namespace FlossApp.Application.Benchmarks.Utils;

/// <summary>
///     Helper class to give a display name in column headers
/// </summary>
public class Named<T>
{
    private readonly string _name;
    public T Value { get; }

    public Named(object? name, T value)
    {
        _name = name?.ToString() ?? string.Empty;
        Value = value;
    }

    public override string ToString() => _name;
}
