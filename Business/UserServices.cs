using Business.Exceptions;
using Business.Interfaces;
using Repository.Interfaces;
using Extensions;
using Domain.Entities;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Business.Validators;

namespace Business
{
    public class UserServices : IUserServices
    {
        IUserRepository userRepository;
        IUserRepositoryAsync userRepositoryAsync;
        private ValidatorService validator;
        public UserServices(IUserRepository userRepository, IUserRepositoryAsync userRepositoryAsync, ValidatorService validator)
        {
            this.userRepository = userRepository;
            this.userRepositoryAsync = userRepositoryAsync;
            this.validator = validator;
        }

        public User Register(User user)
        {
            user.Password = user.Password.ToSHA512();
            if (this.userRepository.IsRegistred(user))
            {
                throw new UserExistentException(user);
            }
            else
            {
                return userRepository.Register(user);
            }
        }

        public IObservable<User> RegisterAsync(User user)
        {
            return this.validator.CheckAsync(new RegisterUserValidation(), user).SelectMany(validator =>
            {
                return this.userRepositoryAsync.IsRegistredAsync(user).SelectMany(isRegistred =>
                {
                    if (!isRegistred)
                    {
                        user.Password = user.Password.ToSHA512();
                        return this.userRepositoryAsync.RegisterAsync(user);
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
