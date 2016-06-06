using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class StringHelper_RemoverCaracteresNaoNumericosTest
    {
        [TestMethod]
        public void StringHelper_RemoverCaracteresNaoNumericos()
        {
            string caracteresComNaoNumericos = "A12.+45-X,\n";

            string caracteresNumericos = "1245";

            string resultado = caracteresComNaoNumericos.RemoverCaracteresNaoNumericos();

            Assert.AreEqual(caracteresNumericos, resultado);
        }
    }
}
