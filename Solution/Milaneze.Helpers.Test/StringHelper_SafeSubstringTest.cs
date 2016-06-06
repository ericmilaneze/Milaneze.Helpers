using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class StringHelper_SafeSubstringTest
    {
        [TestMethod]
        [TestCategory("StringHelper_SafeSubstring")]
        public void StringHelper_SafeSubstring_Normal()
        {
            string nome = "0123456789";

            string valorEsperado = "0123";
            string valorAtual = nome.SafeSubstring(0, 4);

            Assert.AreEqual(valorEsperado, valorAtual);
        }

        [TestMethod]
        [TestCategory("StringHelper_SafeSubstring")]
        public void StringHelper_SafeSubstring_StartIndexMenorQue0()
        {
            string nome = "0123456789";

            string valorEsperado = "0123";
            string valorAtual = nome.SafeSubstring(-1, 4);

            Assert.AreEqual(valorEsperado, valorAtual);
        }

        [TestMethod]
        [TestCategory("StringHelper_SafeSubstring")]
        public void StringHelper_SafeSubstring_LengthMenorQue0()
        {
            string nome = "0123456789";

            string valorEsperado = "";
            string valorAtual = nome.SafeSubstring(0, -1);

            Assert.AreEqual(valorEsperado, valorAtual);
        }

        [TestMethod]
        [TestCategory("StringHelper_SafeSubstring")]
        public void StringHelper_SafeSubstring_StartIndexMenorQue0ELengthMenorQue0()
        {
            string nome = "0123456789";

            string valorEsperado = "";
            string valorAtual = nome.SafeSubstring(-1, -1);

            Assert.AreEqual(valorEsperado, valorAtual);
        }

        [TestMethod]
        [TestCategory("StringHelper_SafeSubstring")]
        public void StringHelper_SafeSubstring_StartIndexMaiorQueNumeroDeCaracteres()
        {
            string nome = "0123456789";

            string valorEsperado = "";
            string valorAtual = nome.SafeSubstring(20, 0);

            Assert.AreEqual(valorEsperado, valorAtual);
        }

        [TestMethod]
        [TestCategory("StringHelper_SafeSubstring")]
        public void StringHelper_SafeSubstring_LengthMaiorQueNumeroDeCaracteres()
        {
            string nome = "0123456789";

            string valorEsperado = "0123456789";
            string valorAtual = nome.SafeSubstring(0, 20);

            Assert.AreEqual(valorEsperado, valorAtual);
        }

        [TestMethod]
        [TestCategory("StringHelper_SafeSubstring")]
        public void StringHelper_SafeSubstring_StartIndexMaiorQueNumeroDeCaracteresELengthDiferenteDeZero()
        {
            string nome = "0123456789";

            string valorEsperado = "";
            string valorAtual = nome.SafeSubstring(20, 10);

            Assert.AreEqual(valorEsperado, valorAtual);
        }

        [TestMethod]
        [TestCategory("StringHelper_SafeSubstring")]
        public void StringHelper_SafeSubstring_StartIndexELengthMaiorQueNumeroDeCaracteres()
        {
            string nome = "0123456789";

            string valorEsperado = "";
            string valorAtual = nome.SafeSubstring(20, 20);

            Assert.AreEqual(valorEsperado, valorAtual);
        }

        [TestMethod]
        [TestCategory("StringHelper_SafeSubstring")]
        public void StringHelper_SafeSubstring_StartIndexIgualLength()
        {
            string nome = "0123456789";

            string valorEsperado = "";
            string valorAtual = nome.SafeSubstring(10, 1);

            Assert.AreEqual(valorEsperado, valorAtual);
        }

        [TestMethod]
        [TestCategory("StringHelper_SafeSubstring")]
        public void StringHelper_SafeSubstring_StartIndexMaiorQueZeroELengthMaisStartIndexMaiorQueTamanhoDaString()
        {
            string nome = "0123456789";

            string valorEsperado = "6789";
            string valorAtual = nome.SafeSubstring(6, 20);

            Assert.AreEqual(valorEsperado, valorAtual);
        }

        [TestMethod]
        [TestCategory("StringHelper_SafeSubstring")]
        public void StringHelper_SafeSubstring_SomenteStartLengthIgual0()
        {
            string nome = "0123456789";

            string valorEsperado = nome;
            string valorAtual = nome.SafeSubstring(0);

            Assert.AreEqual(valorEsperado, valorAtual);
        }

        [TestMethod]
        [TestCategory("StringHelper_SafeSubstring")]
        public void StringHelper_SafeSubstring_SomenteStartLengthMaiorQue0()
        {
            string nome = "0123456789";

            string valorEsperado = "456789";
            string valorAtual = nome.SafeSubstring(4);

            Assert.AreEqual(valorEsperado, valorAtual);
        }

        [TestMethod]
        [TestCategory("StringHelper_SafeSubstring")]
        public void StringHelper_SafeSubstring_SomenteStartLengthIgualTamanhoDaString()
        {
            string nome = "0123456789";

            string valorEsperado = "";
            string valorAtual = nome.SafeSubstring(10);

            Assert.AreEqual(valorEsperado, valorAtual);
        }
    }
}