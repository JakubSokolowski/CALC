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
        private static BaseRepresentation baseRepresentation = new BaseRepresentation(256);

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
            baseRepresentation.CurrentRadix = radix;            

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

                    var strList = RepresentationStringToStringList(valueString, radix);

                    int exponent = strList.Count - 1;
                    for (int i = 0; i <= exponent; i++)
                        result += (long)baseRepresentation.GetValue(strList.ElementAt(i)) * (long)Math.Pow(radix, exponent - i);
                    //result += (long)digitRepresentationString.IndexOf(valueString.ElementAt(i)) * (long)Math.Pow(radix, exponent - i);

                    return result * mult;
                }
                else                
                    throw new System.ArgumentException("The characters in numberString " + valueString + " do not match the radix " + radix);              
            }
            else
                throw new ArgumentException("The radix " + radix + " must be in range 2- 256");
        }

        private static double ArbitraryFractionToDecimal(string fractionStr, int radix)
        {
            double decimalFraction = 0.0;
            double exponent = 1.0;

            var strList = RepresentationStringToStringList(fractionStr, radix);
            baseRepresentation.CurrentRadix = radix;

            for (int i = 0; i < strList.Count; i++)
            {
                //decimalFraction += (double)digitRepresentationString.IndexOf(fractionStr.ElementAt(i)) * Math.Pow(radix, exponent * -1);
                decimalFraction += (double)baseRepresentation.GetValue(strList.ElementAt(i)) * Math.Pow(radix, exponent * -1);
                exponent++;
            }
            return decimalFraction;
        }

        private static string DecimalFractionToArbitraryBase(double fraction, int radix)
        {
            StringBuilder builder = new StringBuilder();
            baseRepresentation.CurrentRadix = radix;

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
                builder.Append(baseRepresentation.GetDigit(wholePart));
                if (radix > 36)
                    builder.Append(" ");
            }
            return builder.ToString();
        }

        private static string DecimalIntegerToArbitraryBase(long decimalNumber, int radix)
        {
            if (radix < 2 || radix > BaseRepresentation.MAX_AVALIBLE_RADIX)
                throw new ArgumentException("The radix must be >= 2 and <= " + BaseRepresentation.MAX_AVALIBLE_RADIX);
            if (decimalNumber == 0)
                return "0";

            long currentNumber = Math.Abs(decimalNumber);
            baseRepresentation.CurrentRadix = radix;

            StringBuilder sb = new StringBuilder();

            while (currentNumber != 0)
            {
                int remainder = (int)(currentNumber % radix);              
                sb.Append(baseRepresentation.GetDigit(remainder));
                if (radix > 36)
                    sb.Append(" ");
                currentNumber = currentNumber / radix;
            }

            String result = new string(sb.ToString().ToCharArray().Reverse().ToArray());            

            if (decimalNumber < 0)
            {
                result = "-" + result;
            }
            return result;
        }

        private static  string GetRepresentationRegexPattern(int radix)
        {
            String regex = " ";
            baseRepresentation.CurrentRadix = radix;

            if (radix <= 10)
            {
                // All characters that optionally start with -, are between 0-given number and have . in between
                regex = "^-?[0-#]+([.][0-#]+)?$";
                regex = regex.Replace('#', baseRepresentation.GetDigit(radix-1)[0]);
            } 
            else
            if(radix > 10 && radix <=36 )
            {
                regex = "^-?[0-9A-#]+([.][0-9A-#]+)?$";
                regex = regex.Replace('#', baseRepresentation.GetDigit(radix-1)[0]);
            }
            else
            if(radix > 36)
            {
                regex = "^-[0-9]?$";
            }
            return regex;
        }

        public static bool IsValidRadix(int radix)
        {
            return radix >= 2 && radix < BaseRepresentation.MAX_AVALIBLE_RADIX;
        }

        public static bool IsValidString(string str, int radix)
        {
            Regex regex = new Regex(GetRepresentationRegexPattern(radix));
            Match match = regex.Match(str);
            return match.Success;
        }

        public static List<String> RepresentationStringToStringList(string str, int radix)
        {
            var strList = new List<String>();
            if(radix <= 36)
            {
                strList = str.ToCharArray().Select(x => x.ToString()).ToList();
            }
            else
            {
                strList = str.Split(' ').ToList();
            }
            return strList;
        }
    }
}
