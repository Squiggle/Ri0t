using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GeolocateMe.Tests
{
    [TestFixture]
    public class Conversations
    {
        private const string MyNumber = "091478496";

        [TearDown]
        public void TearDown()
        {
            GeolocateMe.Message.Reset();
        }

        [Test]
        public void GetAResponse()
        {
            var response = GeolocateMe.Message.Receive("Hello", MyNumber);

            Assert.That(response == "hi", "Expected a new conversation to say 'hi'. Instead recevied '{0}'", response);
        }

        [Test]
        public void CanStartAConversation()
        {
            var response = GeolocateMe.Message.Receive("Hello", MyNumber);
            Assert.That(response == "hi");
            var response2 = GeolocateMe.Message.Receive("What's up?", MyNumber);
            Assert.That(response2 == "Hello again", "Expected a new conversation to say 'Hello again'. Instead recevied '{0}'", response);
        }
    }
}
