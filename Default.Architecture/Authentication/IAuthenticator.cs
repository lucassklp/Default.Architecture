using System;

namespace Default.Architecture.Authentication
{
    public interface IAuthenticator<T> where T: class
    {
        string Login(T identity);
    }

    public interface IAuthenticatorAsync<T> where T: class
    {
        IObservable<string> LoginAsync(T identity);
    }
}
