using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public partial class orders_pizza
    {
        public int id { get; set; }
        public int orders_id { get; set; }
        public int pizza_id { get; set; }
        public int cost { get; set; }
        public int count { get; set; }
        public virtual orders_ orders { get; set; }
        public virtual pizza pizza { get; set; }
    }
}
