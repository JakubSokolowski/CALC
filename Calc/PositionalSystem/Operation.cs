using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.PositionalSystem
{
    struct Operation
    {
        enum OperationName
        {
            Addition,
            Substraction,
            Multiplication,
            Division,
            Inverse,
            SquareRoot,
        }

        OperationName name;
        List<Number> operands;
        Number result;
        
    }
}
