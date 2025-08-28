using System.ComponentModel;
using Microsoft.AspNetCore.Components.Forms;

namespace FlossApp.Wasm.Pages;

public partial class EyedropperPage
{
    protected override void OnInitialized()
    {
        ViewModel.Init();
        base.OnInitialized();

        ViewModel.PropertyChanged += OnPageViewModelOnPropertyChanged;
    }

    private void OnPageViewModelOnPropertyChanged(object? _, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ViewModel.SelectedColor))
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
            await ViewModel.LoadFileStreamAsync(stream);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
