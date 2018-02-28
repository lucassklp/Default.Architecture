using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IUserRepositoryAsync
    {
        IObservable<bool> IsRegistredAsync(User user);
        IObservable<User> LoginAsync(string email, string password);
        IObservable<List<User>> SelectAllAsync();
        IObservable<User> RegisterAsync(User user);
    }
}
