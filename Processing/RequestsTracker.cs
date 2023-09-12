using Galactic.Logging;
using Galactic.Processing.Models;

namespace Galactic.Processing
{
    public class RequestsTracker
    {
        //
        // SINGLETON
        //
        private static RequestsTracker instance = null;
        private static readonly object SingletonLock = new object();

        public static RequestsTracker Instance
        {
            get
            {
                lock (SingletonLock)
                {
                    if (instance == null)
                    {
                        instance = new RequestsTracker(5);
                    }
                    return instance;
                }
            }
        }

        private RequestsTracker(int cadetLimit)
        {

        }

        // 
        // PROPERTIES
        //

        private static object TrackerLock = new object();

        private int CadetMaxRequests { get; set; }
        private int MaxRequestsTimeSpan { get; set; }

        private Dictionary<string, Queue<DateTime>> requestsTracker = new Dictionary<string, Queue<DateTime>>();

        public RequestsTrackerQueryResponse ClearRequest(string nameId)
        {
            RequestsTrackerQueryResponse response = new RequestsTrackerQueryResponse();
            response.RequestCleared = true;
            response.RequestsRemaining = 3;
            response.FailureMessage = "";

            // Free pass for now

            //lock (TrackerLock)
            //{
            //    if (!requestsTracker.ContainsKey(nameId))
            //    {
            //        requestsTracker.Add(nameId, new Queue<DateTime>());
            //    }

            //    if(requestsTracker[nameId].Count < CadetMaxRequests)
            //    {
            //        InsertRequest(nameId);
            //        response.RequestCleared = true;
            //        response.RequestsRemaining = 1; // TODO: Do this
            //    }
            //    else
            //    {

            //    }

            //}

            return response;
        }

        private void InsertRequest(string nameId)
        {
            requestsTracker[nameId].Enqueue(DateTime.Now);
            
            if(requestsTracker[nameId].Count > CadetMaxRequests)
            {
                requestsTracker[nameId].Dequeue();
            }
        }

    }
}
