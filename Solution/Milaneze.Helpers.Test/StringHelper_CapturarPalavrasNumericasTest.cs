using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class StringHelper_CapturarPalavrasNumericasTest
    {
        [TestMethod]
        public void StringHelper_CapturarPalavrasNumericas_UmNumero()
        {
            string frase = "Avenida 23 de Maio";
            string numeroEsperado = "23";

            Assert.AreEqual(numeroEsperado, frase.CapturarPalavrasNumericas()[0]);
        }

        [TestMethod]
        public void StringHelper_CapturarPalavrasNumericas_DoisNumeros()
        {
            string frase = "Avenida 24 de Fevereiro Rua 2";
            string numeroEsperado1 = "24";
            string numeroEsperado2 = "2";

            Assert.IsTrue(numeroEsperado1 == frase.CapturarPalavrasNumericas()[0] && numeroEsperado2 == frase.CapturarPalavrasNumericas()[1]);
        }

        [TestMethod]
        public void StringHelper_CapturarPalavrasNumericas_StringVazia()
        {
            string frase = "";

            Assert.IsFalse(frase.CapturarPalavrasNumericas().Any());
        }

        [TestMethod]
        public void StringHelper_CapturarPalavrasNumericas_SemNumeros()
        {
            string frase = "Avenida Brasil";

            Assert.IsFalse(frase.CapturarPalavrasNumericas().Any());
        }
    }
}
