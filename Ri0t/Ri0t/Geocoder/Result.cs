using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeolocateMe.Geocoder
{
    public class Result
    {
        public IList<Location> Locations { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
