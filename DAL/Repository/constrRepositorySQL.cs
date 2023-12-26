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
    public class constrRepositorySQL : IRepository<constr>
    {
        private pizzaContex dbContext;

        public constrRepositorySQL(pizzaContex dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<constr> GetList()
        {
            return dbContext.constrs.ToList();
        }

        public constr GetItemByNameOrHash(string name)
        {
            return dbContext.constrs.FirstOrDefault(c => c.hashPizza == name);
        }

        public IEnumerable<constr> GetAll()
        {
            return dbContext.constrs;
        }
        public void Update(constr constr)
        {
            dbContext.Entry(constr).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            constr constr = dbContext.constrs.Find(id);
            if (constr != null)
            {
                dbContext.constrs.Remove(constr);
            }
        }
        public constr GetItem(int id)
        {
            return dbContext.constrs.Find(id);
        }
        public void Create(constr constr)
        {
            dbContext.constrs.Add(constr);
        }

        public void Load()
        {
            dbContext.constrs.Load();
        }
    }
}
