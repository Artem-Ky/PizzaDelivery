

namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class user
    {
        public int id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public bool isManager { get; set; }

        // Навигационные свойства для наследования
        public virtual employee Employee { get; set; }
        public virtual customer Customer { get; set; }
    }
}
