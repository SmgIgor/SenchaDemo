using System;
using System.IO;
using System.Net;

namespace Logic
{
    internal static class Util
    {
        public static string GetResponse(string url)
        {
            var request = HttpWebRequest.Create(url);
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            var reader = new StreamReader(responseStream);
            var responseFromServer = reader.ReadToEnd();

            return responseFromServer;
        }

        public static BoundingBox BuildCollisionBox(RoutePoint prev, RoutePoint current)
        {
            return new BoundingBox()
                       {
                           LowerRight = new RoutePoint()
                                            {
                                                Lat = Math.Min(current.Lat, prev.Lat),
                                                Lon = Math.Max(current.Lon, prev.Lon)
                                            },
                           UpperLeft = new RoutePoint()
                                           {
                                               Lat = Math.Max(current.Lat, prev.Lat),
                                               Lon = Math.Min(current.Lon, prev.Lon)
                                           }
                       };
        }
    }
}