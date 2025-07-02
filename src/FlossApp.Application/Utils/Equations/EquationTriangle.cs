using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlossApp.Application.Utils.Equations;

internal class EquationTriangle : Trinomial<float, float, float>
{
    public EquationTriangle()
    {
        CalcX = (y, z) => y / z;
        CalcY = (x, z) => x * z;
        CalcZ = (x, y) => y / x;
    }
}

