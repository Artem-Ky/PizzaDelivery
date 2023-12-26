using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DataHistory
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public DateTime DateTime { get; set; }

        public List<string> DishNames { get; set; }

        public string dishStringReportData { get; set; }

        public DataHistory()
        {
            DishNames = new List<string>();
        }
    }
    public class SPResult
    {
        public string name { get; set; }
        public string adress { get; set; }
        public DateTime date_time { get; set; }
    }
}
