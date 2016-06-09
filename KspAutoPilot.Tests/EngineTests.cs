using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSPAutoPilot;
using Rhino.Mocks;
using KRPC.Client.Services.SpaceCenter;

namespace KspAutoPilot.Tests
{
    [TestClass]
    public class EngineTests
    {
        [TestMethod]
        public void IsActiveIsFalse()
        {
            var mock = MockRepository.Mock<IKrpcHandler>();
            IKspEngine eng = new KspEngine(false, mock);
            Assert.IsFalse(eng.IsActive);
        }

        [TestMethod]
        public void Takeoff()
        {
            var mock = MockRepository.Mock<IKrpcHandler>();
            IKspEngine eng = new KspEngine(false, mock);
            Action<string> messageHandler = x => Assert.AreEqual("Take off!", x);
            eng.TakeOff(85, messageHandler);
        }

        [TestMethod]
        public void TakeoffAsync()
        {
            var mock = MockRepository.Mock<IKrpcHandler>();
            IKspEngine eng = new KspEngine(mock);
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
