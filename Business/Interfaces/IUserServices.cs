using Domain.Entities;
using System;

namespace Business.Interfaces
{
    public interface IUserServices
    {
        User Register(User user);

        IObservable<User> RegisterAsync(User user);
    }
}
