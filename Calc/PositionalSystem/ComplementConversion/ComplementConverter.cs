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

        #region Number Complement

        public BaseComplement GetComplement(string number, int radix)
        {
            representation.CurrentBase = radix;

            string prefix = "";

            if (number[0] == '-')
            {
                prefix = "(" + (radix - 1).ToString() + ")";
                number = number.Substring(1);
            }
            else
            {
                if (radix > 36)
                    return new BaseComplement(number, "(00)");
                return new BaseComplement(number, "(0)");
            }


            int dotIndex = number.IndexOf('.');

            if (radix > 36)
                number = number.Replace(".", " ");
            else
                number = number.Replace(".", String.Empty);


            var strList = ConversionHelpers.RepresentationStringToStringList(number, radix);
            strList.RemoveAll(x => x == " ");

            for (int i = 0; i < strList.Count; i++)
                strList[i] = GetDigitComplement(strList[i], radix);

            var complement = new StringBuilder(IncrementNumber(strList, radix));

            if (dotIndex < 0)
            {
                if (radix > 36)
                    return new BaseComplement(complement.ToString() + "00", prefix);
                else
                    return new BaseComplement(complement.ToString() + "0", prefix);
            }

            if (radix > 36)
                complement.Remove(dotIndex, 1);

            complement.Insert(dotIndex, ".");
            return new BaseComplement(complement.ToString(), prefix);
        }

        public string GetDigitComplement(string digit, int radix)
        {
            return representation.GetDigit(radix - 1 - representation.GetValue(digit));
        }
        public string IncrementNumber(List<string> number, int radix)
        {
            representation.CurrentBase = radix;

            for (int i = number.Count - 1; i != 0; i--)
            {
                int value = representation.GetValue(number.ElementAt(i));

                if (value == radix - 1)
                {
                    number[i] = representation.GetDigit(0);
                    continue;
                }
                else
                {
                    number[i] = representation.GetDigit(value + 1);
                    break;
                }
            }

            if (radix > 36)
                return string.Join(" ", number);
            else
                return string.Join(String.Empty, number);

        }
        #endregion
    }
}
