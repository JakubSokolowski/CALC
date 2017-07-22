using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.PositionalSystem
{
    /// <summary>
    /// Converts numbers to arbitrary base.
    /// </summary>
    public static class BaseConverter
    {

        #region Private Members
        
        private static BaseRepresentation representation = new BaseRepresentation();        
        private static int formatPrecision;

        #endregion

        #region Public Fields

        /// <summary>
        /// Specifies how many decimal places will Converted numbers have
        /// </summary>
        public static int FormatPrecision { get { return formatPrecision; } set { formatPrecision = value; } }       

        #endregion    

        #region Public Converting Methods
        
        /// <summary>
        /// Converts from decimal to arbitrary base <see cref="Number"/>.
        /// </summary>
        /// <param name="decimalValue"> The value that is converted</param>
        /// <param name="resultBase"> The base of the result of conversion</param>
        /// <returns><see cref="Number"/></returns>
        public static Number ToBase(double decimalValue, int resultBase)
        {           
            if (IsValidRadix(resultBase))
            {
                double fractionValue = decimalValue % 1;
                long integerValue = (long)(decimalValue - fractionValue);

                string fractionalStr = DecimalFractionToArbitraryBase(fractionValue, resultBase);
                string integerStr = DecimalIntegerToArbitraryBase(integerValue, resultBase);

                return new Number(resultBase, decimalValue, integerStr + '.' + fractionalStr);
            }
            else throw new ArgumentException("Radix is invalid");
                   
        }
        /// <summary>
        /// Converts from decimal to arbitrary base <see cref="Number"/>.
        /// </summary>
        /// <param name="number"> The value that is converted.</param>
        /// <param name="resultBase"> The base of the result of conversion.</param>
        /// <returns>The<see cref="Number"/> in given base</returns>
        public static Number ToBase(Number number, int resultBase)
        {
            return ToBase(number.DecimalValue, number.Radix);
        }
        /// <summary>
        /// Converts from decimal to arbitrary base <see cref="Number"/>.
        /// </summary>
        /// <param name="decimalValue"> The value that is converted</param>
        /// <param name="resultBase"> The base of the result of conversion</param>
        /// <returns><see cref="Number"/></returns>
        public static Number ToBase(string inputStr, int inputBase, int resultBase)
        {
            if (IsValidRadix(inputBase) && IsValidRadix(resultBase))
            {
                if (IsValidString(inputStr, inputBase))
                {                 
                    string integerStr, fractionalStr;                 
                    double fractionalPart, decimalValue, integerPart;

                    Number inputInDecimal;

                    // First, convert the input to Decimal
          
                    if(IsFloatingPointStr(inputStr))
                    {
                        string[] valueParts = inputStr.Split('.');
                        integerStr = valueParts[0];
                        fractionalStr = valueParts[1];

                        integerPart = ArbitraryBaseToDecimal(integerStr, inputBase);
                        fractionalPart = ArbitraryFractionToDecimal(fractionalStr, inputBase);

                        if (integerPart < 0)
                            fractionalPart *= -1;

                        decimalValue = (double)integerPart + fractionalPart;

                        inputInDecimal = new Number(10, decimalValue, decimalValue.ToString());                        
                    }
                    else
                    {
                        integerPart = ArbitraryBaseToDecimal(inputStr, inputBase);
                        inputInDecimal = new Number(10, (double)integerPart, integerPart.ToString());
                    }

                    // ToBase to another base if needed

                    if (resultBase == 10)
                        return inputInDecimal;
                    else
                        return ToBase(inputInDecimal, resultBase);                   
                }
                else throw new ArgumentException("Input Str is invalid");                
            }
            else throw new ArgumentException("Radix is invalid");
        }

        #endregion  

        #region Converting Integer Part
        
        /// <summary>
        /// Converts V
        /// </summary>
        /// <param name="valueString"></param>
        /// <param name="radix"></param>
        /// <returns></returns>
        private static double ArbitraryBaseToDecimal(string valueString, int radix)
        {    
            if (IsValidRadix(radix))
            {
                if (IsValidString(valueString, radix))
                {
                    representation.CurrentBase = radix;

                    double result = 0.0;
                    int mult = 1;

                    if (valueString.ElementAt(0) == '-')
                    {
                        valueString = valueString.Substring(1);
                        mult = -1;
                    }

                    var strList = RepresentationStringToStringList(valueString, radix);

                    int exponent = strList.Count - 1;
                    for (int i = 0; i <= exponent; i++)
                        result += representation.GetValue(strList.ElementAt(i)) * Math.Pow(radix, exponent - i);                   

                    return result * mult;
                }
                else                
                    throw new System.ArgumentException("The characters in numberString " + valueString + " do not match the systemBase " + radix);              
            }
            else
                throw new ArgumentException("The systemBase " + radix + " must be in range 2- 256");
        }
        private static string DecimalIntegerToArbitraryBase(long decimalNumber, int radix)
        {
            if (radix < 2 || radix > BaseRepresentation.MAX_BASE)
                throw new ArgumentException("The systemBase must be >= 2 and <= " + BaseRepresentation.MAX_BASE);
            if (decimalNumber == 0)
                return "0";

            long currentNumber = Math.Abs(decimalNumber);
            representation.CurrentBase = radix;

            StringBuilder sb = new StringBuilder();

            while (currentNumber != 0)
            {
                int remainder = (int)(currentNumber % radix);
                if (radix > 36)
                {
                    sb.Append(representation.GetDigit(remainder).ToCharArray().Reverse().ToArray());
                    sb.Append(" ");
                    currentNumber = currentNumber / radix;
                    continue;
                }
                sb.Append(representation.GetDigit(remainder));

                currentNumber = currentNumber / radix;
            }

            String result = new string(sb.ToString().ToCharArray().Reverse().ToArray());
            if (radix > 36)
                result = result.Substring(1);

            if (decimalNumber < 0)
            {
                result = "-" + result;
            }
            return result;
        }

        #endregion

        #region Converting Fractional Part

        private static double ArbitraryFractionToDecimal(string fractionStr, int radix)
        {
            double decimalFraction = 0.0;
            double exponent = 1.0;

            var strList = RepresentationStringToStringList(fractionStr, radix);
            representation.CurrentBase = radix;
          
            for (int i = 0; i < strList.Count; i++)
            {               
                decimalFraction += (double)representation.GetValue(strList.ElementAt(i)) * Math.Pow(radix, exponent * -1);
                exponent++;
            }
            return decimalFraction;
        }
        private static string DecimalFractionToArbitraryBase(double fraction, int radix)
        {
            if(fraction == 0.0)
            {
                if (radix <= 36)
                    return "0";
                return "00";
            }

            StringBuilder builder = new StringBuilder();
            representation.CurrentBase = radix;

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
                string str = representation.GetDigit(wholePart);
                builder.Append(str);                 
                if (radix > 36)
                    builder.Append(" ");
            }
            return RemoveTrailingZeros(builder.ToString());
        }

        #endregion

        #region Helper Methods for Converting

        public static List<String> RepresentationStringToStringList(string str, int radix)
        {
            var strList = new List<String>();
            if (radix <= 36)
            {
                strList = str.ToCharArray().Select(x => x.ToString()).ToList();
            }
            else
            {
                strList = str.Split(' ').ToList();
            }
            return strList;
        }
        private static bool IsFloatingPointStr(string str) { return str.Contains("."); }

        private static String RemoveTrailingZeros(string str)
        {
            int lenght = str.Length - 1;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (str.ElementAt(i) == '0' || str.ElementAt(i) == ' ')
                    lenght = i;
                else
                    break;
            }

            return str.Substring(0, lenght);
        }

        #endregion

        #region Floating Point Numbers Precision
        private static string GetRoundingFormat() { return "0." + new string('#', FormatPrecision); }
        #endregion

        #region Input Validation
        /// <summary>
        /// Returns regex pattern matching characters in base 2-36
        /// </summary>
        /// <param name="inputBase">The base that specifies witch characters are avalible</param>
        /// <returns>Regex pattern that matches possible numbers in given base</returns>
        private static string GetRepresentationRegexPattern(int inputBase)
        {
            // Matching characters by regex is only supportet for bases 2 - 36 (10 digits and 26 alphabet characters)
            // Positions in higher bases are represented differently - not by characters but by numbers
            // for example base 64 number - 12 09 45
            if (inputBase > 36)
                throw new ArgumentException();
            else
            {
                String pattern = " ";
                representation.CurrentBase = inputBase;

                // For bases smaller or equal 10, we only have to match digits
                if (inputBase <= 10)
                {
                    // All characters that optionally start with -, are between 0 - given number 
                    // and might have . in between
                    pattern = "^-?[0-#]+([.][0-#]+)?$";
                    pattern = pattern.Replace('#', representation.GetDigit(inputBase - 1)[0]);
                }
                // For bases 10 - 36 we need to match digits and capital letters
                else
                {
                    // All characters that optionally start with -, are between 0 - 9 or A - last character of representaton 
                    // and might . in between
                    pattern = "^-?[0-9A-#]+([.][0-9A-#]+)?$";
                    pattern = pattern.Replace('#', representation.GetDigit(inputBase - 1)[0]);
                }
                return pattern;
            }    
        }

        public static bool IsValidRadix(int radix)
        {
            return radix >= 2 && radix < BaseRepresentation.MAX_BASE;
        }

        public static bool IsValidString(string str, int radix)
        {
            if(radix <=36)
            {
                Regex regex = new Regex(GetRepresentationRegexPattern(radix));
                Match match = regex.Match(str);
                return match.Success;
            }
            else
            {
                if (str[0] == '-')
                    str = str.Substring(1);
                var strList = str.Replace('.',' ').Split(' ').ToList();
                foreach(var num in strList)
                {
                    if (Int32.TryParse(num, out int x))
                    {
                        if (x >= radix)
                            return false;
                    }
                    else
                        return false;
                }
                return true;
            }           
        }

        # endregion
       
    }
}
