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
        [TestMethod()]
        public void isValidRadixTest_ValidRadix_Pass()
        {
            int radix = 16;
            bool expected = true;
            bool actual = BaseConverter.isValidRadix(radix);
            Assert.AreEqual(expected, actual);           
        }

        [TestMethod()]
        public void isValidRadix_InvalidRadix_Fail()
        {
            int radix = -5;
            bool expected = false;
            bool actual = BaseConverter.isValidRadix(radix);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void isValidStringTest_ValidString_Pass()
        {
            string str = "-FFFFA.6556A";
            int radix = 16;
            bool expected = true;
            bool actual = BaseConverter.isValidString(str, radix);
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod()]
        public void isValidStringTest_InvalidString_Fail()
        {
            string str = "ZZZa asd1  sad";
            int radix = 10;
            bool expected = false;
            bool actual = BaseConverter.isValidString(str, radix);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void isValidStringTest_InvalidRadixForStr_Fail()
        {
            string str = "AABFBAA.FF";
            int radix = 15;
            bool expected = false;
            bool actual = BaseConverter.isValidString(str, radix);
            Assert.AreEqual(expected, actual);
        }
    }
}