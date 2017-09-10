using DefaultArchitecture.Domain;
using DefaultArchitecture.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Repository
{
    public class UserRepository
    {
        private Crud<User> crud;

        public UserRepository()
        {
            crud = Crud<User>.GetInstance();
        }

        public User Login(string email, string password)
        {
            DaoContext context = new DaoContext();
            var user = context.Manipulate<User>().Where(x => x.Email == email && x.Password == password).ToList();
            return (user.Count > 0 ? user[0] : null);
        }

        public List<User> SelectAll()
        {
            return crud.SelectAll();
        }

    }
}
