using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class StringHelper_RemoverAcentosTest
    {
        [TestMethod]
        [TestCategory("StringHelper_RemoverAcentos")]
        public void StringHelper_RemoverAcentos_TirarAcentos()
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            string acentosRetirados = comAcentos.RemoverAcentos();

            Assert.AreEqual(semAcentos, acentosRetirados);
        }
    }
}