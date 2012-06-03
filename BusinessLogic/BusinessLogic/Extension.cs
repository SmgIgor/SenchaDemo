using System.Text;

namespace Logic
{
    internal static class Extension
    {
        public static bool FallsWithin(this RoutePoint point, BoundingBox box)
        {
            return (point.Lon <= box.LowerRight.Lon &&
                point.Lon >= box.UpperLeft.Lon &&
                point.Lat >= box.LowerRight.Lat &&
                point.Lat <= box.UpperLeft.Lat);
        }
    }
}
