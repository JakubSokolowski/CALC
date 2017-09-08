using System;

namespace Calc.PositionalSystem.BigDecimal
{
    public class MathContext
    {
        private int mPrecision;
        private RoundingMode mRoundingMode;

        public RoundingMode RoundingMode => mRoundingMode;
        public int Precision => mPrecision;

        public static MathContext Unlimited { get; } = new MathContext(0, RoundingMode.HalfUp);
        public static MathContext Decimal32 { get; } = new MathContext(7, RoundingMode.HalfEven);
        public static MathContext Decimal64 { get; } = new MathContext(16, RoundingMode.HalfEven);
        public static MathContext Decimal128 { get; } = new MathContext(34, RoundingMode.HalfEven);


        public MathContext(int precision) 
            : this(precision, RoundingMode.HalfUp)
        {
            
        }
        public MathContext(int precision, RoundingMode roundingMode)
        {
            if (precision < 0)
                throw new ArgumentException("Precision cannot be less than zero");
            mPrecision = precision;
            mRoundingMode = roundingMode;
        }      

    }
}
