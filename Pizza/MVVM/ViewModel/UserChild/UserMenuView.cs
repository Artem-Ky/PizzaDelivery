using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using DAL.Interfaces;
using DAL.Repository;
using FontAwesome.Sharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Data.Entity;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;
using System.Security.Principal;
using System.ComponentModel;
using BLL.Model;
using Ninject.Parameters;

namespace Pizza.MVVM.ViewModel.UserChild
{
    public class UserMenuView : ViewModelBase
    {
        private ObservableCollection<DTOmenu> _cardList;
        private List<DTOmenu> _currentMenu;


        public List<DTOmenu> currentMenu
        {
            get { return _currentMenu; }
            set
            {
                if (_currentMenu != value)
                {
                    _currentMenu = value;
                    OnPropertyChanged(nameof(currentMenu));
                }
            }
        }
        public ICommand IncreaseCommand { get; private set; }
        public ICommand DecreaseCommand { get; }
        public ICommand ToggleGridVisibilityCommand { get; private set; }
        public ICommand SelectPizzaTypeCommand { get; private set; }
        private ICommand _loadAllMenuCommand;

        public ICommand LoadAllMenuCommand
        {
            get
            {
                if (_loadAllMenuCommand == null)
                {
                    _loadAllMenuCommand = new RelayComman(param => LoadAllMenu());
                }
                return _loadAllMenuCommand;
            }
        }



        private string _menuText = "Все меню";

        public string MenuText
        {
            get { return _menuText; }
            set
            {
                if (_menuText != value)
                {
                    _menuText = value;
                    OnPropertyChanged(nameof(MenuText));
                }
            }
        }

        private void LoadAllMenu()
        {
            currentMenu = MenuData.GetInstance().CurrentMenu;
            OnPropertyChanged(nameof(currentMenu));

            // Показать блок текста для всех меню
            MenuText = "Все меню";
        }

        private void ToggleGridVisibility(object obj)
        {
            if (obj is FrameworkElement element)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                  
                    if (element.Visibility == Visibility.Visible)
                    {
                        element.Visibility = Visibility.Hidden;
                        element.Height = 0;
                        element.Width = 0;
                    }
                    else
                    {
                        element.Visibility = Visibility.Visible;
                        element.Height = pizzaTypeList.Count *25 + 40;
                        element.Width = 200;
                }
                });
            }
        }
        private void IncreaseNumber(object obj)
        {
            if (obj is DTOmenu pizza)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (pizza.count < 99)
                    {

                        pizza.count++;
                        pizza.allPizzaCost += pizza.Cost;
                        OnPropertyChanged(nameof(pizza.count));
                        if (pizza.count == 1)
                            _cardList.Add(pizza);
                    }
                });
            }
        }


        private void DecreaseNumber(object obj)
        {
            if (obj is DTOmenu pizza)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (pizza.count > 0)
                    {
                        pizza.count--;
                        pizza.allPizzaCost -= pizza.Cost;
                        OnPropertyChanged(nameof(pizza.count));
                        if (pizza.count == 0)
                            _cardList.Remove(pizza);
                    }
                });
            }
        }


        ////////////



        private List<DTOpizzaType> _pizzaTypeList;

        public List<DTOpizzaType> pizzaTypeList
        {
            get { return _pizzaTypeList?.Skip(1).ToList(); }
            set
            {
                if (_pizzaTypeList != value)
                {
                    _pizzaTypeList = value;
                    OnPropertyChanged(nameof(pizzaTypeList));
                }
            }
        }
        private DTOpizzaType _selectedPizzaType;
        public DTOpizzaType SelectedPizzaType
        {
            get { return _selectedPizzaType; }
            set
            {
                if (_selectedPizzaType != value)
                {
                    _selectedPizzaType = value;
                    OnPropertyChanged(nameof(SelectedPizzaType));
                }
            }
        }


        private void SelectPizzaType(object parameter)
        {
            if (parameter is DTOpizzaType selectedPizzaType)
            {
                SelectedPizzaType = selectedPizzaType;
                currentMenu = MenuData.GetInstance().CurrentMenu.Where(menu => menu.PizzaType == SelectedPizzaType.PizzaType).ToList();
                OnPropertyChanged(nameof(currentMenu));

                MenuText = selectedPizzaType.PizzaType;
            }
        }

        public UserMenuView()
        {
            currentMenu = MenuData.GetInstance().CurrentMenu;
            pizzaTypeList = MenuData.GetInstance().Type;
            _cardList = CardData.GetInstance().menuCardList;
            IncreaseCommand = new RelayComman(IncreaseNumber);
            DecreaseCommand = new RelayComman(DecreaseNumber);
            ToggleGridVisibilityCommand = new RelayComman(ToggleGridVisibility);
            SelectPizzaTypeCommand = new RelayComman(SelectPizzaType);
        }





    }
}
