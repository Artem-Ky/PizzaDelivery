using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("menu")]
    public partial class menu : pizza
    {
        public string Photo { get; set; }
        public bool isAvalaible { get; set; }
    }
}
