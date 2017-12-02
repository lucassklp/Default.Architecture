using Domain;
using Persistence;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public User Login(string email, string password)
        {

            var user = context.Manipulate<User>()
                .Include(u => u.UserRoles)
                    .ThenInclude(userRoles => userRoles.Role)
                .Single(x => x.Email == email && x.Password == password);


            return user;
        }

        public List<User> SelectAll()
        {
            return crud.SelectAll();
        }

    }
}
