using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using FællesSpisning.Model;

namespace UnitTestUdlagtSum1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestUdlagtSum1Positiv()
        {
            BeregnPris pris1 = new BeregnPris();
            pris1.UdlagtSum1 = 50;
            Assert.AreEqual(50,pris1.UdlagtSum1);
        }

        [TestMethod]
        public void TestUdlagtSum1Negativ()
        {
            BeregnPris pris2 = new BeregnPris();
            pris2.UdlagtSum1 = -50;
            Assert.AreEqual(-50,pris2.UdlagtSum1);
        }
    }
}
