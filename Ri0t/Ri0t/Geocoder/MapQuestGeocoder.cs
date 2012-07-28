using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Web;
using System.IO;
using GeolocateMe.Geocoder.Mapquest;

namespace GeolocateMe.Geocoder
{
    public class MapQuestGeocoder
    {
        private const string MapquestUrl = @"http://open.mapquestapi.com/nominatim/v1/search?format=json&q=";
        
        public IList<Location> Geocode(string searchString)
        {
            var requestUrl = MapquestUrl + System.Web.HttpUtility.UrlEncode(searchString);
            var request = WebRequest.Create(requestUrl);
            var responseStream = request.GetResponse().GetResponseStream();
            var json = new StreamReader(responseStream).ReadToEnd();
            var data = JsonConvert.DeserializeObject<MapquestData[]>(json);

            Console.WriteLine(json);

            return data.Select(p => new Location {
                Description = p.Display_Name,
                Coordinates = String.Format("{0},{1}", p.Lat, p.Lon)
            }).ToList();
        }

    }
}

