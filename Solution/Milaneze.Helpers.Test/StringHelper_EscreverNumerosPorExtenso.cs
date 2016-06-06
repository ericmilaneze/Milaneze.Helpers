using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class StringHelper_EscreverNumerosPorExtenso
    {
        [TestMethod]
        public void StringHelper_EscreverNumerosPorExtenso_UmNumero()
        {
            string frase = "Avenida 23 de Maio";
            string esperado = "Avenida vinte e três de Maio";

            Assert.AreEqual(esperado, frase.EscreverNumerosPorExtenso());
        }

        [TestMethod]
        public void StringHelper_EscreverNumerosPorExtenso_DoisNumeros()
        {
            string frase = "Avenida 24 de Fevereiro Rua 2";
            string esperado = "Avenida vinte e quatro de Fevereiro Rua dois";

            Assert.AreEqual(esperado, frase.EscreverNumerosPorExtenso());
        }

        [TestMethod]
        public void StringHelper_EscreverNumerosPorExtenso_NumeroNoComeco()
        {
            string frase = "24 de Fevereiro";
            string esperado = "vinte e quatro de Fevereiro";

            Assert.AreEqual(esperado, frase.EscreverNumerosPorExtenso());
        }

        [TestMethod]
        public void StringHelper_EscreverNumerosPorExtenso_NumeroNoFim()
        {
            string frase = "Rua 2";
            string esperado = "Rua dois";

            Assert.AreEqual(esperado, frase.EscreverNumerosPorExtenso());
        }

        [TestMethod]
        public void StringHelper_EscreverNumerosPorExtenso_StringVazia()
        {
            string frase = "";
            string esperado = "";

            Assert.AreEqual(esperado, frase.EscreverNumerosPorExtenso());
        }

        [TestMethod]
        public void StringHelper_EscreverNumerosPorExtenso_SemNumeros()
        {
            string frase = "Avenida Brasil";
            string esperado = "Avenida Brasil";

            Assert.AreEqual(esperado, frase.EscreverNumerosPorExtenso());
        }
    }
}
