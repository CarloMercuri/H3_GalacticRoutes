using Galactic.Data.Interfaces;
using Galactic.Data.Models;
using Galactic.Logging;
using Galactic.Processing.Interfaces;
using Galactic.Processing.Models;
using Microsoft.Extensions.Primitives;

namespace Galactic.Processing
{
    public class PublicRequestsProcessor : IPublicRequestsProcessor
    {
        IRouteFetcher _routesFetcher;
        ITokenProcessor _tokenProcessor;
        LogSession _logSession = new LogSession();
        RequestsTracker _requestsTracker = RequestsTracker.Instance;

        public PublicRequestsProcessor(IRouteFetcher routesFetcher, ITokenProcessor tokenProcessor)
        {
            _routesFetcher = routesFetcher;
            _tokenProcessor = tokenProcessor;   
        }
        public RouteRequestOperation GetRoute(string routeName, string token)
        {
            TokenDatabaseData tokenData = _tokenProcessor.GetSingleTokenData(token);
            RouteRequestOperation result = new RouteRequestOperation();

            if (tokenData is null)
            {
                result.Success = false;
                result.Message = "Invalid Token. Access Denied.";
                result.Route = null;

                return result;
            }

            //
            // Token expiration
            //

            if(DateTime.TryParse(tokenData.ExpirationDate, out DateTime tokenExpiryDate))
            {
                if(tokenExpiryDate > DateTime.Now)
                {
                    result.Success = false;
                    result.Message = "Token expired. Must request a new one.";
                    result.Route = null;

                    return result;
                }
            }
            else
            {
                result.Success = false;
                result.Message = "Corrupted token. Must request a new one.";
                result.Route = null;

                _logSession.LogError($"Corrupted DateTime value for token with person name: {tokenData.Name}. Failed to parse", GalacticLogLevel.Always);

                return result;
            }

            //
            // Token max requests
            //

            if(tokenData.Role != "Captain") // captain has unlimited requests
            {
                RequestsTrackerQueryResponse resp = _requestsTracker.ClearRequest(tokenData.Name);

                if (!resp.RequestCleared)
                {
                    result.Success = false;
                    result.Message = resp.FailureMessage;
                    result.Route = null;
                    return result;
                }
            }

            //
            // Route
            //

            RawRouteData route = _routesFetcher.GetRoute(routeName);
           
            if (route is null)
            {
                result.Success = false;
                result.Message = "Route with specified name not found";
                result.Route = null;

                return result;
            }

            // 
            // Route Permissions
            //
            

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
