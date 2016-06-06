using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class CNPJHelper_ValidarTest
    {
        [TestMethod]
        [TestCategory("CNPJHelper_Validar")]
        public void CNPJHelper_Validar_Invalido()
        {
            string cnpj = "06.990.590/0002-00";

            Assert.IsFalse(CNPJHelper.Validar(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_Validar")]
        public void CNPJHelper_Validar_MenosDeCatorzeCaracteres()
        {
            string cnpj = "06.990.590/0002";

            Assert.IsFalse(CNPJHelper.Validar(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_Validar")]
        public void CNPJHelper_Validar_MenosDeOitoCaracteres()
        {
            string cnpj = "3421745";

            Assert.IsFalse(CNPJHelper.Validar(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_Validar")]
        public void CNPJHelper_Validar_Nulo()
        {
            string cnpj = null;

            Assert.IsFalse(CNPJHelper.Validar(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_Validar")]
        public void CNPJHelper_Validar_Formatado()
        {
            string cnpj = "06.990.590/0002-04";

            Assert.IsTrue(CNPJHelper.Validar(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_Validar")]
        public void CNPJHelper_Validar_CaracteresEspeciais()
        {
            string cnpj = "06*990*590|0002_04";

            bool resultado = CNPJHelper.Validar(cnpj);;

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        [TestCategory("CNPJHelper_Validar")]
        public void CNPJHelper_Validar()
        {
            string cnpj = "05630484000176";

            Assert.IsTrue(CNPJHelper.Validar(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_Validar")]
        public void CNPJHelper_Validar_2()
        {
            string cnpj = "06990590000204";

            Assert.IsTrue(CNPJHelper.Validar(cnpj));
        }

        [TestMethod]
        [TestCategory("CNPJHelper_Validar")]
        public void CNPJHelper_Validar_TodosIguais()
        {
            string cnpj = "11111111111111";

            Assert.IsFalse(CNPJHelper.Validar(cnpj));
        }
    }
}