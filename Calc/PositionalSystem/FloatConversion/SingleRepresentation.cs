using System;

namespace Calc.PositionalSystem
{
    public class SingleRepresentation : FloatingPointRepresentation, IFloatingPointValidation
    {        
        private float decimalValue;
        private static FloatConverter fConverter = new FloatConverter();
     
       
        public override string Sign => binaryString.Substring(0,1); 
        public override string Exponent => binaryString.Substring(1, ExponentLength); 
        public override string Mantissa => binaryString.Substring(1 + ExponentLength, MantissaLenght); 

        public override string BinaryString
        {
            get => binaryString;
            set
            {
                if (IsBinaryString(value) && value.Length == BinarStringLength)
                {
                    binaryString = value;
                    decimalValue = fConverter.BinaryStringToSingle(value);
                }
                else
                    throw new ArgumentException("String must be binary and have " + BinarStringLength + " length");
            }
        }
        public float DecimalValue
        {
            get => decimalValue;
            set
            {
                decimalValue = value;
                binaryString = fConverter.SingleToBinaryString(value);
            }
        }


        protected override int ExponentLength { get; } = 8;
        protected override int MantissaLenght { get; } = 23;
        protected override int BinarStringLength { get; } = 32;

        public override double ExponentEncoding => bConverter.ArbitraryBaseToDecimal(Exponent, 2);
        public override double MantissaEncoding => NumberConverter.ToBase("0."+ Mantissa, 2, 10).DecimalValue;

        public override double ExponentValue => ExponentEncoding - 127;
        public override double MantissaValue => MantissaEncoding + 1;
        

        public SingleRepresentation(float f)
        {         
            DecimalValue = f;
        }
        public SingleRepresentation(string binStr)
        {
            BinaryString = binStr;
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
