using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calc.PositionalSystem.Tests
{
    [TestClass()]
    public class BaseConverterTests
    {
        public BaseConverter bc = new BaseConverter();

        #region InputValidationTests
        [TestMethod()]
        public void IsValidRadix_ValidRadix_Pass()
        {
            int radix = 16;
            bool expected = true;
            bool actual = bc.IsValidRadix(radix);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidRadix_InvalidRadix_Fail()
        {
            int radix = -5;
            bool expected = false;
            bool actual = bc.IsValidRadix(radix);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidString_ValidString_Pass()
        {
            string str = "-FFFFA.6556A";
            int radix = 16;
            bool expected = true;
            bool actual = bc.IsValidString(str, radix);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidString_InvalidString_Fail()
        {
            string str = "ZZZa asd1  sad";
            int radix = 10;
            bool expected = false;
            bool actual = bc.IsValidString(str, radix);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidString_InvalidRadixForStr_Fail()
        {
            string str = "AABFBAA.FF";
            int radix = 15;
            bool expected = false;
            bool actual = bc.IsValidString(str, radix);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void IsValidString_MultipleSign_Fail()
        {
            string str = "--1234.-230";
            int radix = 10;
            bool expected = false;
            bool actual = bc.IsValidString(str, radix);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidString_MultipleDot_Fail()
        {
            string str = "1234..230";
            int radix = 10;
            bool expected = false;
            bool actual = bc.IsValidString(str, radix);
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region ConverterTests

        // Test cases for number conversion :
        //
        // 1. Positive Decimal to base Number
        // 2. Negative Decimal to base Number
        // 3. Floating Point Decimal to base Number
        // 4. Negative Floating Point Decimal to base Number
        // 5. Positive base Number to Decimal
        // 6. Negative base Number to Decimal
        // 7. Floating Point base Number to Decimal
        // 8. Negative Floating Point base nUmber to Decimal

        #region Converting Binary

        [TestMethod()]
        public void ToBase_PositveDecimalToBinary_Pass()
        {
            int input = 25;
            string expected = "11001.0";
            int radix = 2;
            string result = bc.ToBase(input, radix).ValueInBase;
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void ToBase_NegativeDecimalToBinary_Pass()
        {
            int input = -25;
            string expected = "-11001.0";
            string expectedComplement = "(1)00111.0";
            int radix = 2;
            BaseRepresentation result = bc.ToBase(input, radix);
            Assert.AreEqual(expected, result.ValueInBase);
            Assert.AreEqual(expectedComplement, result.Complement);
        }
        [TestMethod()]
        public void ToBase_PositiveFloatingPointDecimalToBinary_Pass()
        {
            var input = 25.5;
            string expected = "11001.1";
            string expectedComplement = "(0)11001.1";
            int radix = 2;
            BaseRepresentation result = bc.ToBase(input, radix);
            Assert.AreEqual(expected, result.ValueInBase);
            Assert.AreEqual(expectedComplement, result.Complement);
        }
        [TestMethod()]
        public void ToBase_NegativeFloatingPointDecimalToBinary_Pass()
        {
            var input = -25.5;
            string expected = "-11001.1";
            string expectedComplement = "(1)00110.1";
            int radix = 2;
            BaseRepresentation result = bc.ToBase(input, radix);
            Assert.AreEqual(expected, result.ValueInBase);
            Assert.AreEqual(expectedComplement, result.Complement);
        }
        [TestMethod()]
        public void ToBase_PositiveBinaryToDecimal_Pass()
        {
            string input = "11010";
            var expected = 26.0;
            var result = bc.ToBase(input, 2, 10).DecimalValue;
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void ToBase_NegativeBinaryToDecimal_Pass()
        {
            string input = "-11010";
            var expected = -26.0;
            var result = bc.ToBase(input, 2, 10).DecimalValue;
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void ToBase_PositiveFloatingPointBinaryToDecimal_Pass()
        {
            string input = "11010.1";
            var expected = 26.5;
            var result = bc.ToBase(input, 2, 10).DecimalValue;
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void ToBase_NegativeFloatingPointBinaryToDecimal_Pass()
        {
            string input = "-11010.1";
            var expected = -26.5;
            var result = bc.ToBase(input, 2, 10).DecimalValue;
            Assert.AreEqual(expected, result);
        }


        #endregion

        #region Converting Hexadecimal

        [TestMethod()]
        public void ToBase_PositveDecimalToHex_Pass()
        {
            int input = 255;
            string expected = "FF.0";
            int radix = 16;
            string result = bc.ToBase(input, radix).ValueInBase;
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void ToBase_NegativeDecimalToHex_Pass()
        {
            int input = -255;
            string expected = "-FF.0";
            int radix = 16;
            string result = bc.ToBase(input, radix).ValueInBase;
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void ToBase_PositiveFloatingPointDecimalToHex_Pass()
        {
            var input = 255.5;
            string expected = "FF.8";
            int radix = 16;
            string result = bc.ToBase(input, radix).ValueInBase;
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void ToBase_NegativeFloatingPointDecimalToHex_Pass()
        {
            var input = -255.5;
            string expected = "-FF.8";
            int radix = 16;
            string result = bc.ToBase(input, radix).ValueInBase;
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void ToBase_PositiveHexToDecimal_Pass()
        {
            string input = "FF";
            var expected = 255;
            var result = bc.ToBase(input, 16, 10).DecimalValue;
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void ToBase_NegativeHexToDecimal_Pass()
        {
            string input = "-FF";
            var expected = -255;
            var result = bc.ToBase(input, 16, 10).DecimalValue;
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void ToBase_PositiveFloatingPointHexToDecimal_Pass()
        {
            string input = "FF.8";
            var expected = 255.5;
            var result = bc.ToBase(input, 16, 10).DecimalValue;
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void ToBase_NegativeFloatingPointHexToDecimal_Pass()
        {
            string input = "-FF.8";
            var expected = -255.5;
            var result = bc.ToBase(input, 16, 10).DecimalValue;
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region Converting Base 64

        [TestMethod()]
        public void ToBase_PositveDecimalToBase64_Pass()
        {
            int input = 100;
            string expected = "01 36.00";
            string expectedComplement = "(00)01 36.00";
            int radix = 64;
            BaseRepresentation result = bc.ToBase(input, radix);
            Assert.AreEqual(expected, result.ValueInBase);
            Assert.AreEqual(expectedComplement, result.Complement);
        }
        [TestMethod()]
        public void ToBase_NegativeDecimalToBase64_Pass()
        {
            int input = -100;
            string expected = "-01 36.00";
            string expectedComplement = "(63)62 28.00";
            int radix = 64;
            BaseRepresentation result = bc.ToBase(input, radix);
            Assert.AreEqual(expected, result.ValueInBase);
            Assert.AreEqual(expectedComplement, result.Complement);
        }
        [TestMethod()]
        public void ToBase_PositiveFloatingPointDecimalToBase64_Pass()
        {
            var input = 100.5;
            string expected = "01 36.32";
            string expectedComplement = "(00)01 36.32";
            int radix = 64;
            BaseRepresentation result = bc.ToBase(input, radix);
            Assert.AreEqual(expected, result.ValueInBase);
            Assert.AreEqual(expectedComplement, result.Complement);
        }
        [TestMethod()]
        public void ToBase_NegativeFloatingPointDecimalToBase64_Pass()
        {
            var input = -100.5;
            string expected = "-01 36.32";
            string expectedComplement = "(63)62 27.32";
            int radix = 64;
            BaseRepresentation result = bc.ToBase(input, radix);
            Assert.AreEqual(expected, result.ValueInBase);
            Assert.AreEqual(expectedComplement, result.Complement);
        }
        [TestMethod()]
        public void ToBase_PositiveBase64ToDecimal_Pass()
        {
            string input = "01 36";
            var expected = 100;
            var result = bc.ToBase(input, 64, 10).DecimalValue;
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void ToBase_NegativeBase64ToDecimal_Pass()
        {
            string input = "-01 36";
            var expected = -100;
            var result = bc.ToBase(input, 64, 10).DecimalValue;
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void ToBase_PositiveFloatingPointBase64ToDecimal_Pass()
        {
            string input = "01 36.32";
            var expected = 100.5;
            var result = bc.ToBase(input, 64, 10).DecimalValue;
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void ToBase_NegativeFloatingPointBase64ToDecimal_Pass()
        {
            string input = "-01 36.32";
            var expected = -100.5;
            var result = bc.ToBase(input, 64, 10).DecimalValue;
            Assert.AreEqual(expected, result);
        }
        #endregion

        #region Converting Integer Part

        #endregion

        #region Converting Fractional Part


        #endregion

        #endregion





    }
}