namespace Galactic.Data.Models
{
    public class RawRouteData
    {
        public string Name { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public List<string> NavigationPoints { get; set; } = new List<string>();
        public string Duration { get; set; }
        public List<string> Dangers { get; set; } = new List<string>();
        public string FuelUsage { get; set; }
        public string Description { get; set; }

        public bool IsCaptainOnly
        {
            get { return Duration.Contains("år"); }
        }
    }
}
