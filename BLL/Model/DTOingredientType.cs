using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class DTOingredientType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DTOingredientType()
        {
        }

        public DTOingredientType(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
