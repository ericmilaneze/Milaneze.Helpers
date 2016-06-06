using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class CPFHelper_ValidarTest
    {
        [TestMethod]
        [TestCategory("CPFHelper_Validar")]
        public void CPFHelper_Validar_Invalido()
        {
            string cpf = "12345678909";

            Assert.IsFalse(CPFHelper.Validar(cpf));
        }

        [TestMethod]
        [TestCategory("CPFHelper_Validar")]
        public void CPFHelper_Validar_MenosDeOnzeCaracteres()
        {
            string cpf = "103215011";

            Assert.IsFalse(CPFHelper.Validar(cpf));
        }

        [TestMethod]
        [TestCategory("CPFHelper_Validar")]
        public void CPFHelper_Validar_MenosDeNoveCaracteres()
        {
            string cpf = "1032150";

            Assert.IsFalse(CPFHelper.Validar(cpf));
        }

        [TestMethod]
        [TestCategory("CPFHelper_Validar")]
        public void CPFHelper_Validar_Nulo()
        {
            string cpf = null;

            Assert.IsFalse(CPFHelper.Validar(cpf));
        }

        [TestMethod]
        [TestCategory("CPFHelper_Validar")]
        public void CPFHelper_Validar_Formatado()
        {
            string cpf = "103.215.011-44";

            Assert.IsTrue(CPFHelper.Validar(cpf));
        }

        [TestMethod]
        [TestCategory("CPFHelper_Validar")]
        public void CPFHelper_Validar_CaracteresEspeciais()
        {
            string cpf = "103*215*011_44";

            Assert.IsTrue(CPFHelper.Validar(cpf));
        }

        [TestMethod]
        [TestCategory("CPFHelper_Validar")]
        public void CPFHelper_Validar()
        {
            string cpf1 = "10321501144";
            string cpf2 = "27978734838";

            Assert.IsTrue(CPFHelper.Validar(cpf1));
            Assert.IsTrue(CPFHelper.Validar(cpf2));
        }

        [TestMethod]
        [TestCategory("CPFHelper_Validar")]
        public void CPFHelper_Validar_TodosIguais()
        {
            string cpf = "11111111111";

            Assert.IsFalse(CPFHelper.Validar(cpf));
        }
    }
}