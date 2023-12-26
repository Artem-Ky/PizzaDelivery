using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("constr")]
    public partial class constr : pizza
    {
        public string hashPizza { get; set; }
    }
}
