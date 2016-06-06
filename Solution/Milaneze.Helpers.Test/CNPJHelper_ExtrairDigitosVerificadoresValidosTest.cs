using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class CNPJHelper_ExtrairDigitosVerificadoresValidosTest
    {
        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosVerificadoresValidos")]
        [ExpectedException(typeof(ArgumentException))]
        public void CNPJHelper_ExtrairDigitosVerificadoresValidos_MenosDeDozeCaracteres()
        {
            string CNPJ = "06.990.590/000";

            string digitosVerificadores = CNPJHelper.ExtrairDigitosVerificadoresValidos(CNPJ);
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosVerificadoresValidos")]
        [ExpectedException(typeof(ArgumentException))]
        public void CNPJHelper_ExtrairDigitosVerificadoresValidos_Nulo()
        {
            string CNPJ = null;

            string digitosVerificadores = CNPJHelper.ExtrairDigitosVerificadoresValidos(CNPJ);
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosVerificadoresValidos")]
        public void CNPJHelper_ExtrairDigitosVerificadoresValidos_Formatado()
        {
            string CNPJ = "06.990.590/0002";

            Assert.AreEqual("04", CNPJHelper.ExtrairDigitosVerificadoresValidos(CNPJ));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosVerificadoresValidos")]
        public void CNPJHelper_ExtrairDigitosVerificadoresValidos_CaracteresEspeciais()
        {
            string CNPJ = "06*990*590|0002";

            Assert.AreEqual("04", CNPJHelper.ExtrairDigitosVerificadoresValidos(CNPJ));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosVerificadoresValidos")]
        public void CNPJHelper_ExtrairDigitosVerificadoresValidos_MaisDeDozeCaracteres()
        {
            string CNPJ = "06.990.590/0002-0";

            Assert.AreEqual("04", CNPJHelper.ExtrairDigitosVerificadoresValidos(CNPJ));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosVerificadoresValidos")]
        public void CNPJHelper_ExtrairDigitosVerificadoresValidos()
        {
            string CNPJ1 = "056304840001";
            string CNPJ2 = "069905900002";

            Assert.AreEqual("76", CNPJHelper.ExtrairDigitosVerificadoresValidos(CNPJ1));
            Assert.AreEqual("04", CNPJHelper.ExtrairDigitosVerificadoresValidos(CNPJ2));
        }
    }
}