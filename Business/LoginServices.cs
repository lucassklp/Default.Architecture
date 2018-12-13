using System;
using System.Reactive.Linq;
using Business.Exceptions;
using Business.Interfaces;
using Business.Validators;
using Domain;
using Domain.Entities;
using Repository.Interfaces;

namespace Business
{
    public class LoginServices : ILoginServices
    {
        private IUserRepository repository;
        private ValidatorService validator;

        public LoginServices(IUserRepository repository, ValidatorService validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public User Login(ICredential credential)
        {
            try
            {
                validator.Check(new CredentialValidation(), credential);
                return repository.Login(credential);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidCredentialException();
            }
        }

        public void Logout(ICredential credential)
        {
            
        }

        public IObservable<User> LoginAsync(ICredential credential)
        {
            return validator.CheckAsync(new CredentialValidation(), credential)
                .SelectMany(result => repository.LoginAsync(credential));
        }
    }
}
