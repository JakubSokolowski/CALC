using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calc.PositionalSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.PositionalSystem.Tests
{
    [TestClass()]
    public class BaseConverterTests
    {
        #region InputValidationTests
        [TestMethod()]
        public void IsValidRadix_ValidRadix_Pass()
        {
            int radix = 16;
            bool expected = true;
            bool actual = BaseConverter.IsValidRadix(radix);
            Assert.AreEqual(expected, actual);           
        }

        [TestMethod()]
        public void IsValidRadix_InvalidRadix_Fail()
        {
            int radix = -5;
            bool expected = false;
            bool actual = BaseConverter.IsValidRadix(radix);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidString_ValidString_Pass()
        {
            string str = "-FFFFA.6556A";
            int radix = 16;
            bool expected = true;
            bool actual = BaseConverter.IsValidString(str, radix);
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod()]
        public void IsValidString_InvalidString_Fail()
        {
            string str = "ZZZa asd1  sad";
            int radix = 10;
            bool expected = false;
            bool actual = BaseConverter.IsValidString(str, radix);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidString_InvalidRadixForStr_Fail()
        {
            string str = "AABFBAA.FF";
            int radix = 15;
            bool expected = false;
            bool actual = BaseConverter.IsValidString(str, radix);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void IsValidString_MultipleSign_Fail()
        {
            string str = "--1234.-230";
            int radix = 10;
            bool expected = false;
            bool actual = BaseConverter.IsValidString(str, radix);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidString_MultipleDot_Fail()
        {
            string str = "1234..230";
            int radix = 10;
            bool expected = false;
            bool actual = BaseConverter.IsValidString(str, radix);
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region ConverterTests
        [TestMethod()]
        public void Convert_ValidNumberToBinary_Pass()
        {
            string expected = "11001.0";
            int radix = 2;
            BaseConverter conv = new BaseConverter();
            string result = conv.Convert(25, radix).BaseValueString;
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void Convert_ValidNegativeNumberToBinary_Pass()
        {
            string expected = "-11001.0";
            int radix = 2;
            BaseConverter conv = new BaseConverter();
            string result = conv.Convert(-25, radix).BaseValueString;
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void Convert_ValidBinaryStrToDecimal_Pass()
        {
            string input = "11010";       
            double expected = 26.0;

            BaseConverter conv = new BaseConverter();
            double result = conv.Convert(input, 2, 10).DecimalValue;
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void Convert_ValidNegativeBinaryStrToDecimal_Pass()
        {
            string input = "-11010";
            double expected = -26.0;

            BaseConverter conv = new BaseConverter();
            double result = conv.Convert(input, 2, 10).DecimalValue;
            Assert.AreEqual(expected, result);
        }

        #endregion
    }
}