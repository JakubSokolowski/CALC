﻿using Calc.FloatingPointNumbers;


namespace Calc.PositionalSystem
{
    public static class NumberConverter
    {
        private static FloatConverter fc = new FloatConverter();
        private static ComplementConverter cc = new ComplementConverter();
        private static BaseConverter bc = new BaseConverter();
    
        public static Number ToBase(double value, int resultBase)
        {
            var baseRep = bc.ToBase(value, resultBase);
            var sRep = fc.ToSingle((float)value);
            var dRep = fc.ToDouble((double)value);
            return new Number(baseRep, sRep, dRep);
        }    
        public static Number ToBase(string valueString, int inputBase, int resultBase)
        {
            var baseRep = bc.ToBase(valueString, inputBase, resultBase);
            var sRep = fc.ToSingle((float)baseRep.DecimalValue);
            var dRep = fc.ToDouble((double)baseRep.DecimalValue);
            return new Number(baseRep, sRep, dRep);
        }   

        public static string MaxValueForBase(int radix)
        {
            return bc.DecimalIntegerToArbitraryBase(long.MaxValue - 1, radix);
        }
    }
}
