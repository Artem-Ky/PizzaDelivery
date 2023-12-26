using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class DTOconstr : DTOpizza 
    {
        public string HashPizza { get; set; }
        public List<DTOingredients> IngrList { get; set; }
        public int Weight { get; set; }
        public DTOconstr()
        {
        }

        public DTOconstr(int id, string name, int cost, string about, int pizza_type_id, string hashPizza, int count, List<DTOingredients> ingt, int allpizzacost, int weight)
            : base(id, name, cost, about, pizza_type_id, count)
        {
            HashPizza = hashPizza;
            IngrList = ingt;
            allPizzaCost = allpizzacost;
            Weight = weight;

        }
    }
}
