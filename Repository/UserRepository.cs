using Persistence;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using System;
using System.Reactive.Linq;
using Domain;
using System.Reactive.Disposables;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private DaoContext context;

        public UserRepository(DaoContext daoContext)
        {
            this.context = daoContext;
        }

        public IObservable<bool> IsRegistred(User user)
        {
            return Observable.Create<bool>(observer =>
            {
                observer.OnNext(this.context.Manipulate<User>().Any(x => x.Email == user.Email));
                return Disposable.Empty;
            });
        }

        public IObservable<User> Login(ICredential credential)
        {
            return Observable.Create<User>(observer =>
            {
                var user = context.Manipulate<User>()
                    .Include(u => u.UserRoles)
                        .ThenInclude(userRoles => userRoles.Role)
                    .Single(x => x.Login == credential.Login && x.Password == credential.Password);

                observer.OnNext(user);
                return Disposable.Empty;
            });
        }
    }
}
