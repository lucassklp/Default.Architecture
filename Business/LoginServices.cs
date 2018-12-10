using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using Business.Exceptions;
using Business.Interfaces;
using Business.Validators;
using Domain;
using Domain.Entities;
using Repository;
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
                this.validator.Check(new CredentialValidation(), credential);
                return this.repository.Login(credential);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidCredentialException();
            }
        }

        public void Logout(ICredential credential)
        {
            throw new NotImplementedException();
        }
    }
}
