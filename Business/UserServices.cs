using Business.Exceptions;
using Business.Interfaces;
using Repository.Interfaces;
using Extensions;
using Domain.Entities;
using System;
using System.Reactive.Linq;

namespace Business
{
    public class UserServices : IUserServices
    {
        IUserRepository userRepository;
        IUserRepositoryAsync userRepositoryAsync;

        public UserServices(IUserRepository userRepository, IUserRepositoryAsync userRepositoryAsync)
        {
            this.userRepository = userRepository;
            this.userRepositoryAsync = userRepositoryAsync;
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
            throw new NotImplementedException();
            //return Observable.Create<User>(x => user);
            //this.userRepositoryAsync.IsRegistredAsync(user).Subscribe(IsRegistred =>
            //{
            //    if (!IsRegistred)
            //    {
            //        this.userRepositoryAsync.RegisterAsync(user);
            //        ac.Invoke();
            //    }
            //    else throw new UserExistentException(user);
            //}, 
            //ex => {

            //}, )
        }

    }
}
