using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Calc.PositionalSystem.Tests
{
    [TestClass()]
    public class ConversionHelpersTests
    {
        [TestMethod()]
        public void RepresentationStringToListOfStringsTest_Sub36RadixInput_Pass()
        {
            var input = "7543";
            var radix = 8;
            var expectedList = new List<string>( new string[] {"7", "5", "4", "3"});
            var actualList = ConversionHelpers.RepresentationStringToListOfStrings(input, radix);
            var actual = actualList.SequenceEqual(expectedList);
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void RepresentationStringToListOfStringsTest_MoreThan36RadixInput_Pass()
        {
            var input = "12 24 26 76";
            var radix = 64;
            var expectedList = new List<string>(new string[] { "12", "24", "26", "76" });
            var actualList = ConversionHelpers.RepresentationStringToListOfStrings(input, radix);
            var actual = actualList.SequenceEqual(expectedList);
            Assert.AreEqual(true, actual);
        }
    }
}