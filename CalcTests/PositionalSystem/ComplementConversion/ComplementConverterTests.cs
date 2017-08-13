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
    public class ComplementConverterTests
    {
        [TestMethod()]
        public void GetComplement_PositiveDecimalNumber_Pass()
        {
            ComplementConverter conv = new ComplementConverter();
            string input = "200";
            int radix = 10;
            string expectedValue = "200.0";
            string expectedPrefix = "(0)";
            BaseComplement complement = conv.GetComplement(input, radix);
            Assert.AreEqual(expectedValue, complement.Value);
            Assert.AreEqual(expectedPrefix, complement.Prefix);
        }

        [TestMethod()]
        public void GetComplement_PositiveDecimalNumberWithZeroFloatingPart_Pass()
        {
            ComplementConverter conv = new ComplementConverter();
            string input = "200.0";
            int radix = 10;
            string expectedValue = "200.0";
            string expectedPrefix = "(0)";
            BaseComplement complement = conv.GetComplement(input, radix);
            Assert.AreEqual(expectedValue, complement.Value);
            Assert.AreEqual(expectedPrefix, complement.Prefix);
        }

        [TestMethod()]
        public void GetComplement_NegativeDecimalNumber_Pass()
        {
            ComplementConverter conv = new ComplementConverter();
            string input = "-200";
            int radix = 10;
            string expectedValue = "800.0";
            string expectedPrefix = "(9)";
            BaseComplement complement = conv.GetComplement(input, radix);
            Assert.AreEqual(expectedValue, complement.Value);
            Assert.AreEqual(expectedPrefix, complement.Prefix);
        }

        [TestMethod()]
        public void GetComplement_NegativeDecimalNumberWithZeroFloatingPart_Pass()
        {
            ComplementConverter conv = new ComplementConverter();
            string input = "-200.0";
            int radix = 10;
            string expectedValue = "800.0";
            string expectedPrefix = "(9)";
            BaseComplement complement = conv.GetComplement(input, radix);
            Assert.AreEqual(expectedValue, complement.Value);
            Assert.AreEqual(expectedPrefix, complement.Prefix);
        }

        [TestMethod()]
        public void GetComplement_PositiveFloatingPointDecimal_Pass()
        {
            ComplementConverter conv = new ComplementConverter();
            string input = "-200.73";
            int radix = 10;
            string expectedValue = "799.27";
            string expectedPrefix = "(9)";
            BaseComplement complement = conv.GetComplement(input, radix);
            Assert.AreEqual(expectedValue, complement.Value);
            Assert.AreEqual(expectedPrefix, complement.Prefix);
        }

        [TestMethod()]
        public void GetDigitComplementTest()
        {

        }

        [TestMethod()]
        public void IncrementNumberTest()
        {

        }

        #region Positive Number Complement

        [TestMethod()]
        public void GetPositiveNumberComplement_PositiveNumberNoFloatingPart_Pass()
        {
            ComplementConverter conv = new ComplementConverter();
            string input = "200";
            int radix = 10;
            string expectedPrefix = "(0)";
            string expectedValue = "200.0";
            BaseComplement complement = conv.GetPositiveNumberComplement(input, radix);
            Assert.AreEqual(expectedValue, complement.Value);
            Assert.AreEqual(expectedPrefix, complement.Prefix);
        }

        [TestMethod()]
        public void GetPositiveNumberComplement_PositiveNumberWithFloatingPart_Pass()
        {
            ComplementConverter conv = new ComplementConverter();
            string input = "200.5";
            int radix = 10;
            string expectedPrefix = "(0)";
            string expectedValue = "200.5";
            BaseComplement complement = conv.GetPositiveNumberComplement(input, radix);
            Assert.AreEqual(expectedValue, complement.Value);
            Assert.AreEqual(expectedPrefix, complement.Prefix);
        }

        [TestMethod()]
        public void GetPositiveNumberComplement_Base64PositiveNumberNoFloatingPart_Pass()
        {
            ComplementConverter conv = new ComplementConverter();
            string input = "20 16";
            int radix = 64;
            string expectedPrefix = "(00)";
            string expectedValue = "20 16.00";
            BaseComplement complement = conv.GetPositiveNumberComplement(input, radix);
            Assert.AreEqual(expectedValue, complement.Value);
            Assert.AreEqual(expectedPrefix, complement.Prefix);
        }

        [TestMethod()]
        public void GetPositiveNumberComplement_Base64PositiveNumberWithFloatingPart_Pass()
        {
            ComplementConverter conv = new ComplementConverter();
            string input = "20 16.25";
            int radix = 64;
            string expectedPrefix = "(00)";
            string expectedValue = "20 16.25";
            BaseComplement complement = conv.GetPositiveNumberComplement(input, radix);
            Assert.AreEqual(expectedValue, complement.Value);
            Assert.AreEqual(expectedPrefix, complement.Prefix);
        }

        #endregion

        #region Input Checks

        [TestMethod()]
        public void IsFloatingPointNumberString_MultipleInputs_Pass()
        {
            ComplementConverter conv = new ComplementConverter();

            string number = "10.24";
            bool expected = true;
            bool actual = conv.IsFloatingPointNumberString(number);
            Assert.AreEqual(expected, actual);

            number = "-10.24";
            expected = true;
            actual = conv.IsFloatingPointNumberString(number);
            Assert.AreEqual(expected, actual);

            number = "10";
            expected = false;
            actual = conv.IsFloatingPointNumberString(number);
            Assert.AreEqual(expected, actual);

            number = "-10";
            expected = false;
            actual = conv.IsFloatingPointNumberString(number);
            Assert.AreEqual(expected, actual);

            number = "AAFFQ asdeq";
            expected = false;
            actual = conv.IsFloatingPointNumberString(number);
            Assert.AreEqual(expected, actual);
        }
    }

    #endregion
}