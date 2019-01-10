using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        IObservable<bool> IsRegistred(User user);
        IObservable<User> Login(ICredential credential);
    }
}
