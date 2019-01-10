using Domain.Entities;
using System;

namespace Business.Interfaces
{
    public interface IUserServices
    {
        IObservable<User> RegisterAsync(User user);
    }
}
