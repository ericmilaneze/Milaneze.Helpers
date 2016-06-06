using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class DateTimeHelper_PorExtenso
    {
        [TestMethod]
        public void DateTimeHelper_PorExtenso_MesPorExtenso()
        {
            string[] mesesEsperados = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };

            for (int i = 0; i < 12; i++)
            {
                string mesPorExtenso = new DateTime(2014, i + 1, 10).MesPorExtenso();

                if(mesPorExtenso != mesesEsperados[i])
                    Assert.Fail(string.Format("Mês {0} deveria ser {1}, mas teve como resultado {2}.", i + 1, mesesEsperados[i], mesPorExtenso));
            }
        }

        [TestMethod]
        public void DateTimeHelper_PorExtenso_DataPorExtenso()
        {
            string dataPorExtenso = new DateTime(2014, 12, 3).GetDataExtenso();

            string dataPorExtensoEsperada = "Quarta-feira, 3 de dezembro de 2014";

            Assert.AreEqual(dataPorExtensoEsperada, dataPorExtenso);
        }
    }
}