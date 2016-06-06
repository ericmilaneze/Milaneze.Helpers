using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class StringHelper_RemoverEspacosDuplicadosTest
    {
        [TestMethod]
        [TestCategory("StringHelper_RemoverEspacosDuplicados")]
        public void StringHelper_RemoverEspacosDuplicados()
        {
            string fraseComEspacosDuplicados = "Meu  nome é     Eric Milaneze.";
            string fraseArrumada = "Meu nome é Eric Milaneze.";

            Assert.AreEqual(fraseArrumada, fraseComEspacosDuplicados.RemoverEspacosDuplicados());
        }

        [TestMethod]
        [TestCategory("StringHelper_RemoverEspacosDuplicados")]
        public void StringHelper_RemoverEspacosDuplicados_NenhumDuplicado()
        {
            string fraseComEspacosDuplicados = "Meu  nome é Eric Milaneze.";
            string fraseArrumada = "Meu nome é Eric Milaneze.";

            Assert.AreEqual(fraseArrumada, fraseComEspacosDuplicados.RemoverEspacosDuplicados());
        }
    }
}
