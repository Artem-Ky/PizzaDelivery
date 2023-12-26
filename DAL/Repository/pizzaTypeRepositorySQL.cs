using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class pizzaTypeRepositorySQL : IRepository<pizzaType>
    {
        private pizzaContex dbContext;

        public pizzaTypeRepositorySQL(pizzaContex dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<pizzaType> GetList()
        {
            return dbContext.pizzaTypes.ToList();
        }

        public IEnumerable<pizzaType> GetAll()
        {
            return dbContext.pizzaTypes;
        }

        public pizzaType GetItem(int id)
        {
            return dbContext.pizzaTypes.Find(id);
        }

        public void Create(pizzaType item)
        {
            dbContext.pizzaTypes.Add(item);
        }

        public void Update(pizzaType item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            pizzaType user = dbContext.pizzaTypes.Find(id);
            if (user != null)
            {
                dbContext.pizzaTypes.Remove(user);
            }
        }

        public void Load()
        {
            dbContext.users.Load();
        }

        public pizzaType GetItemByNameOrHash(string name)
        {
            throw new NotImplementedException();
        }
    }
}
