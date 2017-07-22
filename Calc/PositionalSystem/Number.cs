using System;

namespace Calc.PositionalSystem
{
    /// <summary>
    /// Represents number in positional system of arbitrary base. 
    /// </summary>
    public class Number : ICloneable
    {       
        #region Private Members

        private int systemBase;

        private double integerPartDecimalValue;
        private double fractionalPartDecimalValue;

        private String integerPartValueString;
        private String fractionalPartValueString;

        #endregion

        #region Public Properties

        /// <summary>
        /// The Radix of number, represented by decimal integer.
        /// </summary>
        public int Radix { get { return systemBase; } private set { systemBase = value; } }

        /// <summary>
        /// The integer part of number in decimal system.
        /// </summary>
        public double IntegerPartDecimalValue { get { return integerPartDecimalValue; } }
        /// <summary>
        /// The fraction part of number in decimal system.
        /// </summary>
        public double FractionalPartDecimalValue { get { return fractionalPartDecimalValue; } }
        /// <summary>
        /// The decimal value of Number.
        /// </summary>
        public double DecimalValue
        {
            get { return (double)integerPartDecimalValue + fractionalPartDecimalValue; }
            private set
            {
                fractionalPartDecimalValue = value % 1;
                integerPartDecimalValue = (long)(value - fractionalPartDecimalValue);
            }

        }

        /// <summary>
        /// The integer part of Number in given positional System, represented by String
        /// </summary>
        public String IntegerPartValueString { get { return integerPartValueString; } private set { integerPartValueString = value; } }
        /// <summary>
        /// The fraction part of Number in given base, represented by String. This field does not contain the delimeter.
        /// </summary>
        public String FractionalPartValueString { get { return fractionalPartValueString; } private set { fractionalPartValueString = value; } }
        /// <summary>
        /// The string representing value of number in given base. Field concats strings IntegerPart, FractionalPart and adds the . delimeter in between
        /// </summary>
        public String BaseValueString
        {
            get { return integerPartValueString + '.' + fractionalPartValueString; }
            private set
            {
                if (value.Contains("."))
                {
                    string[] parts = value.Split('.');
                    integerPartValueString = parts[0];
                    fractionalPartValueString = parts[1];
                }
                else
                {
                    IntegerPartValueString = value;
                    if (Radix > 36)
                        fractionalPartValueString = "00";
                    else
                        fractionalPartValueString = "0";
                }
            }
        }

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructs <see cref="Number"/> 0 in base 10
        /// </summary>
        public Number()
        {
            Radix = 10;
            DecimalValue = 0.0;
            BaseValueString = "0.0";
        }
        /// <summary>
        /// Constructs <see cref="Number"/> in given base, with it's radix, decimal Value and string value in said base
        /// </summary>
        /// <param name="radix">The radix of a number</param>
        /// <param name="decimalValue">The decimal value of a number</param>
        /// <param name="baseSystemValueStr">The string value of number in given base</param>
        public Number(int radix, double decimalValue, string baseSystemValueStr)
        {
            Radix = radix;
            DecimalValue = decimalValue;
            BaseValueString = baseSystemValueStr;
        }
        
        #endregion     

        #region Math Operations       

        /// <summary>
        /// Returns <see cref="Number"/> which decimal value is the sum of <paramref name="left"/> and <paramref name="right"/>.
        /// The base of result is the same as base of <paramref name="left"/> argument.
        /// </summary>
        /// <param name="left">Arbitrary base <see cref="Number"/>. The base of result will match the base of this argument</param>
        /// <param name="right">Arbitrary base <see cref="Number"/></param>
        /// <returns></returns>
        public static Number operator + (Number left, Number right)
        {
            double result = checked(left.DecimalValue + right.DecimalValue);
            return BaseConverter.ToBase(result, left.Radix);
        }

        /// <summary>
        /// Returns <see cref="Number"/> which decimal value is the difference of <paramref name="left"/> and <paramref name="right"/>.
        /// The base of result is the same as base of <paramref name="left"/> argument.
        /// </summary>
        /// <param name="left">Arbitrary base <see cref="Number"/>. The base of result will match the base of this argument</param>
        /// <param name="right">Arbitrary base <see cref="Number"/></param>
        /// <returns></returns>
        public static Number operator - (Number left, Number right)
        {
            double result = checked(left.DecimalValue - right.DecimalValue);
            return BaseConverter.ToBase(result, left.Radix);
        }

        /// <summary>
        /// Returns <see cref="Number"/> which decimal value is the product of <paramref name="left"/> and <paramref name="right"/>.
        /// The base of result is the same as base of <paramref name="left"/> argument.
        /// </summary>
        /// <param name="left">Arbitrary base <see cref="Number"/>. The base of result will match the base of this argument</param>
        /// <param name="right">Arbitrary base <see cref="Number"/></param>
        /// <returns></returns>
        public static Number operator * (Number left, Number right)
        {
            double result = checked(left.DecimalValue * right.DecimalValue);
            return BaseConverter.ToBase(result, left.Radix);
        }

        /// <summary>
        /// Returns <see cref="Number"/> which decimal value is the quotient of <paramref name="left"/> and <paramref name="right"/>.
        /// The base of result is the same as base of <paramref name="left"/> argument.
        /// </summary>
        /// <param name="left">Arbitrary base <see cref="Number"/>. The base of result will match the base of this argument</param>
        /// <param name="right">Arbitrary base <see cref="Number"/></param>
        /// <returns></returns>
        public static Number operator / (Number left, Number right)
        {
            double result = checked(left.DecimalValue / right.DecimalValue);
            return BaseConverter.ToBase(result, right.Radix);
        }

        #endregion

        public object Clone() { Number clone = (Number)this.MemberwiseClone(); return clone; }
    }
}
