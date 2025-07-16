using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using FlossApp.Application.Data;
using FlossApp.Application.Models.RichColor;
using FlossApp.Application.Utils;
using FlossApp.Core;

namespace FlossApp.Application.ViewModels.Pickers;

public partial class CmykPickerViewModel : ViewModelBase, ICmykPickerViewModel
{
    [ObservableProperty] public partial double Cyan { get; set; }
    [ObservableProperty] public partial double Magenta { get; set; }
    [ObservableProperty] public partial double Yellow { get; set; }
    [ObservableProperty] public partial double Black { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Cyan))]
    [NotifyPropertyChangedFor(nameof(Magenta))]
    [NotifyPropertyChangedFor(nameof(Yellow))]
    [NotifyPropertyChangedFor(nameof(Black))]
    public partial RichColorModel RichColor { get; set; }

    public CmykPickerViewModel(IServiceProvider services) : base(services)
    {
        PropertyChanged += CmykPickerViewModel_OnPropertyChanged;
    }

    private void CmykPickerViewModel_OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(RichColor))
        {
            return;
        }

        UpdateRichColor();
    }

    private void UpdateRichColor()
    {
        var cmyk = new CmykColor { Cyan = Cyan, Magenta = Magenta, Yellow = Yellow, Black = Black };
        RichColor = new RichColorModel(cmyk.AsRichColor());
    }
}

public interface ICmykPickerViewModel
{
    public double Cyan { get; set; }
    public double Magenta { get; set; }
    public double Yellow { get; set; }
    public double Black { get; set; }

    public RichColorModel RichColor { get; set; }
}
