using Galactic.Data.Models;

namespace Galactic.Data.Interfaces
{
    public interface ITokenProcessor
    {
        List<TokenDatabaseData> LoadTokenData();
        TokenDatabaseData GetSingleTokenData(string token);
        string CreateToken();
        string GeneratePrefix();
        string HashToken(string token);

        bool SaveToken(TokenDatabaseData data);
        bool UpdateToken(TokenDatabaseData data);
    }
}
