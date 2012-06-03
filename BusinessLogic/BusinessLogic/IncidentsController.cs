using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Logic
{
    internal class IncidentsController
    {
        public BoundingBox Box { get; set; }
        public IncidentsController(BoundingBox box)
        {
            Box = box;
        }

        private string _rawData;

        public string RawData
        {
            get
            {
                if (string.IsNullOrEmpty(_rawData))
                {
                    _rawData = Util.GetResponse(Url)
                        .Replace("handleIncidentsResponse(", string.Empty).Replace(");", string.Empty);
                }

                return _rawData;
            }
        }

        public string Url
        {
            get
            {
                //Box.Dump();

                return "http://www.mapquestapi.com/traffic/v1/incidents" +
                       "?key=Fmjtd%7Cluua2d08nq%2Ca5%3Do5-hf25h" +
                       string.Format("&boundingBox={0},{1},{2},{3}",
                                     Box.UpperLeft.Lat,
                                     Box.UpperLeft.Lon,
                                     Box.LowerRight.Lat,
                                     Box.LowerRight.Lon);
            }
        }


        public List<Incident> GetIncidents()
        {
            var json = new JavaScriptSerializer();
            var data = json.Deserialize<dynamic>(RawData);
            var dynIncidents = data["incidents"];
            var incidents = (object[])dynIncidents;

            var incidentsList = new List<Incident>();

            if (incidents.Length > 0)
            {
                foreach (var incidentObject in incidents)
                {
                    var incidentDic = (Dictionary<string, object>)incidentObject;
                    var incident = new Incident();
                    incident.Location = new RoutePoint()
                                            {
                                                Lat = double.Parse(incidentDic["lat"].ToString()),
                                                Lon = double.Parse(incidentDic["lng"].ToString())
                                            };
                    incident.ShortDescription = incidentDic["shortDesc"].ToString();
                    incident.FullDescriptoin = incidentDic["fullDesc"].ToString();
                    incident.LinkId = long.Parse(incidentDic["id"].ToString());
                    incidentsList.Add(incident);
                }
            }

            return incidentsList;
        }
    }
}