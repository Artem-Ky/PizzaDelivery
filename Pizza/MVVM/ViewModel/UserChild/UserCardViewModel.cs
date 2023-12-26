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
using Pizza.MVVM.ViewModel.UserMain;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using Pizza.MVVM.View.UserMain;
using Interfaces.Services;
using Pizza.MVVM.Util;

namespace Pizza.MVVM.ViewModel.UserChild
{
    public class UserCardViewModel : ViewModelBase
    {
        private ObservableCollection<DTOmenu> _menu;
        private ObservableCollection<DTOconstr> _custom;
        private ObservableCollection<object> _allItems;
        private DTOconstr _constrItem;
        private int _totalCost;
        private string _streetText;
        private string _houseNumberText;
        private string _entranceNumberText;
        private string _flatNumberText;

        public string StreetText
        {
            get { return _streetText; }
            set
            {
                if (_streetText != value)
                {
                    _streetText = value;
                    OnPropertyChanged(nameof(StreetText));
                }
            }
        }

        public string HouseNumberText
        {
            get { return _houseNumberText; }
            set
            {
                if (_houseNumberText != value)
                {
                    _houseNumberText = value;
                    OnPropertyChanged(nameof(HouseNumberText));
                }
            }
        }

        public string EntranceNumberText
        {
            get { return _entranceNumberText; }
            set
            {
                if (_entranceNumberText != value)
                {
                    _entranceNumberText = value;
                    OnPropertyChanged(nameof(EntranceNumberText));
                }
            }
        }

        public string FlatNumberText
        {
            get { return _flatNumberText; }
            set
            {
                if (_flatNumberText != value)
                {
                    _flatNumberText = value;
                    OnPropertyChanged(nameof(FlatNumberText));
                }
            }
        }

        public int Totalcost 
        {
            get { return _totalCost; }
            set
            {
                _totalCost = value;
                OnPropertyChanged(nameof(Totalcost));
            }
        }

        public ObservableCollection<object> AllItems
        {
            get { return _allItems; }
            set
            {
                    _allItems = value;
                    OnPropertyChanged(nameof(AllItems));
            }
        }
        public ObservableCollection<object> GetAllItems()
        {
            var allItems = new ObservableCollection<object>();

            if (_menu != null)
            {
                foreach (var item in _menu)
                {
                    allItems.Add(item);
                    Totalcost += item.allPizzaCost;
                }
            }

            if (_custom != null)
            {
                foreach (var item in _custom)
                {
                    allItems.Add(item);
                    Totalcost += item.allPizzaCost;
                }
            }

            return allItems;
        }
        public ObservableCollection<DTOmenu> menu
        {
            get { return _menu; }
            set
            {
                _menu = value;
                OnPropertyChanged(nameof(menu));
            }
        }

        public ObservableCollection<DTOconstr> custom
        {
            get { return _custom; }
            set
            {
                _custom = value;
                OnPropertyChanged(nameof(custom));
            }
        }

        public ICommand IncreaseCommand { get; private set; }
        public ICommand DecreaseCommand { get; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand ConstrCommand { get; private set; }
        public ICommand PayCommand { get; private set; }

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
                        Totalcost += pizza.Cost;
                        OnPropertyChanged(nameof(pizza.count));
                        OnPropertyChanged(nameof(pizza.allPizzaCost));
                        OnPropertyChanged(nameof(Totalcost));
                    }
                });
            }
            else if (obj is DTOconstr pizzaCustom)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (pizzaCustom.count < 99)
                    {

                        pizzaCustom.count++;
                        pizzaCustom.allPizzaCost += pizzaCustom.Cost;
                        Totalcost += pizzaCustom.Cost;
                        OnPropertyChanged(nameof(pizzaCustom.count));
                        OnPropertyChanged(nameof(pizzaCustom.allPizzaCost));
                        OnPropertyChanged(nameof(Totalcost));
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
                        Totalcost -= pizza.Cost;
                        OnPropertyChanged(nameof(pizza.count));
                        OnPropertyChanged(nameof(pizza.allPizzaCost));
                        OnPropertyChanged(nameof(Totalcost));
                        if (pizza.count == 0)
                        {
                            menu.Remove(pizza);
                            AllItems.Remove(pizza);
                        }
                        OnPropertyChanged(nameof(menu));
                        OnPropertyChanged(nameof(AllItems));
                    }
                });
            }
            else if (obj is DTOconstr pizzaCustom)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (pizzaCustom.count > 0)
                    {
                        pizzaCustom.count--;
                        pizzaCustom.allPizzaCost -= pizzaCustom.Cost;
                        Totalcost -= pizzaCustom.Cost;
                        OnPropertyChanged(nameof(pizzaCustom.count));
                        OnPropertyChanged(nameof(pizzaCustom.allPizzaCost));
                        OnPropertyChanged(nameof(Totalcost));
                        if (pizzaCustom.count == 0)
                        {
                            custom.Remove(pizzaCustom);
                            AllItems.Remove(pizzaCustom);
                        }
                        OnPropertyChanged(nameof(custom));
                        OnPropertyChanged(nameof(AllItems));
                    }
                });
            }
        }
        private void Delete(object obj)
        {
            if (obj is DTOmenu pizza)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Totalcost -= pizza.allPizzaCost;
                    pizza.allPizzaCost = 0;
                    pizza.count = 0; 
                    OnPropertyChanged(nameof(pizza.count));
                    OnPropertyChanged(nameof(pizza.allPizzaCost));
                    OnPropertyChanged(nameof(Totalcost));
                    menu.Remove(pizza);
                    AllItems.Remove(pizza);
                    OnPropertyChanged(nameof(menu));
                    OnPropertyChanged(nameof(AllItems));
                    
                });
            }
            else if (obj is DTOconstr pizzaCustom)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Totalcost -= pizzaCustom.allPizzaCost;
                    custom.Remove(pizzaCustom);
                    AllItems.Remove(pizzaCustom);
                    OnPropertyChanged(nameof(custom));
                    OnPropertyChanged(nameof(AllItems));
                    OnPropertyChanged(nameof(Totalcost));

                });
            }
        }

        private void Constr(object obj)
        {
            if (obj is DTOconstr pizza)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _constrItem.Cost = pizza.Cost;
                    _constrItem.About = pizza.About;
                    _constrItem.Id = pizza.Id;
                    _constrItem.IngrList = pizza.IngrList;
                    _constrItem.allPizzaCost = pizza.Cost * pizza.count;
                    _constrItem.count = pizza.count;
                    _constrItem.Weight = pizza.Weight;
                    _constrItem.PizzaType = pizza.PizzaType;
                    _constrItem.Name = pizza.Name;
                    _constrItem.PizzaTypeId = pizza.PizzaTypeId;
                    Delete(pizza);
                    MainViewModel.Instance.ShowConstrMenu(null);


                });
            }
        }

        private void Pay(object obj)
        {
            if (StreetText != null && HouseNumberText != null && StreetText != "" && HouseNumberText != "")
            {
                List<string> addressParts = new List<string>
                {
                };
                addressParts.Add($"Улица  {StreetText}");
                addressParts.Add($"Дом {HouseNumberText}");
                if (!string.IsNullOrEmpty(EntranceNumberText))
                {
                    addressParts.Add($"Подъезд {EntranceNumberText}");
                }
                if (!string.IsNullOrEmpty(FlatNumberText))
                {
                    addressParts.Add($"Квартира {FlatNumberText}");
                }
                string fullAddress = string.Join(", ", addressParts);

                int _userId = MainViewModel.Instance.GetUserId();

                IServicesOrders orderServices = NinjectServicesSingleton.OrderServices;

                DTOorders_ neworder = new DTOorders_
                {
                    WaitingTime = -9999,
                    DateTime = DateTime.Now,
                    StatusId = 4,
                    UserId = _userId,
                    Cost = Totalcost,
                    Address = fullAddress,
                    CourierId = 0,
                    OrderPizzasMenu = menu,
                    OrderPizzasCustom = custom
                   

                };

                

                orderServices.MakeOrder(neworder);

                var allItemsCopy = new List<object>(AllItems);
                foreach (var item in allItemsCopy)
                {
                    Delete(item);
                }

                notifier.ShowSuccess("Ваш заказ создан и скоро будет у вас!");
            }
            else
            {
                notifier.ShowError("Заполнены не все обязательные поля");
            }
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive),
            corner: Corner.TopRight,
            offsetX: 10,
            offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
        public UserCardViewModel()
        {
            menu = CardData.GetInstance().menuCardList;
            custom = CardData.GetInstance().customCardList;
            _constrItem = CustomData.GetInstance().constPizza;
            AllItems = GetAllItems();

            IncreaseCommand = new RelayComman(IncreaseNumber);
            DecreaseCommand = new RelayComman(DecreaseNumber);
            DeleteCommand = new RelayComman(Delete);
            ConstrCommand = new RelayComman(Constr);
            PayCommand = new RelayComman(Pay);


        }

    }
}
