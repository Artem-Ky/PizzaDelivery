using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class HistoryData
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string status { get; set; }
        public int status_id { get; set; }
        public int courier_id { get; set; }
        public int orderCost { get; set; }
        public List<DTOmenu> Menu { get; set; }
        public List<DTOconstr> Custom {  get; set; }




    }
    public class HistoryDataAll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public DTOstatus status { get; set; }
        public int waitingTime { get; set; }
        public int orderCost { get; set; }
        public string adres {  get; set; }
        public DTOcourier courier { get; set; }
        public List<DTOmenu> Menu { get; set; }
        public List<DTOconstr> Custom { get; set; }




    }
}
