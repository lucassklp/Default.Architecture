using Business.Exceptions;
using Business.Interfaces;
using Repository.Interfaces;
using Extensions;
using Domain.Entities;
using System;
using System.Reactive.Linq;
using Business.Validators;

namespace Business
{
    public class UserServices : IUserServices
    {
        IUserRepository userRepository;
        private ValidatorService validator;
        ICrud<long, User> crud;
        public UserServices(IUserRepository userRepository, ValidatorService validator, ICrud<long, User> crud)
        {
            this.userRepository = userRepository;
            this.validator = validator;
            this.crud = crud;
        }

        public IObservable<User> RegisterAsync(User user)
        {
            return this.validator.CheckAsync(new RegisterUserValidation(), user).SelectMany(validator =>
            {
                return this.userRepository.IsRegistred(user).SelectMany(isRegistred =>
                {
                    if (!isRegistred)
                    {
                        user.Password = user.Password.ToSHA512();
                        return this.crud.Create(user);
                    }
                    else
                    {
                        throw new UserExistentException(user);
                    }
                });
            });
        }
    }
}
