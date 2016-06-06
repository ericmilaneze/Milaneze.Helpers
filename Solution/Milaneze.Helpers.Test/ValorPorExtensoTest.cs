using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Milaneze.Helpers.ValoresPorExtenso;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class ValorPorExtensoTest
    {
        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Negativo()
        {
            string valorPorExtensoEsperado = "menos mil";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(-1000);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Zero()
        {
            string valorPorExtensoEsperado = "zero";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(0);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_BilhoesComVirgulaSempre()
        {
            string valorPorExtensoEsperado = "nove bilhões, novecentos e noventa e nove milhões, novecentos e noventa e nove mil, novecentos e noventa e nove";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(9999999999);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_MilQuinhentosEOitentaESete()
        {
            string valorPorExtensoEsperado = "mil, quinhentos e oitenta e sete";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(1587);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Mil()
        {
            string valorPorExtensoEsperado = "mil";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(1000);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_MilEUm()
        {
            string valorPorExtensoEsperado = "mil e um";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(1001);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_MilECem()
        {
            string valorPorExtensoEsperado = "mil e cem";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(1100);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_MilCentoEUm()
        {
            string valorPorExtensoEsperado = "mil, cento e um";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(1101);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_UmMilhaoEUm()
        {
            string valorPorExtensoEsperado = "um milhão e um";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(1000001);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_UmMilhaoMilCentoESete()
        {
            string valorPorExtensoEsperado = "um milhão, mil, cento e sete";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(1001107);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_UmMilhaoMilEDez()
        {
            string valorPorExtensoEsperado = "um milhão, mil e dez";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(1001010);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_UmBilhaoEUmMilhao()
        {
            string valorPorExtensoEsperado = "um bilhão e um milhão";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(1001000000);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_UmBilhaoEDoisMilhoes()
        {
            string valorPorExtensoEsperado = "um bilhão e dois milhões";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(1002000000);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_UmBilhaoDoisMilhesCentoECinquentaEQuatro()
        {
            string valorPorExtensoEsperado = "um bilhão, dois milhões, cento e cinquenta e quatro";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(1002000154);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_UmBilhaoEDoisMil()
        {
            string valorPorExtensoEsperado = "um bilhão e dois mil";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(1000002000);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_UmMilhaoEMil()
        {
            string valorPorExtensoEsperado = "um milhão e mil";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(1001000);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Um()
        {
            string valorPorExtensoEsperado = "um";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(1);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_MilCentoEDoze()
        {
            string valorPorExtensoEsperado = "mil, cento e doze";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(1112);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_CentoECinquentaETres()
        {
            string valorPorExtensoEsperado = "cento e cinquenta e três";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(153);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Cinco()
        {
            string valorPorExtensoEsperado = "cinco";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(5);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Sete()
        {
            string valorPorExtensoEsperado = "sete";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(7);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Seis()
        {
            string valorPorExtensoEsperado = "seis";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(6);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Oito()
        {
            string valorPorExtensoEsperado = "oito";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(8);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Onze()
        {
            string valorPorExtensoEsperado = "onze";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(11);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Treze()
        {
            string valorPorExtensoEsperado = "treze";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(13);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Quatorze()
        {
            string valorPorExtensoEsperado = "quatorze";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(14);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Quinze()
        {
            string valorPorExtensoEsperado = "quinze";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(15);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Dezesseis()
        {
            string valorPorExtensoEsperado = "dezesseis";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(16);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Dezessete()
        {
            string valorPorExtensoEsperado = "dezessete";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(17);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Dezoito()
        {
            string valorPorExtensoEsperado = "dezoito";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(18);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Dezenove()
        {
            string valorPorExtensoEsperado = "dezenove";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(19);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Vinte()
        {
            string valorPorExtensoEsperado = "vinte";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(20);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Trinta()
        {
            string valorPorExtensoEsperado = "trinta";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(30);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Quarenta()
        {
            string valorPorExtensoEsperado = "quarenta";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(40);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Sessenta()
        {
            string valorPorExtensoEsperado = "sessenta";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(60);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Setenta()
        {
            string valorPorExtensoEsperado = "setenta";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(70);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Duzentos()
        {
            string valorPorExtensoEsperado = "duzentos";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(200);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Trezentos()
        {
            string valorPorExtensoEsperado = "trezentos";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(300);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Quatrocentos()
        {
            string valorPorExtensoEsperado = "quatrocentos";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(400);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Seiscentos()
        {
            string valorPorExtensoEsperado = "seiscentos";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(600);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Setecentos()
        {
            string valorPorExtensoEsperado = "setecentos";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(700);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtenso_Oitocentos()
        {
            string valorPorExtensoEsperado = "oitocentos";

            string valorPorExtenso = ValorPorExtenso.ParaExtenso(800);

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtensoMoeda_Long()
        {
            string valorPorExtenso = ValorPorExtenso.ParaExtensoMoeda(9999999999);

            string valorPorExtensoEsperado = "nove bilhões, novecentos e noventa e nove milhões, novecentos e noventa e nove mil, novecentos e noventa e nove reais";

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtensoMoeda_Double()
        {
            string valorPorExtenso = ValorPorExtenso.ParaExtensoMoeda(9999999999.99);

            string valorPorExtensoEsperado = "nove bilhões, novecentos e noventa e nove milhões, novecentos e noventa e nove mil, novecentos e noventa e nove reais e noventa e nove centavos";

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtensoMoeda_Centavos()
        {
            string valorPorExtenso = ValorPorExtenso.ParaExtensoMoeda(9999999999.9);

            string valorPorExtensoEsperado = "nove bilhões, novecentos e noventa e nove milhões, novecentos e noventa e nove mil, novecentos e noventa e nove reais e noventa centavos";

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtensoMoeda_Centavos_SomenteNumero()
        {
            string valorPorExtenso = ValorPorExtenso.ParaExtensoMoeda(9999999999.09);

            string valorPorExtensoEsperado = "nove bilhões, novecentos e noventa e nove milhões, novecentos e noventa e nove mil, novecentos e noventa e nove reais e nove centavos";

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtensoMoeda_MuitoDinheiro()
        {
            string valorPorExtenso = ValorPorExtenso.ParaExtensoMoeda(9999999999.01);

            string valorPorExtensoEsperado = "nove bilhões, novecentos e noventa e nove milhões, novecentos e noventa e nove mil, novecentos e noventa e nove reais e um centavo";

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtensoMoeda_ZeroReais()
        {
            string valorPorExtenso = ValorPorExtenso.ParaExtensoMoeda(0.0);

            string valorPorExtensoEsperado = "zero reais";

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtensoMoeda_DezCentavos()
        {
            string valorPorExtenso = ValorPorExtenso.ParaExtensoMoeda(0.1);

            string valorPorExtensoEsperado = "dez centavos";

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtensoMoeda_UmCentavo()
        {
            string valorPorExtenso = ValorPorExtenso.ParaExtensoMoeda(0.01);

            string valorPorExtensoEsperado = "um centavo";

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtensoMoeda_DefinirUnidade()
        {
            ValorPorExtenso.Unidade = new Unidade();

            Assert.AreEqual("real", ValorPorExtenso.Unidade.InteiroSingular);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtensoMoeda_UmRealEUmCentavo()
        {
            string valorPorExtenso = ValorPorExtenso.ParaExtensoMoeda(1.01);

            string valorPorExtensoEsperado = "um real e um centavo";

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtensoMoeda_DoisMilhoesDeReais_Double()
        {
            string valorPorExtenso = ValorPorExtenso.ParaExtensoMoeda(2000000.00);

            string valorPorExtensoEsperado = "dois milhões de reais";

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtensoMoeda_DoisMilhoesDeReais_Decimal()
        {
            string valorPorExtenso = ValorPorExtenso.ParaExtensoMoeda((decimal)2000000);

            string valorPorExtensoEsperado = "dois milhões de reais";

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtensoMoeda_DoisMilhoesDeReais_Float()
        {
            string valorPorExtenso = ValorPorExtenso.ParaExtensoMoeda((float)2000000);

            string valorPorExtensoEsperado = "dois milhões de reais";

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtensoMoeda_DoisMilhoesDeReais_Int()
        {
            string valorPorExtenso = ValorPorExtenso.ParaExtensoMoeda(2000000);

            string valorPorExtensoEsperado = "dois milhões de reais";

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtensoMoeda_MilReais()
        {
            string valorPorExtenso = ValorPorExtenso.ParaExtensoMoeda(1000);

            string valorPorExtensoEsperado = "mil reais";

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }

        [TestMethod]
        public void ValorPorExtenso_ParaExtensoMoeda_MilCentoETrintaReais()
        {
            string valorPorExtenso = ValorPorExtenso.ParaExtensoMoeda(1133);

            string valorPorExtensoEsperado = "mil, cento e trinta e três reais";

            Assert.AreEqual(valorPorExtensoEsperado, valorPorExtenso);
        }
    }
}
