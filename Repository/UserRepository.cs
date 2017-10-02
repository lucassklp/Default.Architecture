using Domain;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository
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
            var user = context.Manipulate<User>().Where(x => x.Email == email && x.Password == password).ToList();
            return (user.Count > 0 ? user[0] : null);
        }

        public List<User> SelectAll()
        {
            return crud.SelectAll();
        }

    }
}
