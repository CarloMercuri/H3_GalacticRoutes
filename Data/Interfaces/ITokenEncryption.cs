namespace Galactic.Data.Interfaces
{
    public interface ITokenEncryption
    {
        string GenerateBase64Accesstoken();
        string HashToken(string token);
        string GeneratePrefix();


    }
}
