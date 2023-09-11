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

        public string GenerateEmailActivationString()
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return new string(Enumerable.Repeat(chars, 64)
              .Select(s => s[random.Next(s.Length)]).ToArray());
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

        public string GenerateSalt()
        {
            return Convert.ToBase64String(GenerateSaltBytes());
        }

        public byte[] GenerateSaltBytes()
        {
            /*
             RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
             byte[] salt = new byte[128];
             randomNumberGenerator.GetBytes(salt);
            */

            var saltBytes = new byte[HashSize];

            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetNonZeroBytes(saltBytes);
            }

            return saltBytes;
        }
    }
}
