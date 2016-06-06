using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class CharHelper_IsNumericTest
    {
        [TestMethod]
        [TestCategory("CharHelper_IsNumeric")]
        public void CharHelper_IsNumeric_Verdadeiro()
        {
            Assert.IsTrue('0'.IsNumeric());
        }

        [TestMethod]
        [TestCategory("CharHelper_IsNumeric")]
        public void CharHelper_IsNumeric_Falso()
        {
            Assert.IsFalse('.'.IsNumeric());
        }
    }
}
