using BLL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IServicesOrders
    {
        List<DTOorders_> GetAllOrders();
        ObservableCollection<DTOstatus> GetAllStatus();
        ObservableCollection<DTOcourier> GetAllCouriers();

        DTOconstr GetCustomBox();
        bool MakeOrder(DTOorders_ orderdto);

        int GetTotalPage(int id);

        ObservableCollection<HistoryData> LoadUserOrders(int customerId, int page, int pageSize);

        ObservableCollection<HistoryDataAll> LoadAllUserOrders(int page, int pageSize);

        ObservableCollection<HistoryDataAll> LoadAllUserOrdersFilter(string text);

        void EditStatusOrCourier(DTOorders_ orderdto);

        void EditMenuAvaible(bool status, int id);
    }
}
