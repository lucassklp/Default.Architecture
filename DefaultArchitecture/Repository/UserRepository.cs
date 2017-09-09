using DefaultArchitecture.Domain;
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

        public List<User> SelectAll()
        {
            return crud.SelectAll();
        }

    }
}
