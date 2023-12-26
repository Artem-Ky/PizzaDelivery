using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Model;
using System.Data.Entity;
using Interfaces.Services;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ServicesPizza : IServicesPizza
    {
        IDbRepository dbContext;
        public ServicesPizza(IDbRepository repos)
        {
            dbContext = repos;
        }
        public List<DTOmenu> GetAllPizza()
        {
            return dbContext.Rmenu.GetList().Select(i => new DTOmenu(i.id, i.name, i.cost, i.about, i.isAvalaible, i.pizza_type_id, i.Photo, 0)).ToList();
        }
        public List<DTOpizzaType> GetAllType()
        {
            return dbContext.RpizzaType
                .GetList()
                .Select(i => new DTOpizzaType(i.id, i.Pizza_type))
                .ToList();
        }
    }
}
