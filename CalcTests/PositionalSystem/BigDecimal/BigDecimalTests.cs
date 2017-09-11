using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using System.Data;

namespace Calc.PositionalSystem.BigDecimal.Tests
{
    [TestClass()]
    public class BigDecimalTests
    {

        public TestContext TestContext { get; set; }

        #region Construction Tests
        [TestMethod()]
        public void BigDecimal_IntegerConstructor_Pass()
        {
            var num = new BigDecimal(Int32.MaxValue);
            var expected = new BigInteger(Int32.MaxValue);
            var actual = num.UnscaledValue;
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(num.Scale, 0);
        }

        [TestMethod()]
        public void BigDecimal_StringConstructor_Pass()
        {
            var num = new BigDecimal("123123612733313124");
            var expected = BigInteger.Parse("123123612733313124");
            var actual = num.UnscaledValue;
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(num.Scale, 0);
        }

        [TestMethod()]
        public void BigDecimal_StringConstructorSmallF4atingPointNumber_Pass()
        {
            var num = new BigDecimal("12.3123");
            var expected = BigInteger.Parse("123123");
            var actual = num.UnscaledValue;
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(4, num.Scale);
        }

        [TestMethod()]
        public void BigDecimal_StringConstructorBigFloatingPointNumber_Pass()
        {
            var num = new BigDecimal("123121641489189271742162412.4512");
            var expected = BigInteger.Parse("1231216414891892717421624124512");
            var actual = num.UnscaledValue;
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(4, num.Scale);
            Assert.AreEqual("123121641489189271742162412.4512", num.ToString());
        }

        #endregion


        // MethodName_StateUnderTest_ExpectedBehaviour

        // Math Operation Test Cases for operations with 2 operands
        // 1. Two positive values
        // 2. Two negative values
        // 3. One Positive, One Negative
        // 4. One Positive Value and Zero
        // 5. One Negative Value and Zero
        // 6. Two Zeroes

        // Separate for big numbers and small numbers


        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"TestData\BigDecimalAddTestCases.csv", "BigDecimalAddTestCases#csv", DataAccessMethod.Sequential)]
        public void Add_ParametersFromData_Pass()
        {
            var left = new BigDecimal(TestContext.DataRow[0].ToString());
            var right = new BigDecimal(TestContext.DataRow[1].ToString());
            var expected = TestContext.DataRow[2].ToString();
            string message = TestContext.DataRow[3].ToString();

            var actual = left.Add(right);
            Assert.AreEqual(expected, actual.ToString(), message);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"TestData\BigDecimalAddTestCases.csv", "BigDecimalSubtractTestCases#csv", DataAccessMethod.Sequential)]
        public void Subtract_ParametersFromData_Pass()
        {
            var left = new BigDecimal(TestContext.DataRow[0].ToString());
            var right = new BigDecimal(TestContext.DataRow[1].ToString());
            var expected = TestContext.DataRow[2].ToString();
            string message = TestContext.DataRow[3].ToString();

            var actual = left.Subtract(right);
            Assert.AreEqual(expected, actual.ToString(), message);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"TestData\BigDecimalAddTestCases.csv", "BigDecimalMultiplyTestCases#csv", DataAccessMethod.Sequential)]
        public void Multiply_ParametersFromData_Pass()
        {
            var left = new BigDecimal(TestContext.DataRow[0].ToString());
            var right = new BigDecimal(TestContext.DataRow[1].ToString());
            var expected = TestContext.DataRow[2].ToString();
            string message = TestContext.DataRow[3].ToString();

            var actual = left.Multiply(right);
            Assert.AreEqual(expected, actual.ToString(), message);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"TestData\BigDecimalAddTestCases.csv", "BigDecimalDivideTestCases#csv", DataAccessMethod.Sequential)]
        public void Divide_ParametersFromData_Pass()
        {
            var left = new BigDecimal(TestContext.DataRow[0].ToString());
            var right = new BigDecimal(TestContext.DataRow[1].ToString());
            var expected = TestContext.DataRow[2].ToString();
            string message = TestContext.DataRow[3].ToString();

            var actual = left.Divide(right, RoundingMode.Up);
            Assert.AreEqual(expected, actual.ToString(), message);
        }



       

        [TestMethod()]
        public void BigDecimal_ToStringExactValue_Pass()
        {
            var num = new BigDecimal("0.1");
            var actual = num.ToString();
            var expected = "0.1";
            Assert.AreEqual(expected, actual);

            var expectedDouble = num.ToDouble();
            Assert.AreEqual(expectedDouble, 0.1);
        }



        [TestMethod()]
        public void Compare_MultipleValues_Pass()
        {
            var num1 = new BigDecimal(20);
            var num2 = new BigDecimal(30);
            var actual = num1.CompareTo(num2);
            var expected = -1;
            Assert.AreEqual(expected, actual);

            actual = num1.CompareTo(num1);
            expected = 0;
            Assert.AreEqual(expected, actual);

            actual = num2.CompareTo(num1);
            expected = 1;
            Assert.AreEqual(expected, actual);
        }
       


    }
}