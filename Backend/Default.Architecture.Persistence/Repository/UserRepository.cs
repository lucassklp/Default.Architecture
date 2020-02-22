using Default.Architecture.Core;
using Domain;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace Persistence.Repository
{
    public class UserRepository : AbstractRepository<User>
    {
        public UserRepository(DbContext daoContext) : base(daoContext)
        {
        }

        public bool IsRegistred(User user)
        {
            return Set().Any(x => x.Email == user.Email);
        }

        public IObservable<bool> IsRegistredAsync(User user)
        {
            return SingleObservable.Create(() => IsRegistred(user));
        }

        public User Login(ICredential credential)
        {
            return Set()
                .Include(u => u.UserRoles)
                    .ThenInclude(userRoles => userRoles.Role)
                .Single(x => x.Email.Equals(credential.Login) && x.Password.Equals(credential.Password));
        }


        public IObservable<User> LoginAsync(ICredential credential)
        {
            return SingleObservable.Create(() => Login(credential));
        }
    }
}
