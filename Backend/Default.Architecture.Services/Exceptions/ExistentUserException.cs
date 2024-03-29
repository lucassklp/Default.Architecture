﻿using Domain.Dtos;

namespace Default.Architecture.Services.Exceptions
{
    public class ExistentUserException : BusinessException
    {
        public ExistentUserException(RegisterUserDto user)
            : base($"The user with email {user.Email} already exists", "existent-user")
        {

        }
    }
}
