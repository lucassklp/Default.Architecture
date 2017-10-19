using Domain;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        User Login(string email, string password);
        List<User> SelectAll();
    }
}
