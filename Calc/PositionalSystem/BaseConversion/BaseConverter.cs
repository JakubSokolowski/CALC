using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;



namespace Calc.PositionalSystem
{
    /// <summary>
    /// Converts numbers to arbitrary base.
    /// </summary>
    public class BaseConverter
    {

        #region Private Members
        
        private static BaseDigits digits = new BaseDigits();
        private static ComplementConverter comp = new ComplementConverter();
        private int formatPrecision = 20;
       

        #endregion

        #region Public Fields

        /// <summary>
        /// Specifies how many decimal places will Converted numbers have
        /// </summary>
        public int FormatPrecision { get { return formatPrecision; } set { formatPrecision = value; } }      

        #endregion    

        #region Public Converting Methods
        
        /// <summary>
        /// Converts from decimal to arbitrary base <see cref="Number"/>.
        /// </summary>
        /// <param name="decimalValue"> The value that is converted</param>
        /// <param name="resultBase"> The base of the result of conversion</param>
        /// <returns><see cref="Number"/></returns>
        public  BaseRepresentation ToBase(decimal decimalValue, int resultBase)
        {           
            if (IsValidRadix(resultBase))
            {
                // Split the value to integer part and fraction part
                decimal fractionValue = decimalValue % 1;
                long integerValue = (long)(decimalValue - fractionValue);

                // Convert each part
                string fractionalStr = DecimalFractionToArbitraryBase(fractionValue, resultBase);
                string integerStr = DecimalIntegerToArbitraryBase(integerValue, resultBase);

                string result = integerStr + '.' + fractionalStr;
                return new BaseRepresentation(resultBase, decimalValue, result, comp.GetComplement(result, resultBase));
            }
            else throw new ArgumentException("Radix is invalid");
                   
        }
        /// <summary>
        /// Converts from decimal to arbitrary base <see cref="Number"/>.
        /// </summary>
        /// <param name="number"> The value that is converted.</param>
        /// <param name="resultBase"> The base of the result of conversion.</param>
        /// <returns>The<see cref="Number"/> in given base</returns>
        public  BaseRepresentation ToBase(BaseRepresentation number, int resultBase)
        {
            return ToBase(number.DecimalValue, number.Radix);
        }
        /// <summary>
        /// Converts from decimal to arbitrary base <see cref="Number"/>.
        /// </summary>
        /// <param name="decimalValue"> The value that is converted</param>
        /// <param name="resultBase"> The base of the result of conversion</param>
        /// <returns><see cref="Number"/></returns>
        public  BaseRepresentation ToBase(string inputStr, int inputBase, int resultBase)
        {
            if (IsValidRadix(inputBase) && IsValidRadix(resultBase))
            {
                if (IsValidString(inputStr, inputBase))
                {                 
                    string integerStr, fractionalStr;                 
                    decimal fractionalPart, decimalValue, integerPart;

                    BaseRepresentation inputInDecimal;

                    // First, convert the input Number to Decimal
          
                    if(IsFloatingPointStr(inputStr))
                    {
                        // Split the whole value into integer part and fraction part strings
                        string[] valueParts = inputStr.Split('.');
                        integerStr = valueParts[0];
                        fractionalStr = valueParts[1];

                        // Convert each part
                        integerPart = ArbitraryBaseToDecimal(integerStr, inputBase);
                        fractionalPart = ArbitraryFractionToDecimal(fractionalStr, inputBase);

                        // Make the fractionalPart negative if the integer part is also negative
                        // This is needed when both parts are added together to create whole value
                        if (integerPart < 0)
                            fractionalPart *= -1;

                        decimalValue = (decimal)integerPart + fractionalPart;

                        inputInDecimal = new BaseRepresentation(10, decimalValue, decimalValue.ToString(), comp.GetComplement(decimalValue.ToString(), resultBase));                        
                    }
                    else
                    {
                        // Not a floating point number, convert only the integer part.
                        integerPart = ArbitraryBaseToDecimal(inputStr, inputBase);
                        inputInDecimal = new BaseRepresentation(10, integerPart, integerPart.ToString(), comp.GetComplement(integerPart.ToString(), resultBase));
                    }                    

                    if (resultBase == 10)
                        return inputInDecimal;                   
                    else
                        // Convert to result Base
                        return ToBase(inputInDecimal, resultBase);                   
                }
                else throw new ArgumentException("Input Str is invalid");                
            }
            else throw new ArgumentException("Radix is invalid");
        }

        #endregion  

        #region Converting Integer Part
        
        /// <summary>
        /// Converts value string of specified base to decimal
        /// </summary>
        /// <param name="valueString"></param>
        /// <param name="radix"></param>
        /// <returns></returns>
        private  decimal ArbitraryBaseToDecimal(string valueString, int radix)
        {    
            if (IsValidRadix(radix))
            {
                if (IsValidString(valueString, radix))
                {
                    // Set the base of representation before converting
                    digits.CurrentBase = radix;

                    decimal result = 0.0m;
                    int mult = 1;
                    
                    // While converting, the numbers are assumed to be unsigned
                    // Detect and remember if number was negative
                    if (valueString.ElementAt(0) == '-')
                    {
                        valueString = valueString.Substring(1);
                        mult = -1;
                    }

                    // Digits at positions in some representation are represented by multiple characters,
                    // so it's necessary to convert valueString to list of strings
                    var strList = ConversionHelpers.RepresentationStringToStringList(valueString, radix);

                    // The value at each position is calculated by taking the value of digit 
                    // and multiplying it by the base of number to the power of exponent

                    // The exponents at positions are as follows:
                    // For number 253
                    // Exponents:  ...2 1 0
                    // Digits:        5 3 1
                    // So the starting value of exponent is the count of elements in lis -1

                    int exponent = strList.Count - 1;
                    for (int i = 0; i <= exponent; i++)
                        // Decrease value of by 1 exponent after each iteration
                        result += (decimal)(digits.GetValue(strList.ElementAt(i)) * Math.Pow(radix, exponent - i));                   

                    // Make the number negative, if needed
                    return result * mult;
                }
                else                
                    throw new System.ArgumentException("The characters in numberString " + valueString + " do not match the systemBase " + radix);              
            }
            else
                throw new ArgumentException("The systemBase " + radix + " must be in range 2- 256");
        }
        /// <summary>
        /// Converts decimal number to valueString in specified base
        /// </summary>
        /// <param name="decimalNumber"></param>
        /// <param name="radix"></param>
        /// <returns>The string representation of <paramref name="decimalNumber"/> in specified base</returns>
        public string DecimalIntegerToArbitraryBase(long decimalNumber, int radix)
        {
            if (radix < 2 || radix > BaseDigits.MAX_BASE)
                throw new ArgumentException("The base must be >= 2 and <= " + BaseDigits.MAX_BASE);

            if (decimalNumber == 0)
            {
                if (radix > 36)
                    return "00";
                return "0";
            }               

            long currentNumber = Math.Abs(decimalNumber);
            digits.CurrentBase = radix;

            StringBuilder sb = new StringBuilder();

            while (currentNumber != 0)
            {
                // Reminder of the division of number by base is value at specified position of number in this base
                int remainder = (int)(currentNumber % radix);
                if (radix > 36)
                {
                    // Reverse the each digit before adding. It's needed for bases higher than 36
                    // The digits at positions are calculated from the smallest to biggest position
                    // and the order in number is from biggest to smallest, so after appending all positions
                    // whole number string must be reversed. That creates problem for bases, where the digits
                    // contain 2 characters. For example, let's say that the result of conversion to base 64 is 04 15 06.
                    // The number will be later reversed - 60 51 40, but we need to reverse digits, not 
                    // all the characters - the right result would be 06 15 04. If we reverse each digit first in 04 15 06, 
                    // before the final reverse we will have 40 51 60, and after we will have proper result 06 15 04

                    sb.Append(digits.GetDigit(remainder).ToCharArray().Reverse().ToArray());
                    // The String with 2 characters are more readible if there is space in between digits
                    sb.Append(" ");
                    
                    // The result of division, without reminder is passed to the next iteration of loop
                    currentNumber = currentNumber / radix;
                    continue;
                }
                sb.Append(digits.GetDigit(remainder));

                currentNumber = currentNumber / radix;
            }

            // Reverse the string 
            String result = new string(sb.ToString().ToCharArray().Reverse().ToArray());

            // There will be additional space at the front for some bases, remove it
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

        /// <summary>
        /// Converts string that represents fraction in specified base to it's value in decimal
        /// </summary>
        /// <param name="fractionStr"></param>
        /// <param name="radix"></param>
        /// <returns></returns>
        private decimal ArbitraryFractionToDecimal(string fractionStr, int radix)
        {
            decimal decimalFraction = 0.0m;
            double exponent = 1.0;

            var strList = ConversionHelpers.RepresentationStringToStringList(fractionStr, radix);
            digits.CurrentBase = radix;

            // The value at each position is calculated by taking the value of digit 
            // and multiplying it by the base of number to the power of exponent

            // The exponents at positions in fraction are as follows:
            // For fraction  0.253
            // Exponents:  0  . -1 -2 -3   
            // Digits:     0  .  5  3  1

            for (int i = 0; i < strList.Count; i++)
            {               
                decimalFraction += (decimal)(digits.GetValue(strList.ElementAt(i)) * Math.Pow(radix, exponent * -1));
                exponent++;
            }
            return decimalFraction;
        }
        /// <summary>
        /// Converts decimal fraction to it's string representation in specified base
        /// </summary>
        /// <param name="fraction">The fraction in decimal</param>
        /// <param name="radix">The radix</param>
        /// <returns></returns>
        private string DecimalFractionToArbitraryBase(decimal fraction, int radix)
        {
            if(fraction == 0.0m)
            {
                if (radix <= 36)
                    return "0";
                return "00";
            }

            StringBuilder builder = new StringBuilder();
            digits.CurrentBase = radix;

            decimal number, fractionPart;
            int wholePart;

            fractionPart = fraction;
            for (int i = 0; i < FormatPrecision; i++)
            {
                number = fractionPart * radix;
                fractionPart = number % 1;
                wholePart = (int)(number - fractionPart);

                if (wholePart < 1)
                    wholePart *= -1;

                string str = digits.GetDigit(wholePart);
                builder.Append(str);                 
                if (radix > 36)
                    builder.Append(" ");
            }
            return RemoveTrailingZeros(builder.ToString());
        }

        #endregion

        #region Helper Methods for Converting

       
        private static bool IsFloatingPointStr(string str) { return str.Contains("."); }

        private static string RemoveTrailingZeros(string str)
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
        private  string GetRoundingFormat() { return "0." + new string('#', FormatPrecision); }
        #endregion

      

        #region Input Validation
        /// <summary>
        /// Returns regex pattern matching characters in base 2-36
        /// </summary>
        /// <param name="inputBase">The base that specifies witch characters are avalible</param>
        /// <returns>Regex pattern that matches possible numbers in given base</returns>
        private  string GetRepresentationRegexPattern(int inputBase)
        {
            // Matching characters by regex is only supportet for bases 2 - 36 (10 digits and 26 alphabet characters)
            // Positions in higher bases are represented differently - not by characters but by numbers
            // for example base 64 number - 12 09 45
            if (inputBase > 36)
                throw new ArgumentException();
            else
            {
                String pattern = " ";
                digits.CurrentBase = inputBase;

                // For bases smaller or equal 10, we only have to match digits
                if (inputBase <= 10)
                {
                    // All characters that optionally start with -, are between 0 - given number 
                    // and might have . in between
                    pattern = "^-?[0-#]+([.][0-#]+)?$";
                    pattern = pattern.Replace('#', digits.GetDigit(inputBase - 1)[0]);
                }
                // For bases 10 - 36 we need to match digits and capital letters
                else
                {
                    // All characters that optionally start with -, are between 0 - 9 or A - last character of representaton 
                    // and might . in between
                    pattern = "^-?[0-9A-#]+([.][0-9A-#]+)?$";
                    pattern = pattern.Replace('#', digits.GetDigit(inputBase - 1)[0]);
                }
                return pattern;
            }    
        }

        public  bool IsValidRadix(int radix)
        {
            return radix >= 2 && radix < BaseDigits.MAX_BASE;
        }

        public  bool IsValidString(string str, int radix)
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
