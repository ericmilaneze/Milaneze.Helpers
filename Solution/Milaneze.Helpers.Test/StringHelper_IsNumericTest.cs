using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class StringHelper_IsNumericTest
    {
        [TestMethod]
        [TestCategory("StringHelper_IsNumeric")]
        public void StringHelper_IsNumeric_TextoComNumero()
        {
            string texto = "abc123";

            Assert.IsFalse(texto.IsNumeric());
        }

        [TestMethod]
        [TestCategory("StringHelper_IsNumeric")]
        public void StringHelper_IsNumeric_Texto()
        {
            string texto = "abc";

            Assert.IsFalse(texto.IsNumeric());
        }

        [TestMethod]
        [TestCategory("StringHelper_IsNumeric")]
        public void StringHelper_IsNumeric_NumeroInteiro()
        {
            string texto = "123";

            Assert.IsTrue(texto.IsNumeric());
        }

        [TestMethod]
        [TestCategory("StringHelper_IsNumeric")]
        public void StringHelper_IsNumeric_NumeroQuebradoComPonto()
        {
            string texto = "123.12";

            Assert.IsTrue(texto.IsNumeric());
        }

        [TestMethod]
        [TestCategory("StringHelper_IsNumeric")]
        public void StringHelper_IsNumeric_NumeroQuebradoComVirgula()
        {
            string texto = "123,12";

            Assert.IsTrue(texto.IsNumeric());
        }
    }
}
