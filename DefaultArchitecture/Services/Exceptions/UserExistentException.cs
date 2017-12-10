using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Services.Exceptions
{
    public class UserExistentException : Exception
    {
        public UserExistentException(User user) : base($"The user with email {user.Email} already exists")
        {

        }
    }
}
