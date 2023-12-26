using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class DTOingredients_pizza
    {
        public int Id { get; set; }
        public int IngredientsId { get; set; }
        public int PizzaId { get; set; }
        public DTOingredients Ingredients { get; set; }
        public DTOpizza Pizza { get; set; }

        public DTOingredients_pizza()
        {
        }

        public DTOingredients_pizza(int id, int ingredientsId, int pizzaId)
        {
            Id = id;
            IngredientsId = ingredientsId;
            PizzaId = pizzaId;
        }
    }

}
