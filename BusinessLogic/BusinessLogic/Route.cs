using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Logic
{
    internal class Route
    {
        string FromAddress { get; set; }
        string ToAddress { get; set; }

        private string _rawData;

        public string Url
        {
            get
            {
                return "http://www.mapquestapi.com/directions/v1/route/" +
                       "?key=Fmjtd%7Cluua2d08nq%2Ca5%3Do5-hf25h" +
                       string.Format("&from={0}", HttpUtility.UrlEncode(FromAddress)) +
                       string.Format("&to={0}", HttpUtility.UrlEncode(ToAddress)) +
                       "&generalize=0";
            }
        }

        public string RawData
        {
            get
            {
                if (string.IsNullOrEmpty(_rawData))
                {
                    _rawData = Util.GetResponse(Url);
                    //.Replace("handleIncidentsResponse(", string.Empty).Replace(");", string.Empty);
                }

                return _rawData;
            }
        }


        public Route(string fromAddress, string toAddress)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
        }

        public List<RoutePoint> GetRoutePoints()
        {
            var json = new JavaScriptSerializer();
            var data = json.Deserialize<dynamic>(RawData);
            var dynShapePoints = data["route"]["shape"]["shapePoints"];
            var shapePoints = (object[])dynShapePoints;

            var points = new List<RoutePoint>();

            for (int i = 0; i < shapePoints.Length; i++)
            {
                if (i % 2 == 0)
                {
                    points.Add(new RoutePoint());
                    points.Last().Lat = double.Parse(shapePoints[i].ToString());
                }
                else
                {
                    points.Last().Lon = double.Parse(shapePoints[i].ToString());
                }
            }

            return points;
        }

        public BoundingBox GetBoundingBox()
        {
            var json = new JavaScriptSerializer();
            var data = json.Deserialize<dynamic>(RawData);
            var dynBox = data["route"]["boundingBox"];
            var boxData = (Dictionary<string, object>)dynBox;

            var ul = (Dictionary<string, object>)boxData["ul"];


            var box = new BoundingBox();

            box.UpperLeft = new RoutePoint();
            box.UpperLeft.Lat = double.Parse(((Dictionary<string, object>)boxData["ul"])["lat"].ToString());
            box.UpperLeft.Lon = double.Parse(((Dictionary<string, object>)boxData["ul"])["lng"].ToString());


            box.LowerRight = new RoutePoint();
            box.LowerRight.Lat = double.Parse(((Dictionary<string, object>)boxData["lr"])["lat"].ToString());
            box.LowerRight.Lon = double.Parse(((Dictionary<string, object>)boxData["lr"])["lng"].ToString());

            return box;
        }

        //public Lis

    }
}