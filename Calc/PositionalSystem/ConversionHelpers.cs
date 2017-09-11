using System;
using System.Collections.Generic;
using System.Linq;

namespace Calc.PositionalSystem
{
    public static class ConversionHelpers
    {
        public static List<string> RepresentationStringToListOfStrings(string str, int radix)
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

        public static string RemoveTrailingZeros(string str)
        {
            
            str = str.TrimEnd('0');

            //If all we are left with is a decimal point
            if (str.EndsWith(".")) //then remove it
                str = str.TrimEnd('.');
            

            return str;
        }

    }
}
