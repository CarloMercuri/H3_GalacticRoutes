namespace Galactic.Processing.Models
{
    public class RequestsTrackerQueryResponse
    {
        public bool RequestCleared { get; set; }
        public string FailureMessage { get; set; }
        public int RequestsRemaining { get; set; }
    }
}
