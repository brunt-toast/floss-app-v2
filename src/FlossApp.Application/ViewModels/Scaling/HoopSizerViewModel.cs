using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FlossApp.Application.ViewModels.Scaling;

public partial class HoopSizerViewModel : ViewModelBase, IHoopSizerViewModel
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HoopDiameterInches))]
    public partial int WidthOrHeightPx { get; set; } = 100;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HoopDiameterInches))]
    public partial int ThreadCountPerInch { get; set; } = 11;

    public int HoopDiameterInches => WidthOrHeightPx / ThreadCountPerInch;

    public HoopSizerViewModel(IServiceProvider services) : base(services)
    {
    }
}

public interface IHoopSizerViewModel
{
    public int WidthOrHeightPx { get; set; }
    public int ThreadCountPerInch { get; set; }
    public int HoopDiameterInches { get; }
}
