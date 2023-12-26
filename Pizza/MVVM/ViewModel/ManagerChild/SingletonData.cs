using BLL.Model;
using Interfaces.Services;
using Pizza.MVVM.Util;
using Pizza.MVVM.ViewModel.UserChild;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.MVVM.ViewModel.ManagerChild
{
    public class SingletonData
    {
        IServicesOrders orderServices = NinjectServicesSingleton.OrderServices;
        private static SingletonData _instance;

        public ObservableCollection<DTOstatus> StatusList;
        public ObservableCollection<DTOcourier> CourierList;

        private SingletonData()
        {
            StatusList = new ObservableCollection<DTOstatus>();
            CourierList = new ObservableCollection<DTOcourier>();

        }

        public static SingletonData GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SingletonData();
                _instance.FillStatusList();
                _instance.FillCourierList();

            }
            return _instance;
        }
        public void FillStatusList()
        {
            StatusList = orderServices.GetAllStatus();
        }

        public void FillCourierList()
        {
            CourierList = orderServices.GetAllCouriers();
        }
    }
}
