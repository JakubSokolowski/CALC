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
    public class NumberTests
    {
        #region Math Operations Tests
        // Math Operation Test Cases for operations with 2 operands
        // 1. Two positive values
        // 2. Two negative values
        // 3. One Positive, One Negative
        // 4. One Positive Value and Zero
        // 5. One Negative Value and Zero
        // 6. Two Zeroes
        // 7. Overflow Check


        #region Addition Tests
        [TestMethod()]
        public void Addition_TwoPositive_Pass()
        {
            var left = 10;
            var right = 20.5;
            var num1 = BaseConverter.ConvertToBase(left, 10);
            var num2 = BaseConverter.ConvertToBase(right, 16);
            var num3 = num1 + num2;

            var expected = 30.5;

            Assert.AreEqual(num3.DecimalValue, expected);
        }

        [TestMethod()]
        public void Addition_TwoNegative_Pass()
        {
            var left = -10.0;
            var right = -20.5;
            var num1 = BaseConverter.ConvertToBase(left, 10);
            var num2 = BaseConverter.ConvertToBase(right, 16);
            var num3 = num1 + num2;

            var expected = -30.5;

            Assert.AreEqual(num3.DecimalValue, expected);
        }

        [TestMethod()]
        public void Addition_PositiveNegatice_Pass()
        {

            var left = -10.0;
            var right = 20.5;
            var num1 = BaseConverter.ConvertToBase(left,10);
            var num2 = BaseConverter.ConvertToBase(right,10);
            var num3 = num1 + num2;

            var expected = 10.5;

            Assert.AreEqual(num3.DecimalValue, expected);
        }

        [TestMethod()]
        public void Addition_PositiveZero_Pass()
        {
            var left = 10;
            var right = 0.0;
            var num1 = BaseConverter.ConvertToBase(left, 10);
            var num2 = BaseConverter.ConvertToBase(right, 10);
            var num3 = num1 + num2;

            var expected = 10.0;

            Assert.AreEqual(num3.DecimalValue, expected);
        }

        [TestMethod()]
        public void Addition_NegativeZero_Pass()
        {
            var left = -10;
            var right = 0;
            var num1 = BaseConverter.ConvertToBase(left, 10);
            var num2 = BaseConverter.ConvertToBase(right, 10);
            var num3 = num1 + num2;

            var expected = -10;

            Assert.AreEqual(num3.DecimalValue, expected);
        }

        [TestMethod()]
        public void Addition_TwoZeros_Pass()
        {
            var left = 0;
            var right = 0;
            var num1 = BaseConverter.ConvertToBase(left, 10);
            var num2 = BaseConverter.ConvertToBase(right, 10);
            var num3 = num1 + num2;

            var expected = 0.0;

            Assert.AreEqual(num3.DecimalValue, expected);
        }

        [TestMethod()]
        [ExpectedException(typeof(OverflowException))]
        public void Addition_Overflow_Fail()
        {
            var left = long.MaxValue;
            var right = long.MaxValue;
            var num1 = BaseConverter.ConvertToBase(left, 10);
            var num2 = BaseConverter.ConvertToBase(right, 10);
            var num3 = num1 + num2;
        }

        #endregion
        #region Substraction Tests

        [TestMethod()]
        public void Substraction_TwoPositive_Pass()
        {
            var left = 20.5;
            var right = 10.5;
            var num1 = BaseConverter.ConvertToBase(left, 10);
            var num2 = BaseConverter.ConvertToBase(right, 16);
            var num3 = num1 - num2;

            var expected = 10.0;

            Assert.AreEqual(num3.DecimalValue, expected);
        }

        [TestMethod()]
        public void Substraction_TwoNegative_Pass()
        {
            var left = -10.0;
            var right = -20.5;
            var num1 = BaseConverter.ConvertToBase(left, 10);
            var num2 = BaseConverter.ConvertToBase(right, 16);
            var num3 = num1 - num2;

            var expected = 10.5;

            Assert.AreEqual(num3.DecimalValue, expected);
        }

        [TestMethod()]
        public void Substraction_PositiveNegatice_Pass()
        {

            var left = -10.0;
            var right = 20.5;
            var num1 = BaseConverter.ConvertToBase(left, 10);
            var num2 = BaseConverter.ConvertToBase(right, 10);
            var num3 = num1 - num2;

            var expected = -30.5;

            Assert.AreEqual(num3.DecimalValue, expected);
        }

        [TestMethod()]
        public void Substraction_PositiveZero_Pass()
        {
            var left = 10;
            var right = 0.0;
            var num1 = BaseConverter.ConvertToBase(left, 10);
            var num2 = BaseConverter.ConvertToBase(right, 10);
            var num3 = num1 - num2;

            var expected = 10.0;

            Assert.AreEqual(num3.DecimalValue, expected);
        }

        [TestMethod()]
        public void Substraction_NegativeZero_Pass()
        {
            var left = -10;
            var right = 0;
            var num1 = BaseConverter.ConvertToBase(left, 10);
            var num2 = BaseConverter.ConvertToBase(right, 10);
            var num3 = num1 - num2;

            var expected = -10;

            Assert.AreEqual(num3.DecimalValue, expected);
        }

        [TestMethod()]
        public void Substraction_TwoZeros_Pass()
        {
            var left = 0;
            var right = 0;
            var num1 = BaseConverter.ConvertToBase(left, 10);
            var num2 = BaseConverter.ConvertToBase(right, 10);
            var num3 = num1 - num2;

            var expected = 0.0;

            Assert.AreEqual(num3.DecimalValue, expected);
        }

        [TestMethod()]
        [ExpectedException(typeof(OverflowException))]
        public void Substraction_Overflow_Fail()
        {
            var left = long.MinValue + 100;
            var right = 10000;
            var num1 = BaseConverter.ConvertToBase(left, 10);
            var num2 = BaseConverter.ConvertToBase(right, 10);
            var num3 = num1 - num2;
        }

        #endregion

        #endregion

    }
}