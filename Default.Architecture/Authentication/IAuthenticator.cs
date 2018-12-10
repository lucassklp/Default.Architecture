namespace Default.Architecture.Authentication
{
    public interface IAuthenticator<T> where T: class
    {
        string Login(T identity);
        string Logout(T identity);
    }
}
