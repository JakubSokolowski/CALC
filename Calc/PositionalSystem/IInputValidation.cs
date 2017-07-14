using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.PositionalSystem
{
    interface IInputValidation
    {
        bool IsValidRadix(int radix);
        bool IsValidString(String str, int radix);
    }
}
