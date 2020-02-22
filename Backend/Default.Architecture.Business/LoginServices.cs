using Default.Architecture.Business.Validators;
using Default.Architecture.Core;
using Domain;
using Domain.Entities;
using Default.Architecture.Core.Extensions;
using Persistence.Repository;
using System;
using Default.Architecture.Business.Validation;
using Default.Architecture.Business.Validation.Validators;
using Domain.Dtos;
using System.Threading.Tasks;
using Default.Architecture.Business.Exceptions;

namespace Default.Architecture.Business
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
            credential.Password = credential.Password.ToSha512();
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
