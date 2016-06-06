using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class DateTimeHelper_CalcularIdade_NaoSetarDataAtualTest
    {
        [TestInitialize]
        public void Inicializar()
        {
            Utilidades.ClearDataAtual();
        }

        [TestMethod]
        public void DateTimeHelper_CalcularIdade_NaoSetarDataAtual()
        {
            DateTime dataNascimento = new DateTime(1986, 2, 26);

            int idadeEsperadaAPartirDe = 28;
            int idade = dataNascimento.CalcularIdade();

            Assert.IsTrue(idade >= idadeEsperadaAPartirDe);
        }
    }
}
