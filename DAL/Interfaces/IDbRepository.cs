using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDbRepository
    {
        IRepository<courier> Rcourier { get; }
        IRepository<employee> Remployee { get; }
        IRepository<customer> Rcustomer { get; }
        IRepository<ingredients> Ringredients { get; }
        IRepository<ingredients_pizza> Ringredients_pizza { get; }
        IRepository<orders_> Rorders { get; }
        IRepository<orders_pizza> Rorders_pizza { get; }
        IRepository<pizza> Rpizza { get; }
        IRepository<menu> Rmenu { get; }
        IRepository<constr> Rconstr { get; }
        IRepository<pizzaType> RpizzaType { get; }
        IRepository<status> Rstatus { get; }
        IRepository<user> Ruser { get; }
        IRepository<ingredientType> RingredientType { get; }
        IRepositoryReport Rreports { get; }
        int Save();
        void SaveUpdate();
    }
}
