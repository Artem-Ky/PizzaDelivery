using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repository
{
    public class courierRepositorySQL : IRepository<courier>
    {
        private pizzaContex dbContext;

        public courierRepositorySQL(pizzaContex dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<courier> GetList()
        {
            return dbContext.couriers.ToList();
        }

        public IEnumerable<courier> GetAll()
        {
            return dbContext.couriers;
        }

        public courier GetItem(int id)
        {
            return dbContext.couriers.Find(id);
        }

        public void Create(courier item)
        {
            dbContext.couriers.Add(item);
        }

        public void Update(courier item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            courier user = dbContext.couriers.Find(id);
            if (user != null)
            {
                dbContext.couriers.Remove(user);
            }
        }

        public void Load()
        {
            dbContext.users.Load();
        }

        public courier GetItemByNameOrHash(string name)
        {
            throw new NotImplementedException();
        }
    }
}
