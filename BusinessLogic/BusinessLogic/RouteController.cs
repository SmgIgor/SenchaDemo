using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Logic
{
    public class RouteController
    {
        private string[] _addresses;

        public RouteController(string [] addresses)
        {
            _addresses = addresses;
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
            if (_addresses.Length == 2)
            {
                return GetIncidents(_addresses[0], _addresses[1]);
            }

            var incidents = new List<Incident>();

            for(int i = 0; i < _addresses.Length - 1; i++)
            {
                incidents.AddRange(GetIncidents(_addresses[i], _addresses[i + 1]));
            }

            return incidents;
        }

        private List<Incident> GetIncidents(string fromAddress, string toAddress)
        {
            var route = new Route(fromAddress,
                                  toAddress);

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


        public void SuggestAlternateRoute(long[] linkToAvoid)
        {
            var route = new Route(_addresses[0],                // TODO: Igor, add support for multiple waypoints
                      _addresses[1]);

            var someString = route.GetAlternateRoute(linkToAvoid);
        }
    }

    [TestFixture]
    public class AddressTests
    {
        [Test]
        public void CanGetIncidentsForTwoAddresses()
        {
            var addresses = new []
                                {
                                    "11127 W 108th Overland Park KS 66210",
                                    "103 E 28th St Kansas City MO 64108"
                                };
            var controller = new RouteController(addresses);
            var incidents = controller.GetIncidents();

            Assert.Greater(incidents.Count, 0);
        }

        [Test]
        public void CanGetIncidentsForThreeAddresses()
        {
            var addresses = new[]
                                {
                                    "11127 W 108th Overland Park KS 66210",
                                    "103 E 28th St Kansas City MO 64108",
                                    "1737 McGee Street Kansas City MO 64108"
                                };
            var controller = new RouteController(addresses);
            var incidents = controller.GetIncidents();

            Assert.Greater(incidents.Count, 0);
        }

        [Test]
        public void CanSuggestAlternateRoute()
        {
            var addresses = new[]
                                {
                                    "11127 W 108th Overland Park KS 66210",
                                    "103 E 28th St Kansas City MO 64108",
                                    "1737 McGee Street Kansas City MO 64108"
                                };
            var controller = new RouteController(addresses);
            var incidents = controller.GetIncidents();

            var linkIds = from i in incidents
                          select i.LinkId;

            controller.SuggestAlternateRoute(linkIds.ToArray());
        }
    }
}