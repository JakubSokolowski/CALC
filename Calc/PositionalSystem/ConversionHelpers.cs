using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.PositionalSystem
{
    public static class ConversionHelpers
    {
        public static List<string> RepresentationStringToStringList(string str, int radix)
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
    }
}
