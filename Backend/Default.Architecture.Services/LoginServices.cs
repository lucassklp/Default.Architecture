﻿using Domain;
using Domain.Entities;
using Default.Architecture.CrossCutting.Extensions;
using Persistence.Repository;
using System;
using System.Threading.Tasks;
using Default.Architecture.Services.Exceptions;

namespace Default.Architecture.Services
{
    public class LoginServices
    {
        private UserRepository repository;

        public LoginServices(UserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<User> LoginAsync(ICredential credential)
        {
            credential.Password = credential.Password.ToSha512();
            try
            {
                return await repository.LoginAsync(credential);
            }
            catch(InvalidOperationException ex)
            {
                throw new InvalidCredentialException(ex);
            }
        }
    }
}
