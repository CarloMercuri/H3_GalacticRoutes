using Galactic.Data.Models;
using Galactic.Processing.Models;

namespace Galactic.Processing.Interfaces
{
    public interface IPublicRequestsProcessor
    {
        RouteRequestOperation GetRoute(string routeName, string token);
    }
}
