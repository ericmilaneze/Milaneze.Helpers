using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class CPFHelper_ExtrairDigitosVerificadoresTest
    {
        [TestMethod]
        [TestCategory("CPFHelper_ExtrairDigitosVerificadores")]
        public void CPFHelper_ExtrairDigitosVerificadores_MenosDeNoveCaracteres()
        {
            string cpf = "10321501";

            string digitosVerificadores = "";
            
            Assert.AreEqual(digitosVerificadores, CPFHelper.ExtrairDigitosVerificadores(cpf));
        }

        [TestMethod]
        [TestCategory("CPFHelper_ExtrairDigitosVerificadores")]
        public void CPFHelper_ExtrairDigitosVerificadores_Nulo()
        {
            string cpf = null;

            string digitosVerificadores = "";

            Assert.AreEqual(digitosVerificadores, CPFHelper.ExtrairDigitosVerificadores(cpf));
        }

        [TestMethod]
        [TestCategory("CPFHelper_ExtrairDigitosVerificadores")]
        public void CPFHelper_ExtrairDigitosVerificadores_Formatado()
        {
            string cpf = "103.215.011-44";

            Assert.AreEqual("44", CPFHelper.ExtrairDigitosVerificadores(cpf));
        }

        [TestMethod]
        [TestCategory("CPFHelper_ExtrairDigitosVerificadores")]
        public void CPFHelper_ExtrairDigitosVerificadores_CaracteresEspeciais()
        {
            string cpf = "103*215*011-44";

            Assert.AreEqual("44", CPFHelper.ExtrairDigitosVerificadores(cpf));
        }

        [TestMethod]
        [TestCategory("CPFHelper_ExtrairDigitosVerificadores")]
        public void CPFHelper_ExtrairDigitosVerificadores_MaisDeNoveCaracteres()
        {
            string cpf = "279787348389";

            string resultado = CPFHelper.ExtrairDigitosVerificadores(cpf);

            string esperado = "";

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        [TestCategory("CPFHelper_ExtrairDigitosVerificadores")]
        public void CPFHelper_ExtrairDigitosVerificadores_1()
        {
            string cpf1 = "10321501144";

            Assert.AreEqual("44", CPFHelper.ExtrairDigitosVerificadores(cpf1));
        }

        [TestMethod]
        [TestCategory("CPFHelper_ExtrairDigitosVerificadores")]
        public void CPFHelper_ExtrairDigitosVerificadores_2()
        {
            string cpf2 = "27978734838";

            Assert.AreEqual("38", CPFHelper.ExtrairDigitosVerificadores(cpf2));
        }
    }
}