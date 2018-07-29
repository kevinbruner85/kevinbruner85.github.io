using System;
using Capstone.Vending_Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
    [TestClass]
    public class UnitTest1
    {
        VendingMachine _vm = new VendingMachine();

        [TestMethod]
        public void WriteContentsTest()
        {
            int result = _vm.InputMoney(0);
            Assert.AreEqual(0, result);

            result = _vm.InputMoney(-1);
            Assert.AreEqual(0, result);

            result = _vm.InputMoney(1);
            Assert.AreEqual(0, result);

            result = _vm.InputMoney(150);
            Assert.AreEqual(0, result);

            result = _vm.InputMoney(-150);
            Assert.AreEqual(0, result);

            result = _vm.InputMoney((int)1.5);
            Assert.AreEqual(0, result);
        }
    }
}
