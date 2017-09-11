using Calc.PositionalSystem;
using System;

namespace Calc.FloatingPointNumbers
{
    public class FloatConverter
    {
        private static BaseConverter bConv = new BaseConverter();
      
        public SingleRepresentation ToSingle(float f)
        {
            string binaryString = SingleToBinaryString(f);
            var property = AssignProperty(f);
            return new SingleRepresentation(f, binaryString, property);
        }
        public SingleRepresentation ToSingle(string binStr)
        { 
            float decimalValue = BinaryStringToSingle(binStr);
            var property = AssignProperty(decimalValue);
            return new SingleRepresentation(decimalValue, binStr, property);
        }
        public DoubleRepresentation ToDouble(double d)
        {
            string binaryString = DoubleToBinaryString(d);
            var property = AssignProperty(d);
            return new DoubleRepresentation(d, binaryString, property);
        }
        public DoubleRepresentation ToDouble(string binStr)
        {
            double decimalValue = BinaryStringToDouble(binStr);
            var property = AssignProperty(decimalValue);
            return new DoubleRepresentation(decimalValue, binStr, property);
        }

        public string DoubleToBinaryString(double d)
        {
            return Convert.ToString(BitConverter.DoubleToInt64Bits(d), 2);
        }
        public double BinaryStringToDouble(string str)
        {
            return BitConverter.Int64BitsToDouble(Convert.ToInt64(str, 2));
        }      

        public string SingleToBinaryString(float f)
        {
            byte[] b = BitConverter.GetBytes(f);
            int i = BitConverter.ToInt32(b, 0);
            string result = Convert.ToString(i, 2);

            string prefix = String.Empty;
            if (result.Length < 32)
            {
                prefix = new String('0', 32 - result.Length);
            }
              
            return prefix + result;
        }
        public float BinaryStringToSingle(string str)
        {
            int i = Convert.ToInt32(str, 2);
            byte[] b = BitConverter.GetBytes(i);
            return BitConverter.ToSingle(b, 0);
        }

        public string GetExponentFromValue(int exponent)
        {
            // Shift the value by a bias of 127
            exponent += 127;
            return GetExponentFromEncoding(exponent);
        }
        public string GetExponentFromEncoding(int exponent)
        {
            if (IsInRange(exponent,0,255))
            {
                var binaryExponent = bConv.DecimalIntegerToArbitraryBase(exponent, 2);
                binaryExponent = bConv.AddZerosToTheLeft(binaryExponent, 8);
                return binaryExponent;
            }
            throw new ArgumentException("The decimal value of exponent must be between 0 and 255");
        }      
       
 
        public FloatingPointProperty AssignProperty(float number)
        {
            if (float.IsPositiveInfinity(number))
                return FloatingPointProperty.PositiveInfinity;
            if (float.IsNegativeInfinity(number))
                return FloatingPointProperty.NegativeInfinity;
            if (float.IsNaN(number))
                return FloatingPointProperty.NaN;
            if (IsDenormalized(number))
                return FloatingPointProperty.Denormalized;

            return FloatingPointProperty.Normalized;
        }
        public FloatingPointProperty AssignProperty(double number)
        {
            if (double.IsPositiveInfinity(number))
                return FloatingPointProperty.PositiveInfinity;
            if (double.IsNegativeInfinity(number))
                return FloatingPointProperty.NegativeInfinity;
            if (double.IsNaN(number))
                return FloatingPointProperty.NaN;
            if (IsDenormalized(number))
                return FloatingPointProperty.Denormalized;

            return FloatingPointProperty.Normalized;
        }

        public bool IsDenormalized(float number)
        {
            if (number == 0f)
            {
                return false;
            }
            // Get the bits
            byte[] buffer = BitConverter.GetBytes(number);
            int bits = BitConverter.ToInt32(buffer, 0);
            // extract the exponent, 8 bits in the upper registers,
            // above the 23 bit significand
            int exponent = (bits >> 23) & 0xff;
            // check and see if anything is there
            return exponent == 0;
        }
        public bool IsDenormalized(double number)
        {
            long bits = BitConverter.DoubleToInt64Bits(number);
            // Note that the shift is sign-extended, hence the test against -1 not 1
            bool negative = (bits < 0);
            int exponent = (int)((bits >> 52) & 0x7ffL);           
            return exponent == 0;
        }
        public bool IsInRange(int value, int lowerLimit, int upperLimit)
        {
            return value >= lowerLimit && value <= upperLimit;
        }
    }
}
