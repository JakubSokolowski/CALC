using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Calc.PositionalSystem.BigDecimal
{
    public class BigDecimal : IFormattable, IComparable, IComparable<BigDecimal>, IEquatable<BigDecimal>
    {
        private BigInteger mIntegerValue;
        private int mScale;
        private int mPrecision;

        public int Precision
        {
            get
            {
                if(mPrecision == 0)
                {
                    string s = mIntegerValue.ToString();
                    mPrecision = s.Length - ((s.ElementAt(0) == '-' ? 1 : 0));
                }
                return mPrecision;
            }                
        }

        public int Scale => mScale;
        public int Sign => mIntegerValue.Sign;
        public BigInteger UnscaledValue => mIntegerValue;
        


        public static BigDecimal Zero = new BigDecimal(BigInteger.Zero, 0);
        public static BigDecimal One = new BigDecimal(BigInteger.One, 0);

        public BigDecimal(int val)
        {
            mIntegerValue = new BigInteger(val);
            mScale = 0;
        }
        public BigDecimal(int val, MathContext mc)
            : this(val)
        {
            if(mc.Precision != 0)
            {
                BigDecimal result = Round(mc);
                mIntegerValue = result.mIntegerValue;
                mScale = result.mScale;
                mPrecision = result.mPrecision;
            }         
        }
        public BigDecimal (BigInteger num)
            : this(num, 0)
        { }
        public BigDecimal(BigInteger num, int scale)
        {
            mIntegerValue = num;
            mScale = scale;
        }
        public BigDecimal(BigInteger num, MathContext mc)
            : this (num, 0)
        {
            if (mc.Precision != 0)
            {
                BigDecimal result = Round(mc);
                mIntegerValue = result.mIntegerValue;
                mScale = result.mScale;
                mPrecision = result.mPrecision;
            }
        }
        public BigDecimal(string val, MathContext mc)
            : this(val)
        {
            if (mc.Precision != 0)
            {
                BigDecimal result = Round(mc);
                mIntegerValue = result.mIntegerValue;
                mScale = result.mScale;
                mPrecision = result.mPrecision;
            }
        }
        public BigDecimal(string num)
        {
            int len = num.Length;
            int start = 0, point = 0;
            int dot = -1;
            bool negative = false;
            if(num.ElementAt(0) == '+')
            {
                ++start;
                ++point;
            }
            else if(num.ElementAt(0) == '-')
            {
                ++start;
                ++point;
                negative = true;
            }
            while(point < len)
            {
                char c = num.ElementAt(point);
                if (c == '.')
                {
                    if (dot >= 0)
                        throw new FormatException("multiple dots in number");
                    dot = point;
                }
                else if (c == 'e' || c == 'E')
                    break;
                else if (!Char.IsDigit(c))
                    throw new FormatException("Unrecognized character: " + c);
                ++point;
            }

            String val;

            if(dot >= 0)
            {
                val = num.Substring(start, dot) + num.Substring(dot + 1, point);
                mScale = point - 1 - dot;
            }
            else
            {
                val = num.Substring(start, point);
                mScale = 0;
            }

            if (val.Length == 0)
                throw new FormatException("no digits seen");

            if (negative)
                val = "-" + val;
            mIntegerValue = BigInteger.Parse(val);

            if(point < len)
            {
                point++;
                if (num.ElementAt(point) == '+')
                    point++;
                if (point >= len)
                    throw new FormatException("no exponenet following e or E");

                try
                { 
                    mScale -= Int32.Parse(num.Substring(point));
                }
                catch(FormatException ex)
                {
                    throw new FormatException("malformed exponent");
                }
            }
        }

      

        public BigDecimal(BigInteger num, int scale, MathContext mc)
            : this(num,scale)
        {
            if (mc.Precision != 0)
            {
                BigDecimal result = Round(mc);
                mIntegerValue = result.mIntegerValue;
                mScale = result.mScale;
                mPrecision = result.mPrecision;
            }
        }
        public BigDecimal (double num, MathContext mc)
            : this(num)
        {
            if (mc.Precision != 0)
            {
                BigDecimal result = Round(mc);
                mIntegerValue = result.mIntegerValue;
                mScale = result.mScale;
                mPrecision = result.mPrecision;
            }
        }
        public BigDecimal (double num)
        {
            if (Double.IsInfinity(num) || Double.IsNaN(num))
                throw new FormatException();

            int mantissaBits = 52;
            int exponentBits = 11;
            long mantissaMask = (1L << mantissaBits) - 1;
            long exponentMask = (1L << exponentBits) - 1;

            long bits = BitConverter.DoubleToInt64Bits(num);
            long mantissa = bits & mantissaMask;

            // might need to cast to long idk
            long exponent = (bits >> mantissaBits) & exponentMask;
            bool isDenormal = exponent == 0;
            exponent -= isDenormal ? 1022 : 1023;
            exponent -= mantissaBits;

            if (!isDenormal)
                mantissa |= (1L << mantissaBits);

            while (exponent < 0 && (mantissa & 1) == 0)
            {
                ++exponent;
                mantissa >>= 1;
            }

            mIntegerValue = new BigInteger(bits < 0 ? -mantissa : mantissa);

            if(exponent < 0)
            {
                mScale = (int)(-exponent);
                BigInteger mult = BigInteger.Pow(new BigInteger(5), mScale);
                mIntegerValue = BigInteger.Multiply(mIntegerValue, mult);
            }
        }


        public BigDecimal Add(BigDecimal val)
        {
            BigInteger op1 = mIntegerValue;
            BigInteger op2 = val.mIntegerValue;
            if (mScale < val.mScale)
                op1 = BigInteger.Multiply(op1, BigInteger.Pow(new BigInteger(10), val.mScale - mScale));
            else if (mScale > val.mScale)
                op2 = BigInteger.Multiply(op2, BigInteger.Pow(new BigInteger(10), mScale - val.mScale));

            return new BigDecimal(BigInteger.Add(op1, op2), Math.Max(mScale, val.mScale));
        }
        public BigDecimal Add(BigDecimal val, MathContext mc)
        {
            return Add(val).Round(mc);
        }

        public BigDecimal Subtract(BigDecimal val)
        {
            return Add(val.Negate());
        }
        public BigDecimal Subtract(BigDecimal val, MathContext mc)
        {
            return Subtract(val).Round(mc);
        }          


        public BigDecimal Multiply(BigDecimal val)
        {
            return new BigDecimal(BigInteger.Multiply(mIntegerValue, val.mIntegerValue), mScale + val.mScale);
        }
        public BigDecimal Multiply(BigDecimal val, MathContext mc)
        {
            return Multiply(val).Round(mc);
        }

        public BigDecimal Divide(BigDecimal val, int newScale, RoundingMode roundingMode)
        {
            // Handle 0.0 / 0.0
            if (mIntegerValue.Sign == 0)
                return newScale == 0 ? Zero : new BigDecimal(Zero.mIntegerValue, newScale);

            // Ensure that pow gets non-negative value
            BigInteger valIntVal = val.mIntegerValue;
            int power = newScale - (Scale - val.mScale);

            if(power < 0)
            {
                valIntVal = BigInteger.Multiply(valIntVal, BigInteger.Pow(new BigInteger(10), -power));
                power = 0;
            }

            BigInteger dividend = BigInteger.Multiply(mIntegerValue, BigInteger.Pow(new BigInteger(10), -power));
            BigInteger unrounded =  BigInteger.DivRem(dividend, valIntVal, out BigInteger reminder);

            if (reminder.Sign == 0)
                return new BigDecimal(unrounded, newScale);

            if (roundingMode == RoundingMode.Unnecessary)
                throw new ArithmeticException("Rounding necessary");
            int sign = mIntegerValue.Sign * valIntVal.Sign;

            if (roundingMode == RoundingMode.Ceiling)
                roundingMode = (sign > 0) ? RoundingMode.Up : RoundingMode.Down;
            else if (roundingMode == RoundingMode.Floor)
                roundingMode = (sign < 0) ? RoundingMode.Up : RoundingMode.Down;
            else
            {
                BigInteger positionRemainder = reminder.Sign < 0 ? BigInteger.Negate(unrounded) : unrounded;
                valIntVal = valIntVal.Sign < 0 ? BigInteger.Negate(valIntVal) : valIntVal;
                int half = (positionRemainder << 1).CompareTo(valIntVal);
                
                switch(roundingMode)
                {
                    case RoundingMode.HalfUp:
                        roundingMode = (half < 0) ? RoundingMode.Down : RoundingMode.Up;
                        break;
                    case RoundingMode.HalfDown:
                        roundingMode = (half > 0) ? RoundingMode.Up : RoundingMode.Down;
                        break;
                    case RoundingMode.HalfEven:
                        if (half < 0)
                            roundingMode = RoundingMode.Up;
                        else if (half > 0)
                            roundingMode = RoundingMode.Up;
                        else if (unrounded.IsEven)
                            roundingMode = RoundingMode.Down;
                        else
                            roundingMode = RoundingMode.Up;
                        break;   
                }
            }
            if (roundingMode == RoundingMode.Up)
                unrounded = BigInteger.Add(unrounded, new BigInteger(sign > 0 ? 1 : -1));
            return new BigDecimal(unrounded, newScale);
        }
        public BigDecimal Divide(BigDecimal val, RoundingMode roundingMode)
        {
            return Divide(val, mScale, roundingMode);
        }
       
        public BigDecimal Divide(BigDecimal divisor)
        {
            return Divide(divisor, mScale, RoundingMode.Unnecessary);
        }




        public BigDecimal Round(MathContext mc)
        {
            int mcPrecision = mc.Precision;
            int numToChop = Precision - mcPrecision;

            if (mcPrecision == 0 || numToChop <= 0)
                return this;

            BigDecimal div = new BigDecimal(BigInteger.Pow(new BigInteger(10), numToChop));
            BigDecimal rounded = Divide(div, mScale, mc.RoundingMode);
            rounded.mScale -= numToChop;
            rounded.mPrecision = mcPrecision;
            return rounded;
        }

        public BigDecimal[] DivideAndReminder(BigDecimal val)
        {
            BigDecimal[] result = new BigDecimal[2];
            result[0] = DivideToIntegralValue(val);
            result[1] = Subtract(result[0].Multiply(val));
            return result;
        }

        public BigDecimal DivideToIntegralValue(BigDecimal val)
        {
            return Divide(val, RoundingMode.Down).Floor().SetScale(mScale - val.mScale, RoundingMode.Down);
        }

        private BigDecimal Floor()
        {
            if (mScale <= 0)
                return this;
            string intValStr = mIntegerValue.ToString();
            intValStr = intValStr.Substring(0, intValStr.Length - mScale);
            mIntegerValue = BigInteger.Multiply(BigInteger.Parse(intValStr), BigInteger.Pow(new BigInteger(10), mScale));
            return this;
        }


        public BigDecimal Negate()
        {
            return new BigDecimal(BigInteger.Negate(mIntegerValue), mScale);
        }
        public BigDecimal Negate(MathContext mc)
        {
            BigDecimal result = Negate();
            if(mc.Precision != 0)
                result = result.Round(mc);
            return result;
        }
        public BigDecimal Abs()
        {
            return new BigDecimal(BigInteger.Abs(mIntegerValue), mScale);
        }

        public BigDecimal SetScale(int scale)
        {
            return SetScale(scale, RoundingMode.Unnecessary);
        }
        public BigDecimal SetScale(int scale, RoundingMode roundingMode)
        {
            if (scale < 0)
                throw new ArithmeticException("Scale parameter < 0");
            return Divide(One, scale, roundingMode);
        }


        

       

 
        



        public int CompareTo(BigDecimal other)
        {
            if (mScale == other.mScale)
                return mIntegerValue.CompareTo(other);

            BigInteger thisResult = BigInteger.DivRem(mIntegerValue, BigInteger.Pow(new BigInteger(10), mScale), out BigInteger thisReminder);
            BigInteger otherResult = BigInteger.DivRem(other.mIntegerValue, BigInteger.Pow(new BigInteger(10), other.mScale), out BigInteger otherReminder);

            int compare;
            if ((compare = thisResult.CompareTo(otherResult)) != 0)
                return compare;

            // quotients are the same, compare remainders

            // Ad trailing zeros to the remainder with teh smallest scael
            if (mScale < other.mScale)
                thisReminder = BigInteger.Multiply(thisReminder, BigInteger.Pow(new BigInteger(10), (other.mScale - mScale)));
            else if(mScale > other.mScale)
                otherReminder = BigInteger.Multiply(otherReminder, BigInteger.Pow(new BigInteger(10), (mScale - other.mScale)));
            return thisReminder.CompareTo(otherReminder);
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public bool Equals(BigDecimal other)
        {
            throw new NotImplementedException();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }
      
    }
}
