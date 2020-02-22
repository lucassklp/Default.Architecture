using Domain.Entities;
using Default.Architecture.Core.Extensions;
using Persistence.Repository;
using System;
using System.Reactive.Linq;
using Default.Architecture.Business.Exceptions;

namespace Default.Architecture.Business
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
                    user.Password = user.Password.ToSha512();
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
