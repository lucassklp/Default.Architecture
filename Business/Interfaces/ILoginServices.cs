using Domain;
using Domain.Entities;
using System;

namespace Business.Interfaces
{
    public interface ILoginServices
    {
        User Login(ICredential credential);
        void Logout(ICredential credential);
        IObservable<User> LoginAsync(ICredential credential);
    }
}
