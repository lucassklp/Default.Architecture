using Business.Exceptions;
using Business.Validators;
using Domain.Entities;
using Core.Extensions;
using Persistence.Repository;
using System;
using System.Reactive.Linq;
using Business.Validation.Validators;

namespace Business
{
    public class UserServices
    {
        UserRepository userRepository;
        Crud<long, User> crud;
        public UserServices(UserRepository userRepository, Crud<long, User> crud)
        {
            this.userRepository = userRepository;
            this.crud = crud;
        }

        public IObservable<User> RegisterAsync(User user)
        {
            return userRepository.IsRegistredAsync(user).Select(isRegistred =>
            {
                if (!isRegistred)
                {
                    user.Password = user.Password.ToSHA512();
                    return this.crud.Create(user);
                }
                else
                {
                    throw new ExistentUserException(user);
                }
            });
        }
    }
}
