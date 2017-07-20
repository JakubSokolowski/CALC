using System;

namespace Calc.PositionalSystem
{
    public class Number : ICloneable
    {       

        #region Private Members
        private int radix;
        private long integerPartDecimalValue;
        private double fractionalPartDecimalValue;

        private String integerPartValueString;
        private String fractionalPartValueString;
        #endregion

        #region Constructors
        public Number(int radix, double decimalValue, string baseSystemValueStr)
        {
            Radix = radix;
            DecimalValue = decimalValue;
            BaseValueString = baseSystemValueStr;
        }
        public Number()
        {
            Radix = 0;
            DecimalValue = 0.0;
            BaseValueString = "0.0";
        }
        #endregion

        # region Fields
        public int Radix { get { return radix; } private set { radix = value; } }
        public long IntegerPartDecimalValue { get { return integerPartDecimalValue; } }
        public double FractionalPartDecimalvalue { get { return fractionalPartDecimalValue; } }
        public double DecimalValue
        {
            get { return (double)integerPartDecimalValue + fractionalPartDecimalValue; }
            private set
            {
                fractionalPartDecimalValue = value % 1;
                integerPartDecimalValue = (long)( value - fractionalPartDecimalValue );

            }
            
        }
        public String IntegerPartValueString { get { return integerPartValueString; } private set { integerPartValueString = value; } }
        public String FractionalPartValueString { get { return fractionalPartValueString; } private set { fractionalPartValueString = value; } }
        public String BaseValueString
        {
            get { return integerPartValueString + '.' + fractionalPartValueString; }
            private set
            {
                if(value.Contains("."))
                {
                    string[] parts = value.Split('.');
                    integerPartValueString = parts[0];
                    fractionalPartValueString = parts[1];
                }
                else
                {
                    IntegerPartValueString = value;
                    fractionalPartValueString = "0";
                }               
            }
        }
        #endregion Fields

        #region Math Operations       
        public static Number operator + (Number left, Number right)
        {
            double result = checked(left.DecimalValue + right.DecimalValue);
            return BaseConverter.ConvertToBase(result, left.Radix);
        }
        public static Number operator - (Number left, Number right)
        {
            double result = checked(left.DecimalValue - right.DecimalValue);
            return BaseConverter.ConvertToBase(result, left.Radix);
        }
        public static Number operator * (Number left, Number right)
        {
            double result = checked(left.DecimalValue * right.DecimalValue);
            return BaseConverter.ConvertToBase(result, left.Radix);
        }
        public static Number operator / (Number left, Number right)
        {
            double result = checked(left.DecimalValue / right.DecimalValue);
            return BaseConverter.ConvertToBase(result, right.Radix);
        }
  
        

        #endregion
        public object Clone() { Number clone = (Number)this.MemberwiseClone(); return clone; }
    }
}
