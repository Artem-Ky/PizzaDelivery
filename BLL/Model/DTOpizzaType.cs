using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class DTOpizzaType
    {
        public int Id { get; set; }
        public string PizzaType { get; set; }

        public DTOpizzaType()
        {
        }

        public DTOpizzaType(int id, string pizzaType)
        {
            Id = id;
            PizzaType = pizzaType;
        }
    }

}
