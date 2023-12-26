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
    public class ingredientsRepositorySQL : IRepository<ingredients>
    {
        private pizzaContex dbContext;

        public ingredientsRepositorySQL(pizzaContex dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ingredients> GetList()
        {
            return dbContext.ingredients.ToList();
        }

        public IEnumerable<ingredients> GetAll()
        {
            return dbContext.ingredients;
        }
        public ingredients GetItem(int id)
        {
            return dbContext.ingredients.Find(id);
        }
        public void Create(ingredients item)
        {
            dbContext.ingredients.Add(item);
        }
        public void Update(ingredients item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            ingredients ingredients = dbContext.ingredients.Find(id);
            if (ingredients != null)
            {
                dbContext.ingredients.Remove(ingredients);
            }
        }
        public void Load()
        {
            dbContext.ingredients.Load();
        }

        public ingredients GetItemByNameOrHash(string name)
        {
            throw new NotImplementedException();
        }
    }
}
