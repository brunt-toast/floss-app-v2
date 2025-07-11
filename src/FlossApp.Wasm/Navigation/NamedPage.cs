namespace FlossApp.Wasm.Navigation;

internal class NamedPage
{
    public string Icon { get; }
    public string Name { get; }
    public string Href { get; }

    public NamedPage(string icon, string name, string href)
    {
        Icon = icon;
        Name = name;
        Href = href;
    }
}
