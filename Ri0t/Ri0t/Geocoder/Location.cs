using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeolocateMe.Geocoder
{
    public class Location
    {
        public string Description { get; set; }
        public string Coordinates { get; set; }
        public Accuracy Accuracy { get; set; }
    }

    public enum Accuracy
    {
        Street = 1,
        Postcode = 2,
        Town = 3
    }
}
