using BLL.Model;
using Interfaces.Services;
using Pizza.MVVM.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using System.Windows;


namespace Pizza.MVVM.ViewModel.ManagerChild
{
    public class OrderCRUDViewModel : ViewModelBase
    {
        private DTOorders_ updateOrderDto;
        IServicesOrders orderServices = NinjectServicesSingleton.OrderServices;
        private ObservableCollection<HistoryDataAll> _historyList;
        private ObservableCollection<DTOstatus> _statusList;
        private ObservableCollection<DTOcourier> _courierList;
        private const int PageSize = 100;     ////////////////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private int _currentPageNumber;
        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    SearchCommand.Execute(_searchText);
                }
            }
        }
        public int CurrentPageNumber
        {
            get { return _currentPageNumber; }
            set
            {
                _currentPageNumber = value;
                OnPropertyChanged(nameof(CurrentPageNumber));
                //UpdateAllButtonNav();
            }
        }
        public ObservableCollection<HistoryDataAll> historyList
        {
            get { return _historyList; }
            set
            {
                _historyList = value;
                OnPropertyChanged(nameof(historyList));
            }
        }
        public ObservableCollection<DTOstatus> statusList
        {
            get { return _statusList; }
            set
            {
                _statusList = value;
                OnPropertyChanged(nameof(statusList));
            }
        }
        public ObservableCollection<DTOcourier> courierList
        {
            get { return _courierList; }
            set
            {
                _courierList = value;
                OnPropertyChanged(nameof(courierList));
            }
        }

        public ICommand SearchCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public OrderCRUDViewModel() 
        {
            statusList = SingletonData.GetInstance().StatusList;
            courierList = SingletonData.GetInstance().CourierList;
            CurrentPageNumber = 1;
            historyList = orderServices.LoadAllUserOrders(CurrentPageNumber, PageSize);
            SearchCommand = new RelayComman(ExecuteSearchCommand);
            SaveCommand = new RelayComman(ExecuteSaveCommand);
            updateOrderDto = new DTOorders_();
            //ExecutePrintCommand(null);


        }

        private void ExecuteSearchCommand(object obj)
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                historyList = orderServices.LoadAllUserOrdersFilter(SearchText);
            }
            else
            {
                historyList = orderServices.LoadAllUserOrders(CurrentPageNumber, PageSize);
            }

        }
        private void ExecuteSaveCommand(object obj)
        {
            if (obj is HistoryDataAll order)
            {
                updateOrderDto.Id = order.Id;
                updateOrderDto.StatusId = order.status.Id;
                updateOrderDto.CourierId = order.courier.Id;
                if (order.status.Id == 2 && order.waitingTime == -9999)
                {
                    updateOrderDto.WaitingTime = (int)(DateTime.Now - order.DateTime).TotalMinutes;
                }
                else if (order.status.Id == 2 && order.waitingTime != -9999)
                {
                    updateOrderDto.WaitingTime = order.waitingTime;
                }
                else
                    updateOrderDto.WaitingTime = -9999;
                orderServices.EditStatusOrCourier(updateOrderDto);
                notifier.ShowSuccess("успешно обновлено");

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
        //private void ExecutePrintCommand(object obj)
        //{
        //    //if (obj is HistoryDataAll order)
        //    //{
        //        string outputPath = "/Orders.pdf";

        //        // Создание PDF-документа формата A6
        //        using (var writer = new PdfWriter(outputPath))
        //        {
        //            using (var pdf = new PdfDocument(writer))
        //            {
        //                var document = new Document(pdf);

        //                // Добавление информации в документ
        //                document.Add(new Paragraph("Привет, это содержимое моего PDF-документа!"));

        //                // Добавление другой информации...

        //                Console.WriteLine("PDF-документ создан успешно.");
        //            }
        //        }
        //    //}

        //}
    }
}
