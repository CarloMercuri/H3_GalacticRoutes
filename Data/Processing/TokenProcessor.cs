using Galactic.Data.Interfaces;
using Galactic.Data.Models;

namespace Galactic.Data.Processing
{
    public class TokenProcessor : ITokenProcessor
    {
        ITokenEncryption _encryptor;
        ITokenDataFetching _fetcher;
        public TokenProcessor(ITokenEncryption encryptor, ITokenDataFetching _dataFetcher)
        {
            _encryptor = encryptor;
            _fetcher = _dataFetcher;
        }

        public string CreateToken()
        {
            string token = _encryptor.GenerateBase64Accesstoken();

            return token;
        }

        public string GeneratePrefix()
        {
            return _encryptor.GeneratePrefix();
        }

        public TokenDatabaseData GetSingleTokenData(string inputToken)
        {
            List<TokenDatabaseData> allTokens = _fetcher.LoadTokenData();

            string hashedInputToken = _encryptor.HashToken(inputToken);

            foreach(TokenDatabaseData token in allTokens)
            {
                if(token.TokenHash == hashedInputToken)
                {
                    return token;
                }
            }

            // if not found
            return null;
        }

        public string HashToken(string token)
        {
            return _encryptor.HashToken(token);
        }

        public List<TokenDatabaseData> LoadTokenData()
        {
            return _fetcher.LoadTokenData();
        }

        public bool SaveToken(TokenDatabaseData data)
        {
            return _fetcher.AddTokenData(data);
        }

        public bool UpdateToken(TokenDatabaseData data)
        {
            return _fetcher.UpdateTokenData(data);
        }
    }
}
