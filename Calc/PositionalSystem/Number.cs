using System;

namespace Calc.PositionalSystem
{
    /// <summary>
    /// Represents number in positional system of arbitrary base. 
    /// </summary>
    public class Number : ICloneable
    {
        #region Private Members 

       
        private BaseRepresentation baseRep;
        private SingleRepresentation singleRep;
        private DoubleRepresentation doubleRep;      

        #endregion

        #region Public Properties

        /// <summary>
        /// The Radix of number, represented by decimal integer.
        /// </summary>
        public int Radix { get { return baseRep.Radix; } private set { baseRep.Radix = value; } }

        /// <summary>
        /// The integer part of number in decimal system.
        /// </summary>
        public decimal IntegerPartDecimalValue { get { return baseRep.IntegerPartDecimalValue- baseRep.FractionPartDecimalValue; } }
        /// <summary>
        /// The fraction part of number in decimal system.
        /// </summary>
        public decimal FractionPartDecimalValue { get { return baseRep.DecimalValue % 1; } }
        /// <summary>
        /// The decimal value of Number.
        /// </summary>
        public decimal DecimalValue  { get { return baseRep.DecimalValue; } private set { baseRep.DecimalValue = value; } }

        /// <summary>
        /// The integer part of Number in given positional System, represented by String
        /// </summary>
        public string IntegerPartBaseValue { get { return baseRep.IntegerPartBaseValue; } }
        /// <summary>
        /// The fraction part of Number in given base, represented by String. This field does not contain the delimeter.
        /// </summary>
        public string FractionPartBaseValue { get { return baseRep.FractionPartBaseValue; } }
        /// <summary>
        /// The string representing value of number in given base. Field concats strings IntegerPart, FractionalPart and adds the . delimeter in between
        /// </summary>
        public string ValueInBase { get { return baseRep.ValueInBase; } private set { baseRep.ValueInBase = value; } }

        public string Complement { get { return baseRep.Complement; } }
        public string ComplementIntegerPart { get { return baseRep.ComplementIntegerPart; } }
        public string ComplementFractionPart { get { return baseRep.ComplementFractionPart; } }      
        
        public string SingleBinaryString { get => singleRep.BinaryString; }
        public string DoubleBinaryString { get => doubleRep.BinaryString; }

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructs <see cref="Number"/> 0 in base 10
        /// </summary>
        public Number()
        {
            Radix = 10;
            DecimalValue = 0.0M;
            ValueInBase = "0.0";
        }       

        public Number(BaseRepresentation bRep, SingleRepresentation sRep, DoubleRepresentation dRep)
        {
            baseRep = bRep;
            singleRep = sRep;
            doubleRep = dRep;
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
            decimal result = checked(left.DecimalValue + right.DecimalValue);
            return NumberConverter.ToBase(result, left.Radix);
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
            decimal result = checked(left.DecimalValue - right.DecimalValue);
            return NumberConverter.ToBase(result, left.Radix);
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
            decimal result = checked(left.DecimalValue * right.DecimalValue);
            return NumberConverter.ToBase(result, left.Radix);
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
            decimal result = checked(left.DecimalValue / right.DecimalValue);
            return NumberConverter.ToBase(result, right.Radix);
        }

        /// <summary>
        /// Returns the square root of specified <see cref="Number"/>
        /// </summary>
        /// <param name="num">The arbitrary base <see cref="Number"/> to be squared</param>
        /// <returns>The square root of <paramref name="num"/></returns>
        public static Number Sqrt(Number num)
        {
            decimal result =(decimal) Math.Sqrt((double)num.DecimalValue);
            return NumberConverter.ToBase(result, num.Radix);
        }
        /// <summary>
        /// Returns the specified <see cref="Number"/> raised to specified <paramref name="power"/>
        /// </summary>
        /// <param name="num">The arbitrary base <see cref="Number"/></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public static Number Pow(Number num, double power)
        {
            decimal result =(decimal)( Math.Pow((double)num.DecimalValue, power));
            return NumberConverter.ToBase(result, num.Radix);
        }

        #endregion

        #region Math Operations With Steps

        #endregion

        public object Clone() { Number clone = (Number)this.MemberwiseClone(); return clone; }

        
    }
}
