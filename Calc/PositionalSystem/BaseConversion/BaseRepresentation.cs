
namespace Calc.PositionalSystem
{
    public class BaseRepresentation
    {
        private int systemBase;

        private double valueInDecimal;
        private string valueInBase;     

        private BaseComplement complement;
   

        /// <summary>
        /// The Radix of number, represented by decimal integer.
        /// </summary>
        public int Radix { get { return systemBase; }  set { systemBase = value; } }

        /// <summary>
        /// The integer part of number in decimal system.
        /// </summary>
        public double IntegerPartDecimalValue { get { return valueInDecimal - FractionPartDecimalValue; } }
        /// <summary>
        /// The fraction part of number in decimal system.
        /// </summary>
        public double FractionPartDecimalValue { get { return valueInDecimal % 1; } }
        /// <summary>
        /// The decimal value of Number.
        /// </summary>
        public double DecimalValue { get { return valueInDecimal; }  set { valueInDecimal = value; } }

        /// <summary>
        /// The integer part of Number in given positional System, represented by String
        /// </summary>
        public string IntegerPartBaseValue { get { return valueInBase.Split('.')[0]; } }
        /// <summary>
        /// The fraction part of Number in given base, represented by String. This field does not contain the delimeter.
        /// </summary>
        public string FractionPartBaseValue { get { return valueInBase.Split('.')[1]; } }
        /// <summary>
        /// The string representing value of number in given base. Field concats strings IntegerPart, FractionalPart and adds the . delimeter in between
        /// </summary>
        public string ValueInBase { get { return valueInBase; }  set { valueInBase = value; } }

        public string Complement { get { return complement.Prefix + complement.Value; } }
        public string ComplementIntegerPart { get { return complement.IntegerPart; } }
        public string ComplementFractionPart { get { return complement.FractionPart; } }


        public BaseRepresentation(int radix, double decimalValue, string baseSystemValueStr, BaseComplement comp)
        {
            Radix = radix;
            DecimalValue = decimalValue;
            ValueInBase = baseSystemValueStr;
            complement = comp;
        }
    }


}
