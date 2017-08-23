using System;

namespace Calc.PositionalSystem
{
    public class FloatConverter
    {
        #region Double Conversion

        public SingleRepresentation ToSingle(float f)
        {
            return new SingleRepresentation(f);
        }

        public SingleRepresentation ToSingle(string binStr)
        {
            return new SingleRepresentation(binStr);
        }

        public DoubleRepresentation ToDouble(double d)
        {
            return new DoubleRepresentation(d);
        }

        public DoubleRepresentation ToDouble(string binStr)
        {
            return new DoubleRepresentation(binStr);
        }

        public string DoubleToBinaryString(double d)
        {
            return Convert.ToString(BitConverter.DoubleToInt64Bits(d), 2);
        }
        public double BinaryStringToDouble(string str)
        {
            return BitConverter.Int64BitsToDouble(Convert.ToInt64(str, 2));
        }

        #endregion

        #region Single Conversion

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

        #endregion

       
    }
}
