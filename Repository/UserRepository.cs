using Persistence;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using System;
using System.Reactive.Linq;

namespace Repository
{
    public class UserRepository : IUserRepository, IUserRepositoryAsync
    {
        private Crud<long, User> crud;
        private DaoContext context;

        public UserRepository(DaoContext daoContext)
        {
            this.context = daoContext;
            crud = new Crud<long, User>(daoContext);
        }

        public bool IsRegistred(User user)
        {
            return this.context.Manipulate<User>().Any(x => x.Email == user.Email);
        }

        public User Login(string email, string password)
        {

            var user = context.Manipulate<User>()
                .Include(u => u.UserRoles)
                    .ThenInclude(userRoles => userRoles.Role)
                .Single(x => x.Email == email && x.Password == password);


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

        public IObservable<User> LoginAsync(string email, string password)
        {
            return Observable.ToAsync<string, string, User>(this.Login)(email, password);
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
