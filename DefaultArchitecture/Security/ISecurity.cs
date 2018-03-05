namespace Security
{
    public interface ISecurity<T> where T: class
    {
        string Login(T identity);
        string Logout(T identity);
    }
}
