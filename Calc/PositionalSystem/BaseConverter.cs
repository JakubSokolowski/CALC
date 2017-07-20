using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.PositionalSystem
{
    public static class BaseConverter 
    {
       
        private static String digitRepresentationString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private static int formatPrecision;  

        public static Number ConvertToBase(long decimalValue, int resultRadix)
        {
            if (IsValidRadix(resultRadix))
            {
                string resultValue = DecimalIntegerToArbitraryBase(decimalValue, resultRadix);
                return new Number(resultRadix, (double)decimalValue, resultValue);
            }
            else
                throw new ArgumentException("Radix is invalid");            
        }
        public static Number ConvertToBase(double decimalValue, int resultRadix)
        {
            if (IsValidRadix(resultRadix))
            {
                double fractionValue = decimalValue % 1;
                long integerValue = (long)(decimalValue - fractionValue);

                string fractionalStr = DecimalFractionToArbitraryBase(fractionValue, resultRadix);
                string integerStr = DecimalIntegerToArbitraryBase(integerValue, resultRadix);

                return new Number(resultRadix, decimalValue, integerStr + '.' + fractionalStr);
            }
            else throw new ArgumentException("Radix is invalid");
                   
        }
        public static Number ConvertToBase(Number number, int resultRadix)
        {
            return ConvertToBase(number.DecimalValue, number.Radix);
        }
        public static Number ConvertToBase(string inputStr, int inputRadix, int resultRadix)
        {
            if (IsValidRadix(inputRadix) && IsValidRadix(resultRadix))
            {
                if (IsValidString(inputStr, inputRadix))
                {                 
                    string integerStr, fractionalStr;
                    long integerPart;
                    double fractionalPart, decimalValue;

                    Number inputInDecimal;

                    // First, convert the input to Decimal
          
                    if(IsFloatingPointStr(inputStr))
                    {
                        string[] valueParts = inputStr.Split('.');
                        integerStr = valueParts[0];
                        fractionalStr = valueParts[1];

                        integerPart = ArbitraryBaseToDecimal(integerStr, inputRadix);
                        fractionalPart = ArbitraryFractionToDecimal(fractionalStr, inputRadix);

                        if (integerPart < 0)
                            fractionalPart *= -1;

                        decimalValue = (double)integerPart + fractionalPart;

                        inputInDecimal = new Number(10, decimalValue, decimalValue.ToString());                        
                    }
                    else
                    {
                        integerPart = ArbitraryBaseToDecimal(inputStr, inputRadix);
                        inputInDecimal = new Number(10, (double)integerPart, integerPart.ToString());
                    }

                    // ConvertToBase to another base if needed

                    if (resultRadix == 10)
                        return inputInDecimal;
                    else
                        return ConvertToBase(inputInDecimal, resultRadix);                   
                }
                else throw new ArgumentException("Input Str is invalid");                
            }
            else throw new ArgumentException("Radix is invalid");
        }

        private static bool IsFloatingPointStr(string str) { return str.Contains("."); }
        private static string GetRoundingFormat() { return "0." + new string('#', FormatPrecision); }


        public static int FormatPrecision { get { return formatPrecision; } set { formatPrecision = value; ; } }      

        private static long ArbitraryBaseToDecimal(string valueString, int radix)
        {
            if (IsValidRadix(radix))
            {
                if (IsValidString(valueString, radix))
                {
                    long result = 0;
                    int mult = 1;

                    if (valueString.ElementAt(0) == '-')
                    {
                        valueString = valueString.Substring(1);
                        mult = -1;
                    }

                    int exponent = valueString.Length - 1;
                    for (int i = 0; i <= exponent; i++)
                        result += (long)digitRepresentationString.IndexOf(valueString.ElementAt(i)) * (long)Math.Pow(radix, exponent - i);
                    
                    return result * mult;
                }
                else                
                    throw new System.ArgumentException("The characters in numberString " + valueString + " do not match the radix " + radix);              
            }
            else
                throw new ArgumentException("The radix " + radix + " must be in range 2-" + (digitRepresentationString.Length - 1));
        }

        private static double ArbitraryFractionToDecimal(string fractionStr, int radix)
        {
            double decimalFraction = 0.0;
            double exponent = 1.0;
            for (int i = 0; i < fractionStr.Length; i++)
            {
                decimalFraction += (double)digitRepresentationString.IndexOf(fractionStr.ElementAt(i)) * Math.Pow(radix, exponent * -1);
                exponent++;
            }
            return decimalFraction;
        }

        private static string DecimalFractionToArbitraryBase(double fraction, int radix)
        {
            StringBuilder builder = new StringBuilder();

            double number, fractionPart;
            int wholePart;

            fractionPart = fraction;
            for (int i = 0; i < 10; i++)
            {
                number = fractionPart * radix;
                fractionPart = number % 1;
                wholePart = (int)(number - fractionPart);
                if (wholePart < 1)
                    wholePart *= -1;
                builder.Append(digitRepresentationString.ElementAt(wholePart));
            }
            return builder.ToString();
        }

        private static string DecimalIntegerToArbitraryBase(long decimalNumber, int radix)
        {
            int bitsInLong = 64;

            if (radix < 2 || radix > digitRepresentationString.Length)
                throw new ArgumentException("The radix must be >= 2 and <= " + digitRepresentationString.Length);
            if (decimalNumber == 0)
                return "0";

            int index = bitsInLong - 1;
            long currentNumber = Math.Abs(decimalNumber);

            char[] charArray = new char[bitsInLong];

            while (currentNumber != 0)
            {
                int remainder = (int)(currentNumber % radix);
                charArray[index--] = digitRepresentationString.ElementAt(remainder);
                currentNumber = currentNumber / radix;
            }

            String result = new String(charArray, index + 1, bitsInLong - index - 1);

            if (decimalNumber < 0)
            {
                result = "-" + result;
            }
            return result;
        }

        private static  string GetRepresentationRegexPattern(int radix)
        {
            String regex;

            if (radix <= 10)
            {
                // All characters that optionally start with -, are between 0-given number and have . in between
                regex = "^-?[0-#]+([.][0-#]+)?$";
                regex = regex.Replace('#', digitRepresentationString.ElementAt(radix - 1));
            }
            else
            {
                regex = "^-?[0-9A-#]+([.][0-9A-#]+)?$";
                regex = regex.Replace('#', digitRepresentationString.ElementAt(radix - 1));
            }
            return regex;
        }

        public static bool IsValidRadix(int radix)
        {
            return radix >= 2 && radix < digitRepresentationString.Length;
        }

        public static bool IsValidString(string str, int radix)
        {
            Regex regex = new Regex(GetRepresentationRegexPattern(radix));
            Match match = regex.Match(str);
            return match.Success;
        }
    }
}
