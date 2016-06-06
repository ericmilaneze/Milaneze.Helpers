using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class CPFHelper_ExtrairDigitosVerificadoresValidosTest
    {
        [TestMethod]
        [TestCategory("CPFHelper_ExtrairDigitosVerificadoresValidos")]
        [ExpectedException(typeof(ArgumentException))]
        public void CPFHelper_ExtrairDigitosVerificadoresValidos_MenosDeNoveCaracteres()
        {
            string cpf = "10321501";

            string digitosVerificadores = CPFHelper.ExtrairDigitosVerificadoresValidos(cpf);
        }

        [TestMethod]
        [TestCategory("CPFHelper_ExtrairDigitosVerificadoresValidos")]
        [ExpectedException(typeof(ArgumentException))]
        public void CPFHelper_ExtrairDigitosVerificadoresValidos_Nulo()
        {
            string cpf = null;

            string digitosVerificadores = CPFHelper.ExtrairDigitosVerificadoresValidos(cpf);
        }

        [TestMethod]
        [TestCategory("CPFHelper_ExtrairDigitosVerificadoresValidos")]
        public void CPFHelper_ExtrairDigitosVerificadoresValidos_Formatado()
        {
            string cpf = "103.215.011";

            Assert.AreEqual("44", CPFHelper.ExtrairDigitosVerificadoresValidos(cpf));
        }

        [TestMethod]
        [TestCategory("CPFHelper_ExtrairDigitosVerificadoresValidos")]
        public void CPFHelper_ExtrairDigitosVerificadoresValidos_CaracteresEspeciais()
        {
            string cpf = "103*215*011";

            Assert.AreEqual("44", CPFHelper.ExtrairDigitosVerificadoresValidos(cpf));
        }

        [TestMethod]
        [TestCategory("CPFHelper_ExtrairDigitosVerificadoresValidos")]
        public void CPFHelper_ExtrairDigitosVerificadoresValidos_MaisDeNoveCaracteres()
        {
            string cpf = "27978734838";

            Assert.AreEqual("38", CPFHelper.ExtrairDigitosVerificadoresValidos(cpf));
        }

        [TestMethod]
        [TestCategory("CPFHelper_ExtrairDigitosVerificadoresValidos")]
        public void CPFHelper_ExtrairDigitosVerificadoresValidos()
        {
            string cpf1 = "10321501144";
            string cpf2 = "27978734838";

            Assert.AreEqual("44", CPFHelper.ExtrairDigitosVerificadoresValidos(cpf1));
            Assert.AreEqual("38", CPFHelper.ExtrairDigitosVerificadoresValidos(cpf2));
        }
    }
}