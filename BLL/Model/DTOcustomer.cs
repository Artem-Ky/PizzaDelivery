using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class DTOcustomer : DTOuser
    {
        public string Phone { get; set; }
        public string Email { get; set; }

        public DTOcustomer()
        {
        }

        public DTOcustomer(int id, string name, string password, string phone, string email)
             : base(id, name, password)
        {
            Phone = phone;
            Email = email;
        }
    }
}
