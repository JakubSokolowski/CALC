using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.PositionalSystem
{
    public class ComplementConverter
    {
        private BaseDigits representation = new BaseDigits();
        

        public BaseComplement GetComplement(string number, int radix)
        {            
            if(IsNegativeNumberString(number))            
                return GetNegativeNumberComplement(number, radix);            
            else            
                return GetPositiveNumberComplement(number, radix); 
        }

        public BaseComplement GetPositiveNumberComplement(string number, int radix)
        {            
            string prefix = GetPrefix(number, radix);
            string value = number;
            string sufix = GetSufix(number, radix);

            return new BaseComplement(value + sufix, prefix);
        }

        public BaseComplement GetNegativeNumberComplement(string number, int radix)
        {
            representation.CurrentBase = radix;

            string prefix = GetPrefix(number,radix);
            string value = number.Substring(1);
            string sufix = GetSufix(number, radix);
            int delimeterIndex;

            var strList = ConvertToDigitListAndRemoveEmptyDigits(value, radix, out delimeterIndex);
            var complement = CalculateComplement(ref strList, radix);

            if(sufix == String.Empty)
                complement = RestoreDelimeter(complement, radix, delimeterIndex);

            return new BaseComplement(complement + sufix, prefix);
        }  

        #region Input Checks

        public bool IsFloatingPointNumberString(string number)
        {
            return number.Contains(".");
        }

        public bool IsNegativeNumberString(string number)
        {
            return number[0] == '-';
        }

        #endregion

        #region Sufix and Prefix

        public string GetSufix(string number, int radix)
        {
            representation.CurrentBase = radix;
            if (IsFloatingPointNumberString(number))
                return String.Empty;
            return "." + representation.GetDigit(0);
        }

        public string GetPrefix(string number, int radix)
        {
            representation.CurrentBase = radix;

            if (IsNegativeNumberString(number))
                return "(" + representation.GetDigit(radix - 1) + ")";
            else
                return "(" + representation.GetDigit(0) + ")";
        }

        public string RemoveDelimeter(string number, int radix, out int delimeterIndex)
        {
            // When calculating the number complement, the position of delimeter
            // does not matter, and the delimeter must be removed from string, but after
            // the delimeter must put in the right place, so remember its index            

            delimeterIndex = number.IndexOf('.');

            // If number is not floating point, delimeter goes after last character in string
            if (delimeterIndex < 0)
                delimeterIndex = number.Length - 1;

            if (radix > 36)
                number = number.Replace(".", " ");
            else
                number = number.Replace(".", String.Empty);

            return number;
        }

        public string RestoreDelimeter(string complement, int radix, int delimeterIndex)
        {
            var sb = new StringBuilder(complement);
            if (radix > 36)
                sb.Remove(delimeterIndex, 1);
            sb.Insert(delimeterIndex, ".");
            return sb.ToString();
        }

        #endregion

        #region Complement Helpers

        public List<string> ConvertToDigitListAndRemoveEmptyDigits(string number, int radix, out int delimeterIndex)
        {
            var value = RemoveDelimeter(number, radix, out delimeterIndex);
            var digitList = ConversionHelpers.RepresentationStringToStringList(value, radix);
            digitList.RemoveAll(x => x == " ");
            return digitList;
        }

        public string GetDigitComplement(string digit, int radix)
        {
            return representation.GetDigit(radix - 1 - representation.GetValue(digit));
        }

        public string IncrementNumber(List<string> digitList, int radix)
        {
            representation.CurrentBase = radix;

            for (int i = digitList.Count - 1; i >= 0; i--)
            {
                int value = representation.GetValue(digitList.ElementAt(i));

                if (value == (radix - 1))
                {
                    digitList[i] = representation.GetDigit(0);
                    continue;
                }
                else
                {
                    digitList[i] = representation.GetDigit(value + 1);
                    break;
                }
            }

            if (radix > 36)
                return string.Join(" ", digitList);
            else
                return string.Join(String.Empty, digitList);
        }      

        public string CalculateComplement(ref List<string> strList, int radix)
        {
            for (int i = 0; i < strList.Count; i++)
                strList[i] = GetDigitComplement(strList[i], radix);

            return IncrementNumber(strList, radix);
        }      

      
        #endregion
        
    }
}
