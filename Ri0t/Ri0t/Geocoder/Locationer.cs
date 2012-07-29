using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ri0t.Interfaces;

namespace Ri0t.Geocoder
{
    /// <summary>
    /// Service which Parses locations from plain english to geocodable strings
    /// And looks up those geocodable strings using Mapquest geocoder
    /// Returns a list of possible Locations
    /// </summary>
    public class Locationer
    {
        private readonly IGeocoder _geocoder;

        public Locationer(IGeocoder geocoder)
        {
            _geocoder = geocoder;
        }

        public Result GetForMessage(string message)
        {
            //parse
            var messageParts = Parse(message);
            //geolocate
            return _geocoder.Geocode(String.Join(",", messageParts));
        }

        public IList<string> Parse(string humanReadableMessage)
        {
            var m = humanReadableMessage.ToLowerInvariant();

            //1. Strip out any "I am" etc
            m = m.Replace("i'm", "");
            m = m.Replace("i am", "");

            //2. Split "near" or "in"
            var parts = new List<string>();
            parts = m.Split(
                new string[] {" near ", " in "},
                StringSplitOptions.RemoveEmptyEntries)
                .ToList();
            
            parts.RemoveAt(0);

            return parts;
        }

    }
}
