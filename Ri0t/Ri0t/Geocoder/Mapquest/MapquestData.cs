using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ri0t.Geocoder.Mapquest
{
    internal class MapquestData
    {
        public string Place_ID { get; set; } //"place_id":"4790872",
        public string Licence { get; set; } //"licence":"Data Copyright OpenStreetMap Contributors, Some Rights Reserved. CC-BY-SA 2.0.",
        public string OSM_Type { get; set; } //"osm_type":"node",
        public string OSM_ID { get; set; } //"osm_id":"469789602",
        //public string BoundingBox { get; set; } //"boundingbox":["51.4890005493","51.509004364","-0.140909100175","-0.120909085274"],
        public string Lat { get; set; } //"lat":"51.4990014",
        public string Lon { get; set; } //"lon":"-0.1309091",
        public string Display_Name { get; set; } //"display_name":"Westminster Abbey, Victoria Street, Whitehall, City of Westminster, Greater London, London, England, SW1P 2EF, United Kingdom",
        public string Class { get; set; } //"class":"highway",
        public string Type { get; set; } //"type":"bus_stop",
        public string Icon { get; set; } //"icon":"http://open.mapquestapi.com/nominatim/v1/images/mapicons/transport_bus_stop2.p.20.png"}
    }
}
