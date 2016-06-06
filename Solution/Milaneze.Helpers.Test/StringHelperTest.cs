using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class StringHelperTest
    {
        [TestMethod]
        public void StringHelper_IsCaracteresDiferentes_True()
        {
            string caracteres = "123456";

            Assert.IsTrue(caracteres.IsCaracteresDiferentes());
        }

        [TestMethod]
        public void StringHelper_IsCaracteresDiferentes_False()
        {
            string caracteres = "111111";

            Assert.IsFalse(caracteres.IsCaracteresDiferentes());
        }

        [TestMethod]
        public void StringHelper_IsCaracteresDiferentes_UmCaractere()
        {
            string caracteres = "0";

            Assert.IsFalse(caracteres.IsCaracteresDiferentes());
        }

        [TestMethod]
        public void StringHelper_IsCaracteresDiferentes_NenhumCaractere()
        {
            string caracteres = "";

            Assert.IsFalse(caracteres.IsCaracteresDiferentes());
        }
    }
}
