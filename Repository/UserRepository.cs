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
                .Include(x => x.UserRoles)
                .Where(x => x.Email == email && x.Password == password)
                .ToList();
            return (user.Count > 0 ? user[0] : null);

        }

        public List<User> SelectAll()
        {
            return crud.SelectAll();
        }

    }
}
