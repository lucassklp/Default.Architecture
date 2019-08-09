using Business.Validators;
using Core;
using Domain;
using Domain.Entities;
using Extensions;
using Repository;
using System;
using Business.Validation;
using Business.Validation.Validators;

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
            return repository.Login(credential);
        }

        public IObservable<User> LoginAsync(ICredential credential) => 
            SingleObservable.Create(() => Login(credential));
    }
}
