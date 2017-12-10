using Domain;
using Persistence;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private Crud<User> crud;
        private DaoContext context;

        public UserRepository(DaoContext daoContext)
        {
            this.context = daoContext;
            crud = new Crud<User>(daoContext);
        }

        public bool Exists(User user)
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

    }
}
