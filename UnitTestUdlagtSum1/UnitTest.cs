using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using FællesSpisning.Model;

namespace UnitTestUdlagtSum
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestUdlagtSumPositiv()
        {
            Udlæg pris1 = new Udlæg();
            pris1.UdlagtSum = 50;
            Assert.AreEqual(50,pris1.UdlagtSum);
        }

        [TestMethod]
        public void TestUdlagtSumNegativ()
        {
            Udlæg pris2 = new Udlæg();
            try
            {
                pris2.UdlagtSum = -50;
                Assert.Fail();
            }
            catch (ArgumentException ae)
            {
                //ok
            }
           
        }
    }
}
