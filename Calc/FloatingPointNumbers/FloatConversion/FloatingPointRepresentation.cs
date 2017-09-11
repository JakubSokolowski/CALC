using Calc.PositionalSystem;


namespace Calc.FloatingPointNumbers
{
    public abstract class FloatingPointRepresentation
    {
        protected string binaryString;
        protected BaseConverter bConverter = new BaseConverter();

        public abstract FloatingPointProperty SpecialProperty { get; protected set; }
        public abstract string BinaryString { get; protected set; }

        public abstract string Sign { get;  }
        public abstract string Exponent { get;  }
        public abstract string Mantissa { get;  }
       
        
        protected abstract int BinarStringLength { get; }
        protected abstract int ExponentLength { get; }
        protected abstract int MantissaLenght { get; }
        
        public abstract double ExponentEncoding { get; }
        public abstract double MantissaEncoding { get; }

        public abstract double ExponentValue { get; }
        public abstract double MantissaValue { get; }

    }
}
