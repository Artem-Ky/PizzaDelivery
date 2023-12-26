using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("customer")]
    public class customer : user
    {
        public string email { get; set; }
        public string phone { get; set; }
    }
}
