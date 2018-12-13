using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IUserRepositoryAsync
    {
        IObservable<bool> IsRegistredAsync(User user);
        IObservable<User> LoginAsync(ICredential credential);
        IObservable<List<User>> SelectAllAsync();
        IObservable<User> RegisterAsync(User user);
    }
}
