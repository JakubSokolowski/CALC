using Calc.PositionalSystem;

namespace Calc.FloatingPointNumbers
{
    public class DoubleRepresentation : FloatingPointRepresentation, IFloatingPointValidation
    {       
        private double decimalValue;

        protected override int BinarStringLength => 64;
        protected override int ExponentLength => 11;
        protected override int MantissaLenght => 52;

        public override string Sign { get => binaryString.Substring(0, 1); }
        public override string Exponent { get => binaryString.Substring(1, ExponentLength); }
        public override string Mantissa { get => binaryString.Substring(1 + ExponentLength, MantissaLenght); }

        public override string BinaryString
        {
            get => binaryString;
            protected set => binaryString = value;
           
        }
        public double DecimalValue
        {
            get => decimalValue;
            private set => decimalValue = value;            
        }       
     
       

        public override double ExponentEncoding => bConverter.ArbitraryBaseToDecimal(Exponent, 2);
        public override double MantissaEncoding => NumberConverter.ToBase("1." + Mantissa, 2, 10).DecimalValue;
        public override double ExponentValue => ExponentEncoding - 1023;
        public override double MantissaValue => MantissaEncoding - 1;

        public override FloatingPointProperty SpecialProperty { get; protected set; }
        public DoubleRepresentation(double d, string binaryStr, FloatingPointProperty property)
        {
            DecimalValue = d;
            BinaryString = binaryStr;
            SpecialProperty = property;
        }   

        public bool IsBinaryString(string str)
        {
            foreach (var ch in str)
            {
                if (ch == '0' || ch == '1')
                    continue;
                else
                    return false;
            }
            return true;
        }
    }
}
