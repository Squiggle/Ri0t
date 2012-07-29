using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using Ri0t.Geocoder;
using Ri0t.Geocoder.Mapquest;

namespace Ri0t.Tests
{
    [TestFixture]
    public class GeolocationByMessage
    {
        const string Message1 = "I'm near Millennium Square in Leeds";

        [Test]
        public void ParseMessageForLocation()
        {
            //ASSIGN
            var geoservice = new Locationer(null);

            //ACT
            var geoparts = geoservice.Parse(Message1);

            //ASSERT
            Assert.AreEqual(2, geoparts.Count);
            Assert.That(geoparts[0].Contains("morrissons"), "Expected to find Morrisons in part 1, instead was '{0}'", geoparts[0]);
            Assert.That(geoparts[1].Contains("leeds"), "Expected to find Leeds in part 2, instead was '{0}'", geoparts[1]);
        }

        //[Test]
        [Ignore]
        public void GeolocateMessageViaMapquest()
        {
            var geoservice = new Locationer(null);

            //1. Parse message
            var parts = geoservice.Parse(Message1);
            
            //2. Concat
            var query = string.Join(",", parts);

            //3. Geocode query
            var result = new MapquestGeocoder().Geocode(query);

            Assert.NotNull(result);
            Assert.GreaterOrEqual(result.Locations.Count, 1);

            foreach (var location in result.Locations)
            {
                Console.WriteLine(location.Description);
            }

        }

        //[TestCase("It's all kicking off in Hyde Park!", true)]
        //[TestCase("Holy crap! There's been a huge accident on the A65 in Horsforth", true)]
        //[TestCase("Woah. I just got mugged by a clown.", false)]
        [Ignore]
        public void AnyHitsFor(string message, bool shouldFindSomething)
        {
            var service = new Locationer(null);
            var parsedText = service.Parse(message);
            var query = string.Join(",", parsedText);

            var hits = service.GetForMessage(query);

            if (shouldFindSomething) {
                Assert.That(hits.Locations.Count > 0, "Expected some locations to be returned. Instead there was none.");
            }
            else {
                Assert.That(hits.Locations.Count == 0, "Expected no results. Instead found {0} locations for the phrase '{1}'", hits.Locations.Count, message);
            }
        }
    }
}
