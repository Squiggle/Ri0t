using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ri0t.Interfaces;
using Ri0t.Geocoder;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Ri0t.Geocoder.Mapquest
{
    public class MapquestGeocoder : IGeocoder
    {

        private const string MapquestUrl = @"http://open.mapquestapi.com/nominatim/v1/search?format=json&q=";

        private IList<Location> GetLocationsForQuery(string searchString)
        {
            var requestUrl = MapquestUrl + System.Web.HttpUtility.UrlEncode(searchString);
            var request = WebRequest.Create(requestUrl);
            var responseStream = request.GetResponse().GetResponseStream();
            var json = new StreamReader(responseStream).ReadToEnd();
            var data = JsonConvert.DeserializeObject<MapquestData[]>(json);

            Console.WriteLine(json);

            return data.Select(p => new Location
            {
                Description = p.Display_Name,
                Coordinates = String.Format("{0},{1}", p.Lat, p.Lon)
            }).ToList();
        }

        #region IGeocoder Members

        public Result Geocode(string query)
        {
            //current hacky mock implementation
            var locations = GetLocationsForQuery(query);

            Console.WriteLine("Looking up location for:\n{0}", query);

            return new Result
            {
                Locations = locations.ToList(),
                Message = String.Format("Found {0} matches", locations.Count),
                Success = locations.Count > 0
            };
        }

        #endregion
    }
}
