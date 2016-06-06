using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class CPFHelperTest
    {
        [TestMethod]
        public void CPFHelper_Raiz_Ok()
        {
            string cpf = "10321501144";

            string raizCpf = "103215011";

            Assert.AreEqual(raizCpf, CPFHelper.ExtrairRaiz(cpf));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CPFHelper_Raiz_MenosCaracteres()
        {
            string cpf = "10321501";

            string raizCpf = CPFHelper.ExtrairRaiz(cpf);
        }

        [TestMethod]
        public void CPFHelper_TirarFormatacao_Ok()
        {
            string cpf = "103.215.011-44";

            string cpfSemFormatacao = "10321501144";

            Assert.AreEqual(cpfSemFormatacao, CPFHelper.TirarFormatacao(cpf));
        }

        [TestMethod]
        public void CPFHelper_TirarFormatacao_MenosDe11Caracteres()
        {
            string cpf = "103.215.011";

            string cpfSemFormatacao = "103215011";

            Assert.AreEqual(cpfSemFormatacao, CPFHelper.TirarFormatacao(cpf));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CPFHelper_TirarFormatacao_Nulo()
        {
            string cpf = null;

            string cpfSemFormatacao = CPFHelper.TirarFormatacao(cpf);
        }

        [TestMethod]
        public void CPFHelper_Formatar_Ok()
        {
            string cpf = "10321501144";

            string cpfSemFormatacao = "103.215.011-44";

            Assert.AreEqual(cpfSemFormatacao, CPFHelper.Formatar(cpf));
        }

        [TestMethod]
        public void CPFHelper_Formatar_MenosDe11Caracteres()
        {
            string cpf = "103.215.011";

            string cpfSemFormatacao = "103.215.011";

            Assert.AreEqual(cpfSemFormatacao, CPFHelper.Formatar(cpf));
        }

        [TestMethod]
        public void CPFHelper_Formatar_Nulo()
        {
            string cpf = null;

            string cpfSemFormatacao = CPFHelper.Formatar(cpf);

            Assert.IsNull(cpfSemFormatacao);
        }
    }
}