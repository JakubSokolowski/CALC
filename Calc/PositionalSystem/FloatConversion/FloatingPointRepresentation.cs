using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.PositionalSystem
{
    public abstract class FloatingPointRepresentation
    {
        protected string binaryString;
        
        public abstract string Sign { get;  }
        public abstract string Exponent { get;  }
        public abstract string Mantissa { get;  }

        public abstract string BinaryString { get; set; }
        
        protected abstract int BinarStringLength { get; }
        protected abstract int ExponentLength { get;}
        protected abstract int MantissaLenght { get;}

    }
}
