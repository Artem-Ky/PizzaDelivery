using BLL.interfaces;
using BLL.Model;
using Interfaces.Services;
using Ninject;
using Pizza.MVVM.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.MVVM.ViewModel.UserChild
{
    public class CustomData
    {
        private static CustomData _instance;

        public DTOconstr constPizza { get; private set; }

        private CustomData()
        {
            IServicesOrders pizza = NinjectServicesSingleton.OrderServices;
            constPizza = pizza.GetCustomBox();
            constPizza.count = 1;
            constPizza.Weight = 150;
            constPizza.Cost = 100;


        }

        public static CustomData GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CustomData();
            }
            return _instance;
        }

    }
}
