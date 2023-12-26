using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public partial class ingredients
    {
        public int id { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public int weightOneCount { get; set; }
        public int costForOneCount { get; set; }
        public bool isAvalaible { get; set; }
        public string photoIngr { get; set; }
        public int ingredientType_id { get; set; }
        public virtual ingredientType ingredientType { get; set; }
    }
}
