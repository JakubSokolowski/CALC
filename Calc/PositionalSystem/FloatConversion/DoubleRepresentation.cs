using System;

namespace Calc.PositionalSystem
{
    public class DoubleRepresentation : FloatingPointRepresentation, IFloatingPointValidation
    {
        #region Private Members

        private double decimalValue;
        private static FloatConverter fConverter = new FloatConverter();

        #endregion

        #region Public Properties

        public override string Sign { get => binaryString.Substring(0, 1); }
        public override string Exponent { get => binaryString.Substring(1, ExponentLength); }
        public override string Mantissa { get => binaryString.Substring(1 + ExponentLength, MantissaLenght); }

        public override string BinaryString
        {
            get => binaryString;
            set
            {
                if (IsBinaryString(value) && value.Length == BinarStringLength)
                {
                    binaryString = value;
                    decimalValue = fConverter.BinaryStringToDouble(value);
                }
                else
                    throw new ArgumentException("String must be binary and have " + BinarStringLength + " length");
            }
        }
        public double DecimalValue
        {
            get => decimalValue;
            set
            {
                decimalValue = value;
                binaryString = fConverter.DoubleToBinaryString(value);
            }
        }

        #endregion

        #region IEEE 754 Lenghts

        protected override int BinarStringLength => 64;

        protected override int ExponentLength => 11;

        protected override int MantissaLenght => 52;

        #endregion


        #region Constructors

        public DoubleRepresentation(double d)
        {
            DecimalValue = d;
        }
        public DoubleRepresentation(string binStr)
        {
            BinaryString = binStr;
        }

        #endregion

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
