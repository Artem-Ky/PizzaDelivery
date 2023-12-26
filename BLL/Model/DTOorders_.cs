
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class DTOorders_
    {
        public int Id { get; set; }
        public int WaitingTime { get; set; }
        public DateTime DateTime { get; set; }
        public int StatusId { get; set; }
        public int CourierId { get; set; }
        public int UserId { get; set; }
        public int Cost { get; set; }
        public string Address { get; set; }
        public ObservableCollection<DTOmenu> OrderPizzasMenu { get; set; }
        public ObservableCollection<DTOconstr> OrderPizzasCustom { get; set; }
        public DTOstatus Status { get; set; }
        public DTOcourier Courier { get; set; }
        public DTOuser User { get; set; }

        public DTOorders_()
        {
        }

        public DTOorders_(int id, int waitingTime, DateTime dateTime, int statusId, int courierId, int userId, int cost, string address, ObservableCollection<DTOmenu> orderPizzasMenu, ObservableCollection<DTOconstr> orderPizzasCustom, DTOstatus status, DTOcourier courier, DTOuser user)
        {
            Id = id;
            WaitingTime = waitingTime;
            DateTime = dateTime;
            StatusId = statusId;
            CourierId = courierId;
            UserId = userId;
            Cost = cost;
            Address = address;
            OrderPizzasMenu = orderPizzasMenu;
            OrderPizzasCustom = orderPizzasCustom;
            Status = status;
            Courier = courier;
            User = user;
        }
    }


}
