using Galactic.Data.Models;

namespace Galactic.Data.Interfaces
{
    public interface ITokenDataFetching
    {
        List<TokenDatabaseData> LoadTokenData();
        TokenDatabaseData GetSingleToken(string personName);
        bool AddTokenData(TokenDatabaseData tokenData);
        bool UpdateTokenData(TokenDatabaseData data);
    }
}
