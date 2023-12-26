using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class DTOorders_pizza
    {
        public int Id { get; set; }
        public int OrdersId { get; set; }
        public int PizzaId { get; set; }
        public DTOorders_ Orders { get; set; }
        public DTOpizza Pizza { get; set; }

        public DTOorders_pizza()
        {
        }

        public DTOorders_pizza(int id, int ordersId, int pizzaId)
        {
            Id = id;
            OrdersId = ordersId;
            PizzaId = pizzaId;
        }
    }


}
