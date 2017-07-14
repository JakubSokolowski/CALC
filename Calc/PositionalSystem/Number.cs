using System;

namespace Calc.PositionalSystem
{
    public class Number : ICloneable
    {
        private int radix;
        private long integerPartDecimalValue;
        private double fractionalPartDecimalValue;

        private String integerPartValueString;
        private String fractionalPartValueString;

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
        public object Clone() { Number clone = (Number) this.MemberwiseClone(); return clone; }

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
    }
}
