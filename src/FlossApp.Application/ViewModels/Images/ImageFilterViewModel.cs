using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using FlossApp.Application.Utils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Application.ViewModels.Images;

public partial class ImageFilterViewModel : ViewModelBase, IImageFilterViewModel
{
    [ObservableProperty]
    public partial Image<Rgba32> ImageIn { get; private set; }
    [ObservableProperty]
    public partial Image<Rgba32> ImageOut { get; private set; }

    [ObservableProperty] public partial string ImageOutBase64 { get; private set; } = "";

    public float PixelRatio
    {
        get;
        set
        {
            SetProperty(ref field, value);
            OnPropertyChanged(nameof(TargetWidth));
            OnPropertyChanged(nameof(TargetHeight));
        }
    } = (float)0.01;

    public int TargetWidth
    {
        get => (int)Math.Floor(ImageIn.Width * PixelRatio);
        set
        {
            PixelRatio = value / ImageIn.Width;
        }
    }
    public int TargetHeight
    {
        get => (int)Math.Floor(ImageIn.Height * PixelRatio);
        set
        {
            PixelRatio = value / ImageIn.Height;
        }
    }

    public ImageFilterViewModel(IServiceProvider services) : base(services)
    {
        ImageIn = new Image<Rgba32>(1, 1, new Rgba32(255, 255, 255));
        ImageOut = new Image<Rgba32>(1, 1, new Rgba32(255, 255, 255));
    }

    public async Task LoadFileStreamAsync(Stream stream)
    {
        try
        {
            ImageIn = await Image.LoadAsync<Rgba32>(stream);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public async Task ProcessImageAsync()
    {
        try
        {
            var pixelated = ImagePixelator.Pixelate(ImageIn, PixelRatio);
            ImageOut = pixelated;
            await using var stream = new MemoryStream();
            await pixelated.SaveAsPngAsync(stream);
            stream.Position = 0;
            byte[] bytes = stream.ToArray();
            ImageOutBase64 = Convert.ToBase64String(bytes);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}

public interface IImageFilterViewModel
{
    public Task LoadFileStreamAsync(Stream stream);
    public Task ProcessImageAsync();

    public string ImageOutBase64 { get; }
    public float PixelRatio { get; set; }
    public Image<Rgba32> ImageIn { get; }
    public Image<Rgba32> ImageOut { get; }

    public int TargetWidth { get; set; }
    public int TargetHeight { get; set; }
}
