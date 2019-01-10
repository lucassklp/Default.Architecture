using Domain;
using Domain.Entities;
using System;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        IObservable<bool> IsRegistred(User user);
        IObservable<User> Login(ICredential credential);
    }
}
