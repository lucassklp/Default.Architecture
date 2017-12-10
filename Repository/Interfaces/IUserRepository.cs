using Domain;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        bool Exists(User user);
        User Login(string email, string password);
        List<User> SelectAll();
        User Register(User user);
    }
}
