using System.Collections.Generic;

namespace Logic
{
    public class RouteController
    {
        string _fromAddress, _toAddress;

        public RouteController(string fromAddress, string toAddress)
        {
            _fromAddress = fromAddress;
            _toAddress = toAddress;
        }

        //	public bool HasIncidents
        //	{
        //		get 
        //		{
        //			return false;
        //		}
        //	}

        public List<Incident> GetIncidents()
        {
            var route = new Route(_fromAddress,
                                  _toAddress);

            var points = route.GetRoutePoints();

            var boundingBox = route.GetBoundingBox();

            var incidentsController = new IncidentsController(boundingBox);

            var incidents = incidentsController.GetIncidents();

            var incidentsOnTheRoute = new List<Incident>();

            foreach (var incident in incidents)
            {
                RoutePoint prevPoint = null;
                foreach (var point in points)
                {
                    if (prevPoint != null)
                    {

                        var box = Util.BuildCollisionBox(prevPoint, point);

                        if (incident.Location.FallsWithin(box))
                        {
                            incidentsOnTheRoute.Add(incident);
                            break;
                        }
                    }
                    prevPoint = point;
                }
            }

            return incidentsOnTheRoute;
        }
    }
}