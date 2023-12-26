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
    public class pizzaRepositorySQL : IRepository<pizza>
    {
        private pizzaContex dbContext;

        public pizzaRepositorySQL(pizzaContex dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<pizza> GetList()
        {
            return dbContext.pizzas.ToList();
        }
        public IEnumerable<pizza> GetAll()
        {
            return dbContext.pizzas;
        }
        public void Update(pizza pizza)
        {
            dbContext.Entry(pizza).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            pizza pizza = dbContext.pizzas.Find(id);
            if (pizza != null)
            {
                dbContext.pizzas.Remove(pizza);
            }
        }
        public pizza GetItem(int id) 
        { 
            return dbContext.pizzas.Find(id); 
        }
        public void Create(pizza pizza)
        {
            dbContext.pizzas.Add(pizza);
        }

        public void Load()
        {
            dbContext.pizzas.Load();
        }

        public pizza GetItemByNameOrHash(string name)
        {
            throw new NotImplementedException();
        }
    }
}
