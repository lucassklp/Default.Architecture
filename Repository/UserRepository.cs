using Persistence;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using System;
using System.Reactive.Linq;
using Domain;

namespace Repository
{
    public class UserRepository : IUserRepository, IUserRepositoryAsync
    {
        private ICrud<long, User> crud;
        private DaoContext context;

        public UserRepository(DaoContext daoContext, ICrud<long, User> crud)
        {
            this.context = daoContext;
            this.crud = crud;
        }

        public bool IsRegistred(User user)
        {
            return this.context.Manipulate<User>().Any(x => x.Email == user.Email);
        }
        

        public User Login(ICredential credential)
        {

            var user = context.Manipulate<User>()
                .Include(u => u.UserRoles)
                    .ThenInclude(userRoles => userRoles.Role)
                .Single(x => x.Login == credential.Login && x.Password == credential.Password);


            return user;
        }

        public User Register(User user)
        {
            this.crud.Create(user);
            return user;
        }

        public List<User> SelectAll()
        {
            return crud.SelectAll();
        }

        public IObservable<bool> IsRegistredAsync(User user)
        {
            return Observable.ToAsync<User, bool>(this.IsRegistred)(user);
        }

        public IObservable<User> LoginAsync(ICredential credential)
        {
            return Observable.ToAsync<ICredential, User>(this.Login)(credential);
        }

        public IObservable<User> RegisterAsync(User user)
        {
            return Observable.ToAsync<User, User>(this.Register)(user);
        }

        public IObservable<List<User>> SelectAllAsync()
        {
            return Observable.ToAsync(this.SelectAll)();
        }

    }
}
