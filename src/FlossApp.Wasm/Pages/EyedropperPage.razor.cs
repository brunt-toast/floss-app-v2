using System.ComponentModel;
using Microsoft.AspNetCore.Components.Forms;

namespace FlossApp.Wasm.Pages;

public partial class EyedropperPage
{
    protected override void OnInitialized()
    {
        PageViewModel.Init();
        base.OnInitialized();

        PageViewModel.PropertyChanged += OnPageViewModelOnPropertyChanged;
    }

    private void OnPageViewModelOnPropertyChanged(object? _, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PageViewModel.SelectedColor))
        {
            StateHasChanged();
        }
    }

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
