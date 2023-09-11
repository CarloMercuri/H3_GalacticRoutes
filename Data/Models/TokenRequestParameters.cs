namespace Galactic.Data.Models
{
    public class TokenRequestParameters
    {
        public bool IsCaptain { get; set; }
        public string Name { get; set; }
        public TimeSpan TokenDuration { get; set; }
        public List<string> TokenPermissions { get; set; }

    }
}
