using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ingredientTypeRepositorySQL : IRepository<ingredientType>
    {
        private pizzaContex dbContext;

        public ingredientTypeRepositorySQL(pizzaContex dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<ingredientType> GetList()
        {
            return dbContext.ingredientTypes.ToList();
        }

        public IEnumerable<ingredientType> GetAll()
        {
            return dbContext.ingredientTypes;
        }

        public ingredientType GetItem(int id)
        {
            return dbContext.ingredientTypes.Find(id);
        }

        public void Create(ingredientType item)
        {
            dbContext.ingredientTypes.Add(item);
        }

        public void Update(ingredientType item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ingredientType user = dbContext.ingredientTypes.Find(id);
            if (user != null)
            {
                dbContext.ingredientTypes.Remove(user);
            }
        }

        public void Load()
        {
            dbContext.statuses.Load();
        }

        public ingredientType GetItemByNameOrHash(string name)
        {
            throw new NotImplementedException();
        }
    }
}
