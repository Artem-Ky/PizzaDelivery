using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class CustomerRepositorySQL : IRepository<customer>
    {
        private pizzaContex dbContext;

        public CustomerRepositorySQL(pizzaContex dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool AuthenticateUser(NetworkCredential credential)
        {
            var validUser = dbContext.customers
                .Any(user => user.name == credential.UserName && user.password == credential.Password);

            return validUser;
        }
        public List<customer> GetList()
        {
            return dbContext.customers.ToList();
        }
        public IEnumerable<customer> GetAll()
        {
            return dbContext.customers;
        }
        public void Update(customer Customer)
        {
            dbContext.Entry(Customer).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            customer customer = dbContext.customers.Find(id);
            if (customer != null)
            {
                dbContext.customers.Remove(customer);
            }
        }
        public customer GetItem(int id)
        {
            return dbContext.customers.Find(id);
        }
        public void Create(customer Customer)
        {
            dbContext.customers.Add(Customer);
        }

        public void Load()
        {
            dbContext.customers.Load();
        }

        public customer GetItemByNameOrHash(string name)
        {
            throw new NotImplementedException();
        }
    }
}

