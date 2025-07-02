using System.ComponentModel.DataAnnotations;
using FlossApp.Application.Enums;
using FlossApp.Application.Utils.Equations;

namespace FlossApp.Application.ViewModels.Scaling;

public class HoopSizerViewModel : ViewModelBase, IHoopSizerViewModel
{
    private readonly EquationTriangle _equation = new();

    public int HoopDiameterInches
    {
        get => (int)Math.Ceiling(_equation.X);
        set
        {
            _equation.X = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(WidthOrHeightPx));
            OnPropertyChanged(nameof(ThreadCountPerInch));
        }
    }

    public int WidthOrHeightPx
    {
        get => (int)Math.Ceiling(_equation.Y);
        set
        {
            _equation.Y = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HoopDiameterInches));
            OnPropertyChanged(nameof(ThreadCountPerInch));
        }
    }

    public int ThreadCountPerInch
    {
        get => (int)Math.Ceiling(_equation.Z);
        set
        {
            _equation.Z = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HoopDiameterInches));
            OnPropertyChanged(nameof(WidthOrHeightPx));
        }
    }

    public HoopSizeCalculationMode Target
    {
        get => TrinomialTargetToHoopSizeCalculationMode(_equation.Target);
        set
        {
            _equation.Target = HoopSizeCalculationModeToTrinomialTarget(value);
            OnPropertyChanged();
            OnPropertyChanged(nameof(HoopDiameterInches));
            OnPropertyChanged(nameof(WidthOrHeightPx));
            OnPropertyChanged(nameof(ThreadCountPerInch));
        }
    }

    public HoopSizerViewModel(IServiceProvider services) : base(services)
    {
        WidthOrHeightPx = 100;
        ThreadCountPerInch = 10;
        Target = HoopSizeCalculationMode.HoopDiameterInches;
    }

    private HoopSizeCalculationMode TrinomialTargetToHoopSizeCalculationMode(TrinomialTarget target)
    {
        return target switch
        {
            TrinomialTarget.X => HoopSizeCalculationMode.HoopDiameterInches,
            TrinomialTarget.Y => HoopSizeCalculationMode.WidthOrHeightPx,
            TrinomialTarget.Z => HoopSizeCalculationMode.ThreadCountPerInch,
            _ => throw new ArgumentOutOfRangeException(nameof(target), target, null)
        };
    }

    private TrinomialTarget HoopSizeCalculationModeToTrinomialTarget(HoopSizeCalculationMode mode)
    {
        return mode switch
        {
            HoopSizeCalculationMode.WidthOrHeightPx => TrinomialTarget.Y,
            HoopSizeCalculationMode.ThreadCountPerInch => TrinomialTarget.Z,
            HoopSizeCalculationMode.HoopDiameterInches => TrinomialTarget.X,
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
    }
}

public interface IHoopSizerViewModel
{
    public int WidthOrHeightPx { get; set; }
    public int ThreadCountPerInch { get; set; }
    public int HoopDiameterInches { get; set; }
    public HoopSizeCalculationMode Target { get; set; }
}

