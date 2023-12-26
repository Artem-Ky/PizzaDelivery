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
    public class statusRepositorySQL : IRepository<status>
    {
        private pizzaContex dbContext;

        public statusRepositorySQL(pizzaContex dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<status> GetList()
        {
            return dbContext.statuses.ToList();
        }

        public IEnumerable<status> GetAll()
        {
            return dbContext.statuses;
        }

        public status GetItem(int id)
        {
            return dbContext.statuses.Find(id);
        }

        public void Create(status item)
        {
            dbContext.statuses.Add(item);
        }

        public void Update(status item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            status user = dbContext.statuses.Find(id);
            if (user != null)
            {
                dbContext.statuses.Remove(user);
            }
        }

        public void Load()
        {
            dbContext.statuses.Load();
        }

        public status GetItemByNameOrHash(string name)
        {
            throw new NotImplementedException();
        }
    }
}
