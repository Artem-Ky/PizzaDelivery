using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL.Entities
{
    [Table("pizza")]
    public partial class pizza
    {
        public int id { get; set; }
        public string name { get; set; }
        public int cost { get; set; }
        public string about { get; set; }
        public int pizza_type_id { get; set; }
        public int weight { get; set; }
        public virtual pizzaType pizzaType { get; set; }
    }
}
