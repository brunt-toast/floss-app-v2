using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using FlossApp.Application.Data;
using FlossApp.Application.Models.RichColor;
using FlossApp.Core;

namespace FlossApp.Application.ViewModels.Pickers;
public partial class CieLabPickerViewModel : ViewModelBase, ICieLabPickerViewModel
{
    [ObservableProperty] public partial double L { get; set; }
    [ObservableProperty] public partial double A { get; set; }
    [ObservableProperty] public partial double B { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(L))]
    [NotifyPropertyChangedFor(nameof(A))]
    [NotifyPropertyChangedFor(nameof(B))]
    public partial RichColorModel RichColor { get; set; }

    public CieLabPickerViewModel(IServiceProvider services) : base(services)
    {
        PropertyChanged += CieLabPickerViewModel_OnPropertyChanged;
    }

    private void CieLabPickerViewModel_OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(RichColor))
        {
            return;
        }

        UpdateRichColor();
    }

    private void UpdateRichColor()
    {
        (byte red, byte green, byte blue) = GetRgb();
        RichColor = new RichColorModel(new RichColor
        {
            Red = red,
            Green = green,
            Blue = blue
        });
    }

    private (byte R, byte G, byte B) GetRgb()
    {
        const double refX = 0.95047;
        const double refY = 1.00000;
        const double refZ = 1.08883;

        double fy = (L + 16.0) / 116.0;
        double fx = A / 500.0 + fy;
        double fz = fy - B / 200.0;

        static double PivotInverse(double t) => Math.Pow(t, 3) > 0.008856 ? Math.Pow(t, 3) : (t - 16.0 / 116.0) / 7.787;

        double x = refX * PivotInverse(fx);
        double y = refY * PivotInverse(fy);
        double z = refZ * PivotInverse(fz);

        double r = x *  3.2406 + y * -1.5372 + z * -0.4986;
        double g = x * -0.9689 + y *  1.8758 + z *  0.0415;
        double bl = x *  0.0557 + y * -0.2040 + z *  1.0570;

        r = GammaCorrect(r);
        g = GammaCorrect(g);
        bl = GammaCorrect(bl);

        byte red = (byte)Math.Round(Math.Clamp(r, 0, 1) * 255);
        byte green = (byte)Math.Round(Math.Clamp(g, 0, 1) * 255);
        byte blue = (byte)Math.Round(Math.Clamp(bl, 0, 1) * 255);

        return (red, green, blue);

        static double GammaCorrect(double c) =>
            c <= 0.0031308 ? 12.92 * c : 1.055 * Math.Pow(c, 1.0 / 2.4) - 0.055;
    }
}

public interface ICieLabPickerViewModel
{
    public double L { get; set; }
    public double A { get; set; }
    public double B { get; set; }
    public RichColorModel RichColor { get; set; }
}
