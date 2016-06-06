using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class Utilidades_Feriados
    {
        [TestMethod]
        public void Utilidades_Feriados_EncontrarCarnaval_AnoComum()
        {
            var feriados = Utilidades.GetFeriados(2014, true, true, true);
            
            DateTime carnaval = new DateTime(2014, 3, 4);

            Assert.IsTrue(feriados.Contains(carnaval));
        }

        [TestMethod]
        public void Utilidades_Feriados_EncontrarCarnaval_AnoBissexto()
        {
            var feriados = Utilidades.GetFeriados(2012, true, true, true);

            DateTime carnaval = new DateTime(2012, 2, 21);

            Assert.IsTrue(feriados.Contains(carnaval));
        }

        [TestMethod]
        public void Utilidades_Feriados_EncontrarQuartaCinzas_AnoComum()
        {
            var quartaCinzas = Utilidades.GetFeriadosQuartaFeiraDeCinzas(2014);

            DateTime quartaCinzasEsperado = new DateTime(2014, 3, 5);

            Assert.AreEqual(quartaCinzasEsperado, quartaCinzas);
        }

        [TestMethod]
        public void Utilidades_Feriados_EncontrarQuartaCinzas_AnoBissexto()
        {
            var quartaCinzas = Utilidades.GetFeriadosQuartaFeiraDeCinzas(2012);

            DateTime quartaCinzasEsperado = new DateTime(2012, 2, 22);

            Assert.AreEqual(quartaCinzasEsperado, quartaCinzas);
        }

        [TestMethod]
        public void Utilidades_Feriados_EntreDatas()
        {
            var feriados = Utilidades.GetFeriados(new DateTime(2012, 2, 21), new DateTime(2014, 3, 5), true, true, true);

            if (!feriados.Contains(new DateTime(2012, 2, 21)))
                Assert.Fail("Carnaval de 2012 não encontrado.");

            if (!feriados.Contains(new DateTime(2014, 3, 4)))
                Assert.Fail("Carnaval de 2014 não encontrado.");
        }

        [TestMethod]
        public void Utilidades_Feriados_EntreDatas2()
        {
            var feriados = Utilidades.GetFeriados(new DateTime(2012, 2, 21), new DateTime(2014, 3, 3), true, true, true);

            if (!feriados.Contains(new DateTime(2012, 2, 21)))
                Assert.Fail("Carnaval de 2012 não encontrado.");

            if (feriados.Contains(new DateTime(2014, 3, 4)))
                Assert.Fail("Carnaval de 2014 encontrado.");
        }
    }
}
