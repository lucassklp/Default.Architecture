using Domain.Entities;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        bool IsRegistred(User user);
        User Login(string email, string password);
        List<User> SelectAll();
        User Register(User user);
    }
}
