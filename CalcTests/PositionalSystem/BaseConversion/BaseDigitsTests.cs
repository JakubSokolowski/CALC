using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Calc.PositionalSystem.Tests
{
    [TestClass()]
    public class BaseDigitsTests
    {
        #region Constructor Tests

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void BaseDigits_RadixTooBig_ThrowArgumentException()
        {
            BaseDigits digits = new BaseDigits(100);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void BaseDigits_RadixTooSmall_ThrowArgumentException()
        {
            BaseDigits digits = new BaseDigits(1);
        }

        #endregion

        #region GetDigit Tests

        [TestMethod()]
        public void GetDigit_Sub36Radix_Pass()
        {
            BaseDigits digits = new BaseDigits();
            digits.CurrentBase = 16;
            var expected = "A";
            var actual = digits.GetDigit(10);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetDigit_MoreThan36Radix_Pass()
        {
            BaseDigits digits = new BaseDigits();
            digits.CurrentBase = 64;
            var expected = "10";
            var actual = digits.GetDigit(10);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDigit_ValueNotInRange_ThrowArgumentExepction()
        {
            BaseDigits digits = new BaseDigits();
            digits.CurrentBase = 10;            
            var actual = digits.GetDigit(16);           
        }

      

        #endregion

        #region GetValue Tests

        [TestMethod()]
        public void GetValue_Sub36Radix_Pass()
        {
            BaseDigits digits = new BaseDigits();
            digits.CurrentBase = 16;
            var expected = 10;
            var actual = digits.GetValue("A");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetValue_MoreThan36Radix_Pass()
        {
            BaseDigits digits = new BaseDigits();
            digits.CurrentBase = 64;
            var expected = 10;
            var actual = digits.GetValue("10");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.Collections.Generic.KeyNotFoundException))]
        public void GetValue_KeyNotInDictionary_ThrowArgumentExepction()
        {
            BaseDigits digits = new BaseDigits();
            digits.CurrentBase = 10;
            var actual = digits.GetValue("AAA");
        }

        [TestMethod()]
        [ExpectedException(typeof(System.Collections.Generic.KeyNotFoundException))]
        public void GetValue_ValueHigherThanBase_ThrowArgumentExepction()
        {
            BaseDigits digits = new BaseDigits();
            digits.CurrentBase = 10;
            var actual = digits.GetValue("10");
        }

        #endregion       
    }
}