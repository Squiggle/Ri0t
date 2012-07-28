using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using Ri0t.Geocoder;

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
            var geoservice = new Locationer();

            //ACT
            var geoparts = geoservice.Parse(Message1);

            //ASSERT
            Assert.AreEqual(2, geoparts.Count);
            Assert.That(geoparts[0].Contains("morrissons"), "Expected to find Morrisons in part 1, instead was '{0}'", geoparts[0]);
            Assert.That(geoparts[1].Contains("leeds"), "Expected to find Leeds in part 2, instead was '{0}'", geoparts[1]);
        }

        [Test]
        public void GeolocateMessageViaMapquest()
        {
            var geoservice = new Locationer();

            //1. Parse message
            var parts = geoservice.Parse(Message1);
            
            //2. Concat
            var query = string.Join(",", parts);

            //3. Geocode query
            var result = new MapQuestGeocoder().Geocode(query);

            Assert.NotNull(result);
            Assert.GreaterOrEqual(result.Count, 1);
            foreach (var location in result)
            {
                Console.WriteLine(location.Description);
            }

        }


    }
}
