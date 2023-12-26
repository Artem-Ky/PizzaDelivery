using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public partial class orders_
    {
        public int id { get; set; }
        public int waiting_time { get; set; }
        public DateTime date_time { get; set; }
        public int cost { get; set; }
        public string adress { get; set; }
        public int status_id { get; set; }
        public int courier_id { get; set; }
        public int user_id { get; set; }
        public virtual user user { get; set; }
        public virtual status status { get; set; }
        public virtual courier courier { get; set; }

    }
}
