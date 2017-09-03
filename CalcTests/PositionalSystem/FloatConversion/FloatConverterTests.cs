using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calc.PositionalSystem.Tests
{
    [TestClass()]
    public class FloatConverterTests
    {
        // Test Cases for Float Converter
        // 1. Positive Number
        // 2. Negative Number
        // 3. Positive Zero   
        // 4. Negative Zero
        // 5. Very Small Positive Number
        // 6. Very Small Negative Number
        // 7. Very Large Positive Number
        // 8. Very Large Negative Number
        // 9. Minimum Float Value
        // 10. Maximum Float Value

        // Same for strings and double precision numbers

        // Binary values taken from :
        // https://www.h-schmidt.net/FloatConverter/IEEE754.html
        // http://www.binaryconvert.com/result_double.html?decimal=051046049052049053057050
        // https://en.wikipedia.org/wiki/Single-precision_floating-point_format

        public FloatConverter fc = new FloatConverter();

        #region Single To Binary String

        [TestMethod()]
        public void SingleToBinaryString_Positive_Pass()
        {
            float input = 3.141592f;
            string expected = "01000000010010010000111111011000";
            string actual = fc.SingleToBinaryString(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SingleToBinaryString_Negative_Pass()
        {
            float input = -3.141592f;
            string expected = "11000000010010010000111111011000";
            string actual = fc.SingleToBinaryString(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SingleToBinaryString_PositiveZero_Pass()
        {
            float input = 0f;
            string expected = "00000000000000000000000000000000";
            string actual = fc.SingleToBinaryString(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SingleToBinaryString_NegativeZero_Pass()
        {
            float input = -0f;
            string expected = "10000000000000000000000000000000";
            string actual = fc.SingleToBinaryString(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SingleToBinaryString_VerySmallPositive_Pass()
        {
            float input = 8.552926E-36f;
            string expected = "00000101001101011110011010001111";
            string actual = fc.SingleToBinaryString(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SingleToBinaryString_VerySmallNegative_Pass()
        {
            float input = -8.552926E-36f;
            string expected = "10000101001101011110011010001111";
            string actual = fc.SingleToBinaryString(input);
            Assert.AreEqual(expected, actual);
        }       

        [TestMethod()]
        public void SingleToBinaryString_VeryLargePositive_Pass()
        {
            float input = 2.657424E36f;
            string expected = "01111011111111111110011010001111";
            string actual = fc.SingleToBinaryString(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SingleToBinaryString_VeryLargeNegative_Pass()
        {
            float input = -2.657424E36f;
            string expected = "11111011111111111110011010001111";
            string actual = fc.SingleToBinaryString(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SingleToBinaryString_Minimum_Pass()
        {
            float input = float.MinValue;
            string expected = "11111111011111111111111111111111";
            string actual = fc.SingleToBinaryString(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SingleToBinaryString_Maximum_Pass()
        {
            float input = float.MaxValue;
            string expected = "01111111011111111111111111111111";
            string actual = fc.SingleToBinaryString(input);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Binary String to Single

        [TestMethod()]
        public void BinaryStringToSingle_PositiveBinaryString_Pass()
        {
            string input = "01000000010010010000111111011000";
            float expected = 3.141592f;
            float actual = fc.BinaryStringToSingle(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BinaryStringToSingle_NegativeBinaryString_Pass()
        {
            string input = "11000000010010010000111111011000";
            float expected = -3.141592f;
            float actual = fc.BinaryStringToSingle(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BinaryStringToSingle_PositiveZeroString_Pass()
        {
            string input = "00000000000000000000000000000000";
            float expected = 0f;
            float actual = fc.BinaryStringToSingle(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BinaryStringToSingle_NegativeZeroString_Pass()
        {
            string input = "10000000000000000000000000000000";
            float expected = -0f;
            float actual = fc.BinaryStringToSingle(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BinaryStringToSingle_VerySmallPositiveBinaryString_Pass()
        {
            string input = "00000101001101011110011010001111";
            float expected = 8.552926E-36f;
            float actual = fc.BinaryStringToSingle(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BinaryStringToSingle_VerySmallNegativeBinaryString_Pass()
        {
            string input = "10000101001101011110011010001111";
            float expected = -8.552926E-36f;
            float actual = fc.BinaryStringToSingle(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BinaryStringToSingle_VeryLargePositveBinaryString_Pass()
        {
            string input = "01111011111111111110011010001111";
            float expected = 2.657424E36f;
            float actual = fc.BinaryStringToSingle(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BinaryStringToSingle_VeryLargeNegativeinaryString_Pass()
        {
            string input = "11111011111111111110011010001111";
            float expected = -2.657424E36f;
            float actual = fc.BinaryStringToSingle(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BinaryStringToSingle_MinimumBinaryString_Pass()
        {
            string input = "11111111011111111111111111111111";
            float expected = float.MinValue;
            float actual = fc.BinaryStringToSingle(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BinaryStringToSingle_MaximumSmallBinaryString_Pass()
        {
            string input = "01111111011111111111111111111111";
            float expected = float.MaxValue;
            float actual = fc.BinaryStringToSingle(input);
            Assert.AreEqual(expected, actual);
        }



        #endregion

        #region Double To Binary String
        #endregion

        #region Binary String to Double

        #endregion
    }
}