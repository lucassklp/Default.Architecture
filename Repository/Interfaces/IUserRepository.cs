using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        bool IsRegistred(User user);
        User Login(ICredential credential);
        List<User> SelectAll();
        User Register(User user);
    }
}
