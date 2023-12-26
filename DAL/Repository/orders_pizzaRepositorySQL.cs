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
    public class orders_pizzaRepositorySQL : IRepository<orders_pizza>
    {
        private pizzaContex dbContext;

        public orders_pizzaRepositorySQL(pizzaContex dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<orders_pizza> GetList()
        {
            return dbContext.orders_pizza.ToList();
        }

        public IEnumerable<orders_pizza> GetAll()
        {
            return dbContext.orders_pizza;
        }
        public orders_pizza GetItem(int id)
        {
            return dbContext.orders_pizza.Find(id);
        }
        public void Create(orders_pizza item)
        {
            dbContext.orders_pizza.Add(item);
        }
        public void Update(orders_pizza item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            orders_pizza orders_dish = dbContext.orders_pizza.Find(id);
            if (orders_dish != null)
            {
                dbContext.orders_pizza.Remove(orders_dish);
            }
        }
        public void Load()
        {
            dbContext.orders.Load();
        }

        public orders_pizza GetItemByNameOrHash(string name)
        {
            throw new NotImplementedException();
        }
    }
}