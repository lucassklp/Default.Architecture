using Domain;
using Domain.Entities;
using System;

namespace Business.Interfaces
{
    public interface ILoginServices
    {
        IObservable<User> Login(ICredential credential);
    }
}
