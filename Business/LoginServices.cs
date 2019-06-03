using Business.Validators;
using Core;
using Domain;
using Domain.Entities;
using Extensions;
using Repository;
using System;

namespace Business
{
    public class LoginServices
    {
        private UserRepository repository;
        private ValidatorService validator;

        public LoginServices(UserRepository repository, ValidatorService validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public User Login(ICredential credential)
        {
            validator.Check(new CredentialValidation(), credential);
            credential.Password = credential.Password.ToSHA512();
            return repository.Login(credential);
        }

        public IObservable<User> LoginAsync(ICredential credential) => SingleObservable.Create(() => Login(credential));
    }
}
