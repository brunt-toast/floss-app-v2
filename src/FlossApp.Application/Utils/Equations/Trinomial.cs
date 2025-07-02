using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Enums;

namespace FlossApp.Application.Utils.Equations;

internal class Trinomial<TX, TY, TZ>
{
    protected Func<TY, TZ, TX> CalcX;
    protected Func<TX, TZ, TY> CalcY;
    protected Func<TX, TY, TZ> CalcZ;

    public Trinomial(Func<TY, TZ, TX> calcX, Func<TX, TZ, TY> calcY, Func<TX, TY, TZ> calcZ)
    {
        CalcX = calcX;
        CalcY = calcY;
        CalcZ = calcZ;
    }

    protected Trinomial()
    {
    }

    public TrinomialTarget Target { get; set; }

    private bool SetProperty<T>(ref T property, T value)
    {
        if (EqualityComparer<T>.Default.Equals(property, value))
        {
            return false;
        }

        property = value;
        CalculateTarget();
        return true;
    }

    public TX X
    {
        get;
        set => SetProperty(ref field, value);
    }

    public TY Y
    {
        get;
        set => SetProperty(ref field, value);
    }

    public TZ Z
    {
        get;
        set => SetProperty(ref field, value);
    }

    private void CalculateTarget()
    {
        switch (Target)
        {
            case TrinomialTarget.X:
                X = CalcX(Y, Z);
                break;
            case TrinomialTarget.Y:
                Y = CalcY(X, Z);
                break;
            case TrinomialTarget.Z:
                Z = CalcZ(X, Y);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

}
