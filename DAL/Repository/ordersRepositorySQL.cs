using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using System.Data.Entity;

namespace DAL.Repository
{
    public class ordersRepositorySQL : IRepository<orders_>
    {
        private pizzaContex dbContext;

        public ordersRepositorySQL(pizzaContex dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<orders_> GetList()
        {
            return dbContext.orders.ToList();
        }

        public IEnumerable<orders_> GetAll()
        {
            return dbContext.orders;
        }
        public orders_ GetItem(int id)
        {
            return dbContext.orders.Find(id);
        }
        public void Create(orders_ item)
        {
            dbContext.orders.Add(item);
        }
        public void Update(orders_ item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            orders_ orders_ = dbContext.orders.Find(id);
            if (orders_ != null)
            {
                dbContext.orders.Remove(orders_);
            }
        }
        public void Load()
        {
            dbContext.orders.Load();
        }

        public orders_ GetItemByNameOrHash(string name)
        {
            throw new NotImplementedException();
        }
    }
}