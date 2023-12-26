using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class DTOemployee : DTOuser
    {
        public DTOemployee()
        {
        }

        public DTOemployee(int id, string name, string password)
             : base(id, name, password)
        {
        }
    }

}
