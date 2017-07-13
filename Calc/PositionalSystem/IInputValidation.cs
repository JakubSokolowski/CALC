using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.PositionalSystem
{
    interface IInputValidation
    {
        bool isValidRadix(int radix);
        bool isValidString(String str, int radix);
    }
}
