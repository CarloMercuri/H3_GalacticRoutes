using Galactic.Data.Models;

namespace Galactic.Data.Interfaces
{
    public interface IRouteFetcher
    {
        List<RawRouteData> GetAllRoutes();
        RawRouteData? GetRoute(string routeName);
    }
}
