using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class DTOcourier
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DTOcourier()
        {
        }

        public DTOcourier(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

}
