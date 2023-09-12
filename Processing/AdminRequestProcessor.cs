using CsvHelper.Configuration.Attributes;
using Galactic.Data.Interfaces;
using Galactic.Data.Models;
using Galactic.Processing.Interfaces;

namespace Galactic.Processing
{
    public class AdminRequestProcessor : ISecureRequestsProcessor
    {
          ITokenProcessor _tokenProcessor;

        public AdminRequestProcessor(ITokenDataFetching fetcher, ITokenProcessor tokenProcessor)
        {
            _tokenProcessor = tokenProcessor;   
        }

        public string CreateToken(string personName, bool isCaptain)
        {
            List<TokenDatabaseData> tokens = _tokenProcessor.LoadTokenData();

            TokenDatabaseData? found = tokens.Find(x => x.Name == personName);

            string plainTextToken = "";

            TokenRequestParameters request = new TokenRequestParameters();
            request.Name = personName;
            request.IsCaptain = isCaptain;
            request.TokenPermissions = new List<string>() { "routes.read" };
            request.TokenDuration = new TimeSpan(2, 0, 0, 0, 0); // 2 days duration for captain


            plainTextToken = _tokenProcessor.CreateToken();

            // If existing, then we can update it.
            // Updateable fields: token, role, expiration date
            if (found != null)
            {
                TokenDatabaseData db = new TokenDatabaseData();
                db.Name = found.Name;
                db.Role = isCaptain? "Captain" : "Cadet";
                db.Prefix = found.Prefix;
                db.TokenHash = _tokenProcessor.HashToken(plainTextToken);
                db.ExpirationDate = (DateTime.Now + request.TokenDuration).ToString("yyyy-MM-dd HH:mm");
                db.Permissions = found.Permissions;

                _tokenProcessor.UpdateToken(db);
            }
            else
            {
                // Create a new one
                TokenDatabaseData db = new TokenDatabaseData();
                db.Name = personName;
                db.Role = isCaptain? "Captain" : "Cadet";
                db.Prefix = _tokenProcessor.GeneratePrefix();
                db.TokenHash = _tokenProcessor.HashToken(plainTextToken);
                db.ExpirationDate = (DateTime.Now + request.TokenDuration).ToString("yyyy-MM-dd HH:mm"); 
                db.Permissions = request.TokenPermissions;

                _tokenProcessor.SaveToken(db);
            }

            return plainTextToken;
        }


    }
}
