using System;
using System.Reactive.Linq;
using Business.Exceptions;
using Business.Interfaces;
using Business.Validators;
using Domain;
using Domain.Entities;
using Extensions;
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

        public IObservable<User> Login(ICredential credential)
        {
            return validator.CheckAsync(new CredentialValidation(), credential)
                .SelectMany(result => {
                    return repository.Login(credential);
                });
        }
    }
}
