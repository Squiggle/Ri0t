using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ri0t.Geocoder;

namespace Ri0t.Interfaces
{
    public interface IGeocoder
    {
        Result Geocode(string query);
    }
}
