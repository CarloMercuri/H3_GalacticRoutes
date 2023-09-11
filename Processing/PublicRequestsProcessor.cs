using Galactic.Data.Interfaces;
using Galactic.Data.Models;
using Galactic.Processing.Interfaces;
using Galactic.Processing.Models;

namespace Galactic.Processing
{
    public class PublicRequestsProcessor : IPublicRequestsProcessor
    {
        IRouteFetcher _routesFetcher;
        ITokenProcessor _tokenProcessor;

        public PublicRequestsProcessor(IRouteFetcher routesFetcher, ITokenProcessor tokenProcessor)
        {
            _routesFetcher = routesFetcher;
            _tokenProcessor = tokenProcessor;   
        }
        public RouteRequestOperation GetRoute(string routeName, string token)
        {
            RawRouteData route = _routesFetcher.GetRoute(routeName);
            RouteRequestOperation result = new RouteRequestOperation();


            if(route is null)
            {
                result.Success = false;
                result.Message = "Route with specified name not found";
                result.Route = null;

                return result;
            }

            TokenDatabaseData tokenData = _tokenProcessor.GetSingleTokenData(token);

            if(tokenData is null)
            {
                result.Success = false;
                result.Message = "Invalid Token. Access Denied.";
                result.Route = null;

                return result;
            }

            if (route.IsCaptainOnly)
            {
                if(tokenData.Role != "Captain")
                {
                    result.Success = false;
                    result.Message = "Unauthorized. This route is only accessible to Captains.";
                    result.Route = null;

                    return result;
                }
            }

            // Otherwise, all good
            result.Success = true;
            result.Message = "";
            result.Route = route;

            return result;

        }
    }
}
