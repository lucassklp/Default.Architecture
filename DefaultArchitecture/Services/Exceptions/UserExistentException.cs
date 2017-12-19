using Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Services.Exceptions
{
    public class UserExistentException : DefaultException
    {
        public UserExistentException(User user) : base($"The user with email {user.Email} already exists", SystemErrorCode.UserAlreadyExists)
        {

        }
    }
}
