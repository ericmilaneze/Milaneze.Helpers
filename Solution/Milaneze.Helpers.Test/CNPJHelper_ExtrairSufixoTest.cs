using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class CNPJHelper_ExtrairDigitosSufixoTest
    {
        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosSufixo")]
        public void CNPJHelper_ExtrairDigitosSufixo_MenosDeCatorzeCaracteres()
        {
            string cnpj = "0563048400017";

            string sufixo = "0001";
            
            Assert.AreEqual<string>(sufixo, CNPJHelper.ExtrairDigitosSufixo(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosSufixo")]
        public void CNPJHelper_ExtrairDigitosSufixo_Nulo()
        {
            string cnpj = null;

            string sufixo = "";

            Assert.AreEqual(sufixo, CNPJHelper.ExtrairDigitosSufixo(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosSufixo")]
        public void CNPJHelper_ExtrairDigitosSufixo_Formatado()
        {
            string cnpj = "06.990.590/0002-04";

            Assert.AreEqual("0002", CNPJHelper.ExtrairDigitosSufixo(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosSufixo")]
        public void CNPJHelper_ExtrairDigitosSufixo_CaracteresEspeciais()
        {
            string cnpj = "06x990x590|0002*04";

            Assert.AreEqual("0002", CNPJHelper.ExtrairDigitosSufixo(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosSufixo")]
        public void CNPJHelper_ExtrairDigitosSufixo_MaisDeCatorzeCaracteres()
        {
            string cnpj = "06.990.590/0002-045";

            string sufixoCnpj = CNPJHelper.ExtrairDigitosSufixo(cnpj);

            Assert.AreEqual("0002", sufixoCnpj);
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosSufixo")]
        public void CNPJHelper_ExtrairDigitosSufixo_1()
        {
            string cnpj = "05630484000176";

            Assert.AreEqual("0001", CNPJHelper.ExtrairDigitosSufixo(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_ExtrairDigitosSufixo")]
        public void CNPJHelper_ExtrairDigitosSufixo_2()
        {
            string cnpj = "06990590000204";

            Assert.AreEqual("0002", CNPJHelper.ExtrairDigitosSufixo(cnpj));
        }
    }
}