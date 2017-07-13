using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.PositionalSystem
{
    public interface IBaseConversion
    {
        String DecimalIntegerToArbitraryBase(long decimalNumber, int radix);
        String DecimalFractionToArbitraryBase(double fraction, int radix);

        long ArbitraryBaseToDecimal(String valueString, int radix);
        double ArbitraryFractionToDecimal(String valueString, int radix);

    }
}
