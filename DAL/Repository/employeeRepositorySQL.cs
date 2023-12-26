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
    public class employeeRepositorySQL : IRepository<employee>
    {
        private pizzaContex dbContext;

        public employeeRepositorySQL(pizzaContex dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<employee> GetList()
        {
            return dbContext.employees.ToList();
        }

        public IEnumerable<employee> GetAll()
        {
            return dbContext.employees;
        }

        public employee GetItem(int id)
        {
            return dbContext.employees.Find(id);
        }

        public void Create(employee item)
        {
            dbContext.employees.Add(item);
        }

        public void Update(employee item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            employee user = dbContext.employees.Find(id);
            if (user != null)
            {
                dbContext.employees.Remove(user);
            }
        }

        public void Load()
        {
            dbContext.employees.Load();
        }

        public employee GetItemByNameOrHash(string name)
        {
            throw new NotImplementedException();
        }
    }

}
