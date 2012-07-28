using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ri0t.Geocoder;

namespace Ri0t
{
    public class Conversation
    {
        public string Identity { get; set; }
        public Location Location { get; set; }
        public string Announcement { get; set; }
        public List<string> Messages { get; set; }
    }
}