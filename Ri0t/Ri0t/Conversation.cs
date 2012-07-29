using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ri0t.Geocoder;
using Ri0t.Interfaces;

namespace Ri0t
{
    public class Conversation
    {
        public string Identity { get; set; }
        public List<Location> Locations { get; set; }
        public string Announcement { get; set; }
        public List<string> Messages { get; set; }

        private IGeocoder _geocoder;

        public Conversation(IGeocoder geocoder)
        {
            _geocoder = geocoder;
        }

        public void Add(string message)
        {
            Messages = Messages == null ? new List<string>() : Messages;
            Messages.Add(message);

            //now geocode something and populate the local properties of this convo
            var locationer = new Locationer(_geocoder);
            var locationResult = locationer.GetForMessage(message);
            locationResult.Locations = locationResult.Locations ?? new List<Location>(); //bleh
            Locations = Locations == null ? new List<Location>() : Locations;

            Locations = locationResult.Locations;
        }

        public string GetNext()
        {
            //if we don't have a location, prompt for one
            if (Locations.Count == 0)
            {
                return "Could you tell me where that's happening?";
            }
            else if (Locations.Count > 1)
            {
                //return a list of choices
                return String.Format(
                    "Did you mean {0} or {1}?",
                    Locations[0].Description.Substring(0, Math.Min(75, Locations[0].Description.Length)),
                    Locations[1].Description.Substring(0, Math.Min(75, Locations[1].Description.Length))
                    );
            }
            else
            {
                return "Send more texts to this number if there's more to say. We'll publish it all!";
            }
        }
    }
}