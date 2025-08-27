using Microsoft.AspNetCore.Components.Forms;

namespace FlossApp.Wasm.Pages;

public partial class EyedropperPage
{
    private async Task UploadFiles(IBrowserFile? file)
    {
        if (file is null)
        {
            return;
        }

        try
        {
            await using var stream = file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 1024);
            await PageViewModel.LoadFileStreamAsync(stream);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
