namespace FlossApp.Core;

public readonly record struct RichColor
{
    public byte Red { get; init; }
    public byte Green { get; init; }
    public byte Blue { get; init; }
    public string Name { get; init; }
    public string Number { get; init; }
}
