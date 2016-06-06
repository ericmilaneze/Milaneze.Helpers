using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class DateTimeHelperTest
    {
        [TestMethod]
        [TestCategory("DateTimeHelper_CalcularIdade")]
        public void DateTimeHelper_CalcularIdade_SetarDataAtualDefault()
        {
            DateTime dataNascimento = new DateTime(1986, 2, 26);
            Utilidades.SetDataAtualDefault();

            int idadeEsperadaAPartirDe = 28;
            int idade = dataNascimento.CalcularIdade();

            Assert.IsTrue(idade >= idadeEsperadaAPartirDe);
        }

        [TestMethod]
        [TestCategory("DateTimeHelper_CalcularIdade")]
        public void DateTimeHelper_CalcularIdade_LimparDataAtual()
        {
            DateTime dataNascimento = new DateTime(1986, 2, 26);
            Utilidades.ClearDataAtual();

            int idadeEsperadaAPartirDe = 28;
            int idade = dataNascimento.CalcularIdade();

            Assert.IsTrue(idade >= idadeEsperadaAPartirDe);
        }

        [TestMethod]
        [TestCategory("DateTimeHelper_CalcularIdade")]
        public void DateTimeHelper_CalcularIdade_MesAtualMenorMesNascimento()
        {
            DateTime dataNascimento = new DateTime(1986, 2, 26);
            Utilidades.SetDataAtual(new DateTime(2014, 1, 22));

            int idadeEsperada = 27;
            int idade = dataNascimento.CalcularIdade();

            Assert.AreEqual(idadeEsperada, idade);
        }

        [TestMethod]
        [TestCategory("DateTimeHelper_CalcularIdade")]
        public void DateTimeHelper_CalcularIdade_MesAtualIgualMesNascimentoDiaAtual_E_DiaAtualMenorDiaNascimento()
        {
            DateTime dataNascimento = new DateTime(1986, 2, 26);
            Utilidades.SetDataAtual(new DateTime(2014, 2, 22));

            int idadeEsperada = 27;
            int idade = dataNascimento.CalcularIdade();

            Assert.AreEqual(idadeEsperada, idade);
        }

        [TestMethod]
        [TestCategory("DateTimeHelper_CalcularIdade")]
        public void DateTimeHelper_CalcularIdade_DefinirDataAtualPorParametro()
        {
            DateTime dataNascimento = new DateTime(1986, 2, 26);
            DateTime dataAtual = new DateTime(2014, 2, 22);

            int idadeEsperada = 27;
            int idade = dataNascimento.CalcularIdade(dataAtual);

            Assert.AreEqual(idadeEsperada, idade);
        }
    }
}
