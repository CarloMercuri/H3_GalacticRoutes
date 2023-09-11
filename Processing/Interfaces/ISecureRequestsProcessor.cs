namespace Galactic.Processing.Interfaces
{
    public interface ISecureRequestsProcessor
    {
        string CreateToken(string personName, bool isCaptain);
    }
}
