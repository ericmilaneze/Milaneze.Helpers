using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Milaneze.Helpers;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class ArrayHelper_IsIndexOkTest
    {
        [TestMethod]
        public void ArrayHelper_IsIndexOk_Fora_Igual()
        {
            string[] strArray = new string[3];

            Assert.IsFalse(strArray.IsIndexOk(3));
        }

        [TestMethod]
        public void ArrayHelper_IsIndexOk_Fora_Maior()
        {
            string[] strArray = new string[3];

            Assert.IsFalse(strArray.IsIndexOk(4));
        }

        [TestMethod]
        public void ArrayHelper_IsIndexOk_Fora_MenosQueZero()
        {
            string[] strArray = new string[3];

            Assert.IsFalse(strArray.IsIndexOk(-1));
        }

        [TestMethod]
        public void ArrayHelper_IsIndexOk_Dentro_IgualZero()
        {
            string[] strArray = new string[3];

            Assert.IsTrue(strArray.IsIndexOk(0));
        }

        [TestMethod]
        public void ArrayHelper_IsIndexOk_Dentro()
        {
            string[] strArray = new string[3];

            Assert.IsTrue(strArray.IsIndexOk(2));
        }
    }
}
