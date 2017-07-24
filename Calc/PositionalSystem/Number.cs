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

        private double valueInDecimal;  
        private string valueInBase;

        private string valueInBaseComplement;

        #endregion

        #region Public Properties

        /// <summary>
        /// The Radix of number, represented by decimal integer.
        /// </summary>
        public int Radix { get { return systemBase; } private set { systemBase = value; } }

        /// <summary>
        /// The integer part of number in decimal system.
        /// </summary>
        public double IntegerPartDecimalValue { get { return valueInDecimal - FractionPartDecimalValue; } }
        /// <summary>
        /// The fraction part of number in decimal system.
        /// </summary>
        public double FractionPartDecimalValue { get { return valueInDecimal % 1; } }
        /// <summary>
        /// The decimal value of Number.
        /// </summary>
        public double DecimalValue  { get { return valueInDecimal; } private set { valueInDecimal = value; } }

        /// <summary>
        /// The integer part of Number in given positional System, represented by String
        /// </summary>
        public string IntegerPartBaseValue { get { return valueInBase.Split('.')[0]; } }
        /// <summary>
        /// The fraction part of Number in given base, represented by String. This field does not contain the delimeter.
        /// </summary>
        public string FractionPartBaseValue { get { return valueInBase.Split('.')[1]; } }
        /// <summary>
        /// The string representing value of number in given base. Field concats strings IntegerPart, FractionalPart and adds the . delimeter in between
        /// </summary>
        public string ValueInBase { get { return valueInBase; } private set { valueInBase = value; } }

        public string Complement { get { return valueInBaseComplement ; } set { valueInBaseComplement = value; } }
        public string ComplementIntegerPart { get { return Complement.Split('.')[0]; } }
        public string ComplementFractionPart { get { return Complement.Split('.')[1]; } }

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructs <see cref="Number"/> 0 in base 10
        /// </summary>
        public Number()
        {
            Radix = 10;
            DecimalValue = 0.0;
            ValueInBase = "0.0";
        }
        /// <summary>
        /// Constructs <see cref="Number"/> in given base, with it's radix, decimal Value and string value in said base
        /// </summary>
        /// <param name="radix">The radix of a number</param>
        /// <param name="decimalValue">The decimal value of a number</param>
        /// <param name="baseSystemValueStr">The string value of number in given base</param>  

        public Number(int radix, double decimalValue, string baseSystemValueStr, string complement)
        {
            Radix = radix;
            DecimalValue = decimalValue;
            ValueInBase = baseSystemValueStr;
            Complement = complement;
        }

        #endregion

        #region Math Operations Without Steps      

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

        /// <summary>
        /// Returns the square root of specified <see cref="Number"/>
        /// </summary>
        /// <param name="num">The arbitrary base <see cref="Number"/> to be squared</param>
        /// <returns>The square root of <paramref name="num"/></returns>
        public static Number Sqrt(Number num)
        {
            double result = Math.Sqrt(num.DecimalValue);
            return BaseConverter.ToBase(result, num.Radix);
        }
        /// <summary>
        /// Returns the specified <see cref="Number"/> raised to specified <paramref name="power"/>
        /// </summary>
        /// <param name="num">The arbitrary base <see cref="Number"/></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public static Number Pow(Number num, double power)
        {
            double result = Math.Pow(num.DecimalValue, power);
            return BaseConverter.ToBase(result, num.Radix);
        }

        #endregion

        #region Math Operations With Steps

        #endregion

        public object Clone() { Number clone = (Number)this.MemberwiseClone(); return clone; }

        
    }
}
