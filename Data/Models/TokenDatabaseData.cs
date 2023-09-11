namespace Galactic.Data.Models
{
    public class TokenDatabaseData
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Prefix { get; set; }
        public string TokenHash { get; set; }
        public string ExpirationDate { get; set; }
        public List<string> Permissions { get; set; } = new List<string>();

        public string CreateCsvHeaderLine()
        {
            return "Name,Role,Prefix,TokenHash,ExpirationDate,Permissions";
        }

        public string CreateCsvFileLine()
        {
            string formatted = $"{Name},{Role},{Prefix},{TokenHash},{ExpirationDate}";
            foreach(string s in Permissions)
            {
                formatted += $",{s}";
            }

            return formatted;
        }
    }
}
