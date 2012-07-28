using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeolocateMe.Geocoder
{
    /// <summary>
    /// Service which Parses locations from plain english to geocodable strings
    /// And looks up those geocodable strings using Mapquest geocoder
    /// Returns a list of possible Locations
    /// </summary>
    public class Locationer
    {
        public Result GetForMessage(string message)
        {
            //parse
            var messageParts = Parse(message);
            //geolocate
            return GetLocations(String.Join(",", messageParts));
        }

        /// <summary>
        /// Geolocates the string.
        /// Returns a list of possible locations.
        /// </summary>
        /// <param name="parsedText"></param>
        /// <returns></returns>
        public Result GetLocations(string parsedText)
        {
            //current hacky mock implementation

            //TODO: hook this up to Google Maps or OSM to try and geolocate the string
            return new Result
            {
                Locations = new List<Location>
                {
                    new Location { Description = "Morrissons, Kirkstall, Leeds, LS4", Coordinates = "10,10", Accuracy = Accuracy.Street },
                    new Location { Description = "Morrissons, City Centre, Leeds, LS2", Coordinates = "15,8", Accuracy = Accuracy.Street }
                },
                Message = "Found 2 matches",
                Success = true
            };
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
                new string[] {"near", "in"},
                StringSplitOptions.RemoveEmptyEntries)
                .ToList();
            
            parts.RemoveAt(0);

            return parts;
        }
    }
}
