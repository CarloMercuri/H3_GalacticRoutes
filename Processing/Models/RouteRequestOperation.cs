using Galactic.Data.Models;

namespace Galactic.Processing.Models
{
    public class RouteRequestOperation
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public RawRouteData Route { get; set; }
    }
}
