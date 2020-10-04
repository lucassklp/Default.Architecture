using Default.Architecture.Core;
using Domain;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UserRepository : AbstractRepository<User>
    {
        public UserRepository(DbContext daoContext) : base(daoContext)
        {
        }

        public async Task<bool> IsRegistredAsync(RegisterUserDto user)
        {
            return await Set().AnyAsync(x => x.Email == user.Email);
        }

        public Task<User> LoginAsync(ICredential credential)
        {
            return Set()
                .Include(u => u.UserRoles)
                    .ThenInclude(userRoles => userRoles.Role)
                .SingleOrDefaultAsync(x => x.Email.Equals(credential.Login) && x.Password.Equals(credential.Password));
        }
    }
}
