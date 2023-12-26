using BLL.Model;
using Interfaces.Services;
using Ninject;
using Pizza.MVVM.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.MVVM.ViewModel.UserChild
{
    public class CardData
    {
        private static CardData _instance;

        public ObservableCollection<DTOmenu> menuCardList;
        public ObservableCollection<DTOconstr> customCardList;

        private CardData()
        {
            menuCardList = new ObservableCollection<DTOmenu>();
            customCardList = new ObservableCollection<DTOconstr>();

        }

        public static CardData GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CardData();
            }
            return _instance;
        }
        public void AddMenuItem(DTOmenu menuItem)
        {
            menuCardList.Add(menuItem);
        }

        public void AddCustomItem(DTOconstr customItem)
        {
            customCardList.Add(customItem);
        }

    }
}
