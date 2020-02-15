using System;

namespace Default.Architecture.Authentication
{
    public interface IAuthenticator<T> where T : class
    {
        IObservable<string> Login(T identity);
    }
}
