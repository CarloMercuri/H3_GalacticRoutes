using Galactic.Data.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Galactic.Data.Encryption
{
    public class Encryptor : ITokenEncryption
    {
        private static int HashInterations = 10000;
        private static int HashSize = 128;

        private static int AuthTokenSize = 128;
        private static int PrefixSize = 8;

        public string HashToken(string token)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return Convert.ToBase64String(algorithm.ComputeHash(Encoding.UTF8.GetBytes(token)));
        }

        public string GeneratePrefix()
        {
            var tokenBytes = new byte[PrefixSize];

            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetNonZeroBytes(tokenBytes);
            }

            return Convert.ToBase64String(tokenBytes);
        }

        public string GenerateBase64Accesstoken()
        {
            var tokenBytes = new byte[AuthTokenSize];

            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetNonZeroBytes(tokenBytes);
            }

            return Convert.ToBase64String(tokenBytes);
        }
    }
}
