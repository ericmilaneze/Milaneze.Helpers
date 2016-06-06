using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class StringHelper_ReplaceFirstTest
    {
        [TestMethod]
        [TestCategory("StringHelper_ReplaceFirst")]
        public void StringHelper_ReplaceFirst_SubstituirSomentePrimeiro()
        {
            string fraseSubstituir = "Meu nome é Eric. Meu nome é Eric";

            string fraseSubstituida = "Abcd nome é Eric. Meu nome é Eric";

            Assert.AreEqual(fraseSubstituida, fraseSubstituir.ReplaceFirst("Meu", "Abcd"));
        }

        [TestMethod]
        [TestCategory("StringHelper_ReplaceFirst")]
        public void StringHelper_ReplaceFirst_StringVazia()
        {
            string fraseSubstituir = "";

            string fraseSubstituida = "";

            Assert.AreEqual(fraseSubstituida, fraseSubstituir.ReplaceFirst("Meu", "Abcd"));
        }
    }
}
