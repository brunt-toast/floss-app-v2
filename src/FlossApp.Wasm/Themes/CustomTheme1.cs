using MudBlazor;

namespace FlossApp.Wasm.Themes;

public class CustomTheme1 : MudTheme
{
    private static CustomTheme1? s_instance;
    public static CustomTheme1 Instance => s_instance ??= new();

    private CustomTheme1()
    {
        PaletteLight = new PaletteLight { Primary = Colors.LightGreen.Default };
    }
}
