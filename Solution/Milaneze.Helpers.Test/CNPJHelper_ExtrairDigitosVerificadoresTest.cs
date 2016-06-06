using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Milaneze.Helpers;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class CNPJHelper_ExtrairDigitosVerificadoresTest
    {
        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosVerificadores")]
        public void CNPJHelper_ExtrairDigitosVerificadores_MenosDeCatorzeCaracteres()
        {
            string cnpj = "0563048400017";

            string digitosVerificadores = "";
            
            Assert.AreEqual(digitosVerificadores, CNPJHelper.ExtrairDigitosVerificadores(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosVerificadores")]
        public void CNPJHelper_ExtrairDigitosVerificadores_Nulo()
        {
            string cnpj = null;

            string digitosVerificadores = "";

            Assert.AreEqual(digitosVerificadores, CNPJHelper.ExtrairDigitosVerificadores(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosVerificadores")]
        public void CNPJHelper_ExtrairDigitosVerificadores_Formatado()
        {
            string cnpj = "06.990.590/0002-04";

            Assert.AreEqual("04", CNPJHelper.ExtrairDigitosVerificadores(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosVerificadores")]
        public void CNPJHelper_ExtrairDigitosVerificadores_CaracteresEspeciais()
        {
            string cnpj = "06x990x590|0002*04";

            Assert.AreEqual("04", CNPJHelper.ExtrairDigitosVerificadores(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosVerificadores")]
        public void CNPJHelper_ExtrairDigitosVerificadores_MaisDeCatorzeCaracteres()
        {
            string cnpj = "06.990.590/0002-045";

            Assert.AreEqual("", CNPJHelper.ExtrairDigitosVerificadores(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosVerificadores")]
        public void CNPJHelper_ExtrairDigitosVerificadores_1()
        {
            string cnpj = "05630484000176";

            Assert.AreEqual("76", CNPJHelper.ExtrairDigitosVerificadores(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosVerificadores")]
        public void CNPJHelper_ExtrairDigitosVerificadores_2()
        {
            string cnpj = "06990590000204";

            Assert.AreEqual("04", CNPJHelper.ExtrairDigitosVerificadores(cnpj));
        }
    }
}