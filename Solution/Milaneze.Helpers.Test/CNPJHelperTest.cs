using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class CNPJHelperTest
    {
        [TestMethod]
        public void CNPJHelper_Raiz_Ok()
        {
            string cnpj = "06.990.590/0002-04";

            string raizCnpj = "06990590";

            string resultado = CNPJHelper.ExtrairRaiz(cnpj);

            Assert.AreEqual(raizCnpj, resultado);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CNPJHelper_Raiz_MenosCaracteres()
        {
            string cnpj = "06.990.59";

            string raizCnpj = CNPJHelper.ExtrairRaiz(cnpj);
        }

        [TestMethod]
        public void CNPJHelper_TirarFormatacao_Ok()
        {
            string cnpj = "06.990.590/0002-04";

            string cnpjSemFormatacao = "06990590000204";

            Assert.AreEqual(cnpjSemFormatacao, CNPJHelper.TirarFormatacao(cnpj));
        }

        [TestMethod]
        public void CNPJHelper_TirarFormatacao_MenosDe14Caracteres()
        {
            string cpf = "06.990.590/0002-0";

            string cpfSemFormatacao = "0699059000020";

            Assert.AreEqual(cpfSemFormatacao, CNPJHelper.TirarFormatacao(cpf));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CNPJHelper_TirarFormatacao_Nulo()
        {
            string cnpj = null;

            string cnpjSemFormatacao = CNPJHelper.TirarFormatacao(cnpj);
        }

        [TestMethod]
        public void CNPJHelper_Formatar_Ok()
        {
            string cnpj = "06990590000204";

            string cnpjSemFormatacao = "06.990.590/0002-04";

            string cnpjFormatado = CNPJHelper.Formatar(cnpj);

            Assert.AreEqual(cnpjSemFormatacao, cnpjFormatado);
        }

        [TestMethod]
        public void CNPJHelper_Formatar_MenosDe14Caracteres()
        {
            string cnpj = "06.990.590/0002-0";

            string cnpjSemFormatacao = "06.990.590/0002-0";

            string cnpjFormatado = CNPJHelper.Formatar(cnpj);

            Assert.AreEqual(cnpjSemFormatacao, cnpjFormatado);
        }

        [TestMethod]
        public void CNPJHelper_Formatar_Nulo()
        {
            string cnpj = null;

            string cnpjSemFormatacao = CNPJHelper.Formatar(cnpj);

            Assert.IsNull(cnpjSemFormatacao);
        }

        [TestMethod]
        public void CNPJHelper_IsMatriz_Ok()
        {
            string cnpj = "05630484000176";

            bool isCnpjMatriz = CNPJHelper.IsMatriz(cnpj);

            Assert.IsTrue(isCnpjMatriz);
        }

        [TestMethod]
        public void CNPJHelper_IsMatriz_NaoOk()
        {
            string cnpj = "06.990.590/0002-04";

            bool isCnpjMatriz = CNPJHelper.IsMatriz(cnpj);

            Assert.IsFalse(isCnpjMatriz);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CNPJHelper_IsMatriz_Exception()
        {
            string cnpj = null;

            bool isCnpjMatriz = CNPJHelper.IsMatriz(cnpj);
        }

        [TestMethod]
        public void CNPJHelper_AdicionarSufixoMatriz_Ok()
        {
            string cnpj = "05630484";

            string cnpjComMatriz = CNPJHelper.AdicionarSufixoMatriz(cnpj);

            string esperado = "056304840001";

            Assert.AreEqual(esperado, cnpjComMatriz);
        }

        [TestMethod]
        public void CNPJHelper_AdicionarSufixoMatriz_MenosDe8Caracteres()
        {
            string cnpj = "5630484";

            string cnpjComMatriz = CNPJHelper.AdicionarSufixoMatriz(cnpj);

            string esperado = "056304840001";

            Assert.AreEqual(esperado, cnpjComMatriz);
        }

        [TestMethod]
        public void CNPJHelper_AdicionarSufixoMatriz_MaisDe8Caracteres()
        {
            string cnpj = "056304840";

            string cnpjComMatriz = CNPJHelper.AdicionarSufixoMatriz(cnpj);

            string esperado = "056304840001";

            Assert.AreEqual(esperado, cnpjComMatriz);
        }

        [TestMethod]
        public void CNPJHelper_AdicionarSufixoMatriz_Nulo()
        {
            string cnpj = null;

            string cnpjComMatriz = CNPJHelper.AdicionarSufixoMatriz(cnpj);

            Assert.IsNull(cnpjComMatriz);
        }

        [TestMethod]
        public void CNPJHelper_AdicionarSufixoMatrizComDigitosVerificadores_Ok()
        {
            string cnpj = "05630484";

            string cnpjComMatriz = CNPJHelper.AdicionarSufixoMatrizComDigitosVerificadores(cnpj);

            string esperado = "05630484000176";

            Assert.AreEqual(esperado, cnpjComMatriz);
        }

        [TestMethod]
        public void CNPJHelper_AdicionarSufixoMatrizComDigitosVerificadores_MenosDe8Caracteres()
        {
            string cnpj = "5630484";

            string cnpjComMatriz = CNPJHelper.AdicionarSufixoMatrizComDigitosVerificadores(cnpj);

            string esperado = "05630484000176";

            Assert.AreEqual(esperado, cnpjComMatriz);
        }

        [TestMethod]
        public void CNPJHelper_AdicionarSufixoMatrizComDigitosVerificadores_MenosDe8Caracteres2()
        {
            string cnpj = "5579245";

            string cnpjComMatriz = CNPJHelper.AdicionarSufixoMatrizComDigitosVerificadores(cnpj);

            string esperado = "05579245000139";

            Assert.AreEqual(esperado, cnpjComMatriz);
        }

        [TestMethod]
        public void CNPJHelper_AdicionarSufixoMatrizComDigitosVerificadores_MaisDe8Caracteres()
        {
            string cnpj = "056304840";

            string cnpjComMatriz = CNPJHelper.AdicionarSufixoMatrizComDigitosVerificadores(cnpj);

            string esperado = "05630484000176";

            Assert.AreEqual(esperado, cnpjComMatriz);
        }

        [TestMethod]
        public void CNPJHelper_AdicionarSufixoMatrizComDigitosVerificadores_Nulo()
        {
            string cnpj = null;

            string cnpjComMatriz = CNPJHelper.AdicionarSufixoMatrizComDigitosVerificadores(cnpj);

            Assert.IsNull(cnpjComMatriz);
        }
    }
}