using Microsoft.AspNetCore.Components.Forms;

namespace FlossApp.Wasm.Pages;

public partial class ImageFilterPage
{
    private async Task UploadFiles(IBrowserFile file)
    {
        using var _ = ViewModel.SetBusy();

        try
        {
            await using var stream = file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 1024);
            await ViewModel.LoadFileStreamAsync(stream);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
