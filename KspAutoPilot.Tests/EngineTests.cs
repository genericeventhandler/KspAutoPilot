using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSPAutoPilot;

namespace KspAutoPilot.Tests
{
    [TestClass]
    public class EngineTests
    {
        [TestMethod]
        public void IsActiveIsFalse()
        {
            IKspEngine eng = new KspEngine(false);
            Assert.IsFalse(eng.IsActive);
        }

        [TestMethod]
        public void Takeoff()
        {
            IKspEngine eng = new KspEngine(false);
            eng.TakeOff(85, x =>
            {
                Assert.AreEqual("Take off!", x);
            });
        }

        [TestMethod]
        public void TakeoffAsync()
        {
            IKspEngine eng = new KspEngine();
            eng.TakeOff(100, x =>
            {
                Assert.AreEqual("Take off!", x);
                Assert.IsTrue(eng.IsActive);
            });

            eng.WaitForTaskToEnd();
            Assert.IsFalse(eng.IsActive);
        }
    }
}
