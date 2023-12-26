using BLL.Model;
using BLL.Services;
using DAL.Entities;
using DAL.Repository;
using Interfaces.Services;
using Ninject;
using Pizza.MVVM.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Pizza.MVVM.ViewModel.UserChild
{
    public class MenuData
    {
        private static MenuData _instance;

        public List<DTOmenu> CurrentMenu { get; private set; }
        public List<DTOpizzaType> Type { get; private set; }

        private MenuData()
        {
            IServicesPizza pizza = NinjectServicesSingleton.PizzaServices;
            CurrentMenu = new List<DTOmenu>(pizza.GetAllPizza().OrderBy(i => i.PizzaTypeId));
            Type = new List<DTOpizzaType>(pizza.GetAllType().OrderBy(i => i.Id).ToList());
            CurrentMenu = CurrentMenu
                .Join(Type, menu => menu.PizzaTypeId, type => type.Id, (menu, type) => new DTOmenu
                {
                    Id = menu.Id,
                    Name = menu.Name,
                    Cost = menu.Cost,
                    About = menu.About,
                    IsAvailable = menu.IsAvailable,
                    PizzaTypeId = menu.PizzaTypeId,
                    count = menu.count,
                    Photo = menu.Photo,
                    PizzaType = type.PizzaType
                })
                .Where(menu => menu.IsAvailable)
                .ToList();


        }

        public static MenuData GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MenuData();
            }
            return _instance;
        }

    }
}
