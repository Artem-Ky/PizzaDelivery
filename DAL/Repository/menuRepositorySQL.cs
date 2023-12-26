using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class menuRepositorySQL : IRepository<menu>
    {
        private pizzaContex dbContext;

        public menuRepositorySQL(pizzaContex dbContext)
        {
            this.dbContext = dbContext;
            this.dbContext.Database.Log = LogSql;
        }
            private void LogSql(string sql)
    {
        Console.WriteLine(sql);
    }

        public List<menu> GetList()
        {
            try
            {
                var query = dbContext.menu
                    .Select(m => new
                    {
                        m.id,
                        m.Photo,
                        m.isAvalaible,
                        m.name,
                        m.cost,
                        m.about,
                        m.pizza_type_id,
                        m.weight
                    })
                    .AsEnumerable() // Переключаемся на выполнение оставшейся части запроса на стороне клиента
                    .Select(m => new menu
                    {
                        id = m.id,
                        Photo = m.Photo,
                        isAvalaible = m.isAvalaible,
                        name = m.name,
                        cost = m.cost,
                        about = m.about,
                        pizza_type_id = m.pizza_type_id,
                        weight = m.weight

                    });

                var sql = query.ToString(); // Получаем строку запроса для отладки
                Console.WriteLine(sql);    // Выводим запрос в лог

                return query.ToList(); // Возвращаем результат запроса в виде списка menu
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting menu list from the database: {ex}");
                throw;
            }
        }



        public IEnumerable<menu> GetAll()
        {
            return dbContext.menu;
        }
        public void Update(menu menu)
        {
            dbContext.Entry(menu).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            menu menu = dbContext.menu.Find(id);
            if (menu != null)
            {
                dbContext.menu.Remove(menu);
            }
        }
        public menu GetItem(int id)
        {
            return dbContext.menu.Find(id);
        }
        public void Create(menu menu)
        {
            dbContext.menu.Add(menu);
        }

        public void Load()
        {
            dbContext.menu.Load();
        }

        public menu GetItemByNameOrHash(string name)
        {
            throw new NotImplementedException();
        }
    }
}
