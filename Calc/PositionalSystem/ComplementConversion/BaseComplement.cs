using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.PositionalSystem
{
    public class BaseComplement
    {
        private string valueString;
        private string prefix;

        public string Prefix { get => prefix; set => prefix = value; }
        public string Value { get => valueString; set => valueString = value ; }
        public string IntegerPart { get => Value.Split('.')[0];  }
        public string FractionPart { get => Value.Split('.')[1]; }

        public BaseComplement(string value, string prefix)
        {
            Value = value;
            Prefix = prefix;
        }
    }
}
