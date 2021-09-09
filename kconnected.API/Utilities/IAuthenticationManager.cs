namespace kconnected.API.Utilities
{
    public interface IAuthenticationManager
    {
        string Authenticate(string email, string password);
    }
}