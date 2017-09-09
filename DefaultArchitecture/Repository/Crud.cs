using DefaultArchitecture.Domain;
using DefaultArchitecture.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Repository
{
    public class Crud<T>
        where T : class, Identifiable
    {
        private DaoContext context;
        private static Crud<T> instance;

        private Crud()
        {
            context = new DaoContext();
        }

        public static Crud<T> GetInstance()
        {
            if (instance == null)
                instance = new Crud<T>();
            return instance;
        }

        public void Create(T item)
        {
            context.Manipulate<T>().Add(item);
            context.SaveChanges();
        }

        public T Delete(long id)
        {
            var selectedItem = context.Manipulate<T>().Where(x => x.ID == id).FirstOrDefault();
            context.Manipulate<T>().Remove(selectedItem);
            context.SaveChanges();
            return selectedItem;
        }

        public T Read(long id)
        {
            var selectedItem = context.Manipulate<T>().Where(x => x.ID == id).FirstOrDefault();
            return selectedItem;
        }

        public List<T> SelectAll()
        {
            return context.Manipulate<T>().ToList();
        }

        public T Update(T item)
        {
            //context.Manipulate<T>().AddOrUpdate<T>(item);
            //context.SaveChanges();
            return item;
        }
    }
}
