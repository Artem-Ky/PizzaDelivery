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
    public class ingredients_pizzaRepositorySQL : IRepository<ingredients_pizza>
    {
        private pizzaContex dbContext;

        public ingredients_pizzaRepositorySQL(pizzaContex dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<ingredients_pizza> GetList()
        {
            return dbContext.ingredients_pizzas.ToList();
        }

        public IEnumerable<ingredients_pizza> GetAll()
        {
            return dbContext.ingredients_pizzas;
        }

        public ingredients_pizza GetItem(int id)
        {
            return dbContext.ingredients_pizzas.Find(id);
        }

        public void Create(ingredients_pizza item)
        {
            dbContext.ingredients_pizzas.Add(item);
        }

        public void Update(ingredients_pizza item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ingredients_pizza user = dbContext.ingredients_pizzas.Find(id);
            if (user != null)
            {
                dbContext.ingredients_pizzas.Remove(user);
            }
        }

        public void Load()
        {
            dbContext.ingredients_pizzas.Load();
        }

        public ingredients_pizza GetItemByNameOrHash(string name)
        {
            throw new NotImplementedException();
        }
    }
}
