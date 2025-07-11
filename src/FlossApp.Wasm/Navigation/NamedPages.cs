using MudBlazor;

namespace FlossApp.Wasm.Navigation;

internal static class NamedPages
{
    public static IReadOnlyList<NamedPage> AllNamedPages { get; } =
    [
        new(Icons.Material.Filled.Compare, "Color Convert", "/FindSimilarColorsPage"),
        new(Icons.Material.Filled.FilterHdr, "Image Filtering", "/ImageFilter"),
        new(Icons.Material.Filled.LinearScale, "Upscaler", "/ImageUpscaler"),
        new(Icons.Material.Filled.FormatSize, "Hoop sizer", "/HoopSizer"),
    ];
}
