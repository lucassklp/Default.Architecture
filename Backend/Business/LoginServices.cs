using Business.Validators;
using Core;
using Domain;
using Domain.Entities;
using Core.Extensions;
using Persistence.Repository;
using System;
using Business.Validation;
using Business.Validation.Validators;
using Domain.Dtos;
using System.Threading.Tasks;
using Business.Exceptions;

namespace Business
{
    public class LoginServices
    {
        private UserRepository repository;

        public LoginServices(UserRepository repository)
        {
            this.repository = repository;
        }

        public User Login(ICredential credential)
        {
            credential.Password = credential.Password.ToSHA512();
            try
            {
                return repository.Login(credential);
            }
            catch(InvalidOperationException ex)
            {
                throw new InvalidCredentialException(ex);
            }
        }

        public async Task<User> LoginAsync(ICredential credential) => Login(credential);
    }
}
