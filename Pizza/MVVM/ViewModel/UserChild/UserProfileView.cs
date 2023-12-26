using BLL.Model;
using Interfaces.Services;
using Pizza.MVVM.Util;
using Pizza.MVVM.View.UserChild;
using Pizza.MVVM.ViewModel.UserMain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using Serilog;
using DAL.Entities;
using Serilog.Events;


namespace Pizza.MVVM.ViewModel.UserChild
{
    public class UserProfileView : ViewModelBase
    {
        int _userId = MainViewModel.Instance.GetUserId();
        IServicesOrders orderServices = NinjectServicesSingleton.OrderServices;
        private ObservableCollection<HistoryData> _historyList;
        private const int PageSize = 4;     ////////////////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private int _currentPageNumber;
        private int _totalRow;
        private NumberInputViewModel viewModel = new NumberInputViewModel();
        private NumberInputWindow inputDialog = new NumberInputWindow();
        private ObservableCollection<DTOmenu> _menu;
        private ObservableCollection<DTOconstr> _custom;
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
        public int CurrentPageNumber
        {
            get { return _currentPageNumber; }
            set
            {
                _currentPageNumber = value;
                OnPropertyChanged(nameof(CurrentPageNumber));
                UpdateAllButtonNav();
            }
        }
        private int _nextPageNumber;
        public int NextPageNumber
        {
            get { return _nextPageNumber; }
            set
            {
                _nextPageNumber = value;
                OnPropertyChanged(nameof(NextPageNumber));
            }
        }
        private int _next2PageNumber;
        public int Next2PageNumber
        {
            get { return _next2PageNumber; }
            set
            {
                _next2PageNumber = value;
                OnPropertyChanged(nameof(Next2PageNumber));
            }
        }
        private int _previousPageNumber;
        public int PreviousPageNumber
        {
            get { return _previousPageNumber; }
            set
            {
                _previousPageNumber = value;
                OnPropertyChanged(nameof(PreviousPageNumber));
            }
        }
        private int _previous2PageNumber;
        public int Previous2PageNumber
        {
            get { return _previous2PageNumber; }
            set
            {
                _previous2PageNumber = value;
                OnPropertyChanged(nameof(Previous2PageNumber));
            }
        }
        private int _totalPageNumber;
        public int TotalPageNumber
        {
            get { return _totalPageNumber; }
            set
            {
                _totalPageNumber = value;
                OnPropertyChanged(nameof(TotalPageNumber));
            }
        }
        public ObservableCollection<HistoryData> historyList
        {
            get { return _historyList; }
            set
            {
                _historyList = value;
                OnPropertyChanged(nameof(historyList));
            }
        }


        public ICommand NextPageCommand { get; private set; }
        public ICommand PreviousPageCommand { get; private set; }
        public ICommand Next2PageCommand { get; private set; }
        public ICommand Previous2PageCommand { get; private set; }
        public ICommand StartPageCommand { get; private set; }
        public ICommand LastPageCommand { get; private set; }
        public ICommand VariablePageCommand { get; private set; }
        public ICommand AddCommand { get; private set; }

        private void NextPage(object obj)
        {
            if(CurrentPageNumber < TotalPageNumber)
            {
                CurrentPageNumber += 1;
                UpdateCurrentPageList();
            }
        }
        private void Next2Page(object obj)
        {
            if (CurrentPageNumber +1 < TotalPageNumber)
            {
                CurrentPageNumber += 2;
                UpdateCurrentPageList();
            }
        }

        private void PreviousPage(object obj)
        {
            if (CurrentPageNumber > 1)
            {
                CurrentPageNumber--;
                UpdateCurrentPageList();
            }
        }
        private void Previous2Page(object obj)
        {
            if (CurrentPageNumber -1 > 1)
            {
                CurrentPageNumber -= 2;
                UpdateCurrentPageList();
            }
        }
        private void startPage(object obj)
        {
                CurrentPageNumber = 1;
                UpdateCurrentPageList();
        }
        private void lastPage(object obj)
        {
                CurrentPageNumber = TotalPageNumber;
                UpdateCurrentPageList();
        }

        private void variablePage(object obj)
        {
            NumberInputViewModel viewModel = new NumberInputViewModel();


            NumberInputWindow inputDialog = new NumberInputWindow();


            inputDialog.DataContext = viewModel;

            if (inputDialog != Application.Current.MainWindow)
            {
                //  Owner только если окно Application.Current.MainWindow уже показано
                if (Application.Current.MainWindow != null && Application.Current.MainWindow.IsVisible)
                {
                    inputDialog.Owner = Application.Current.MainWindow;
                }

                inputDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                var result = inputDialog.ShowDialog();
                if (result == true)
                {

                    int? userInput = viewModel.EnteredNumber;

                    if (userInput.HasValue && userInput <= TotalPageNumber && userInput >= 1)
                    {

                        CurrentPageNumber = userInput.Value;
                        UpdateCurrentPageList();
                    }
                    else
                    {
                        notifier.ShowError("Такой страницы не существует");
                    }
                }

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



        private void TotalPageCount()
        {
            TotalPageNumber = _totalRow / PageSize;
            if (_totalRow % PageSize != 0)
                TotalPageNumber += 1;
        }
        private void UpdateAllButtonNav()
        {
            NextPageNumber = CurrentPageNumber + 1;
            Next2PageNumber = CurrentPageNumber +2;
            PreviousPageNumber = CurrentPageNumber - 1;
            Previous2PageNumber = CurrentPageNumber - 2;
        }
        private void UpdateCurrentPageList()
        {
            historyList = orderServices.LoadUserOrders(_userId, CurrentPageNumber, PageSize);
        }
        private void order(object obj)
        {
            try
            {
                Log.Information("Processing order...");

                if (obj is HistoryData order)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        try
                        {
                            foreach (var menuItem in order.Menu)
                            {
                                Log.Information("Processing menu item...");
                                var matchingMenu = MenuData.GetInstance().CurrentMenu.FirstOrDefault(menu => menu.Id == menuItem.Id);
                                if (matchingMenu != null && matchingMenu.count == 0)
                                {
                                    Log.Information("Adding menu item to menuCardList...");
                                    CardData.GetInstance().menuCardList.Add(matchingMenu);
                                }

                                matchingMenu.count += menuItem.count;
                                matchingMenu.allPizzaCost += menuItem.allPizzaCost;
                            }

                            foreach (var customItem in order.Custom.ToList())
                            {
                                Log.Information("Processing custom item...");
                                var customPizza = CardData.GetInstance().customCardList.FirstOrDefault(custom => custom.HashPizza == customItem.HashPizza);
                                if (customPizza != null)
                                {
                                    customPizza.count += customItem.count;
                                    customPizza.allPizzaCost += customItem.allPizzaCost;
                                }
                                else
                                {
                                    Log.Information("Adding custom item to customCardList...");
                                    CardData.GetInstance().customCardList.Add(customItem);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, "An error occurred inside Dispatcher.Invoke.");
                            throw;
                        }
                    });

                    Log.Information("Order processed successfully!");
                    notifier.ShowSuccess("Заказ добавлен в корзину!");
                }
            }
            catch (Exception ex)
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();

                Log.Error(ex, "An error occurred while processing the order!");
                notifier.ShowError("Произошла ошибка при обработке заказа!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public UserProfileView()
        {

            _totalRow = orderServices.GetTotalPage(_userId);
            TotalPageCount();
            historyList = orderServices.LoadUserOrders(_userId , CurrentPageNumber, PageSize);
            NextPageCommand = new RelayComman(NextPage);
            PreviousPageCommand = new RelayComman(PreviousPage);
            Next2PageCommand = new RelayComman(Next2Page);
            Previous2PageCommand = new RelayComman(Previous2Page);
            StartPageCommand = new RelayComman(startPage);
            LastPageCommand = new RelayComman(lastPage);
            VariablePageCommand = new RelayComman(variablePage);
            AddCommand = new RelayComman(order);
            CurrentPageNumber = 1;
            menu = CardData.GetInstance().menuCardList;
            custom = CardData.GetInstance().customCardList;
        }

    }
}
