using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using Ri0t.Interfaces;
using Ri0t.Geocoder;

namespace Ri0t.Tests
{
    [TestFixture]
    public class Conversations
    {
        private const string MyNumber = "091478496";

        private Mock<IGeocoder> NoResultsGeomock()
        {
            var geomock = new Mock<IGeocoder>();
            geomock.Setup(o => o.Geocode(It.IsAny<string>())).Returns(
                new Geocoder.Result
                {
                    Locations = new List<Ri0t.Geocoder.Location>(),
                    Message = "No results",
                    Success = false
                });
            return geomock;
        }
        private Mock<IGeocoder> ManyResultsGeomock()
        {
            var geomock = new Mock<IGeocoder>();
            geomock.Setup(o => o.Geocode(It.IsAny<string>())).Returns(
                new Geocoder.Result
                {
                    Locations = new List<Ri0t.Geocoder.Location> {
                        new Location { Description = "Hyde Park, London" },
                        new Location { Description = "Hyde Park, Leeds" },
                        new Location { Description = "Hide Park, Bristol" },
                    },
                    Message = "Three results",
                    Success = true
                });
            return geomock;
        }

        [TearDown]
        public void TearDown()
        {
            Ri0t.MessageManager.Reset();
        }

        [Test]
        public void CanStartAConversation()
        {
            var response = new MessageManager(NoResultsGeomock().Object).GetConversation(MyNumber);
            Assert.NotNull(response);
        }

        [Test]
        public void NoGeodataInFirstMessagePromptsForMoreInfo()
        {
            var convo = new MessageManager(NoResultsGeomock().Object).GetConversation(MyNumber);
            convo.Add("Hey there");

            var nextMessage = convo.GetNext();
            Assert.AreEqual("Could you tell me where that's happening?", nextMessage);
        }

        [Test]
        public void TooManyPossibleLocationsPromptsForConfirmation()
        {
            var convo = new MessageManager(ManyResultsGeomock().Object).GetConversation(MyNumber);
            convo.Add("It's all kicking off in Hyde Park!");

            var nextMessage = convo.GetNext();
            Assert.AreEqual("Did you mean Hyde Park, London or Hyde Park, Leeds?", nextMessage);
        }

    }
}
