using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class DTOuser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public DTOuser()
        {
        }

        public DTOuser(int id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }
    }

}
