using BLL.Interfaces;
using BLL.Model;
using Pizza.MVVM.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using System.Windows;


namespace Pizza.MVVM.ViewModel.ManagerChild
{
    public class DeliteFormViewModel : ViewModelBase
    {
        public DTOmenu copyOfSelectedDish;
        public DTOingredients copyOfSelectedIngr;
        private bool dishOrIngr;
        private IServicesCRUD crudServices = NinjectServicesSingleton.CRUDServices;
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        private string _name2;
        public string Name2
        {
            get { return _name2; }
            set
            {
                if (_name2 != value)
                {
                    _name2 = value;
                    OnPropertyChanged(nameof(Name2));
                }
            }
        }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand DeliteObjectCommand { get; set; }
        public ICommand DeliteIngrCommand { get; set; }
        public DeliteFormViewModel() 
        {
            DeliteObjectCommand = new RelayComman(ExecuteDeliteCommand);
            DeliteIngrCommand = new RelayComman(ExecuteDeliteIngrCommand);
            SaveChangesCommand = new RelayComman(edit);

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
        private void edit(object obj)
        {
            if (dishOrIngr == true)
            {
                if (copyOfSelectedDish.Name == Name)
                {
                    crudServices.Delite(copyOfSelectedDish);
                    notifier.ShowSuccess("успешно удалено");
                }
                else
                    notifier.ShowError("Неправильное имя");
            }
            else if (dishOrIngr == false)
            {
                if (copyOfSelectedIngr.Name == Name2)
                {
                    crudServices.DeliteIngr(copyOfSelectedIngr);
                    notifier.ShowSuccess("успешно удалено");
                }
                else
                    notifier.ShowError("Неправильное имя");
            }

        }

        private void ExecuteDeliteCommand(object parameter)
        {
            if (parameter is DTOmenu selectedDish)
            {
                copyOfSelectedDish = new DTOmenu
                {
                    Id = selectedDish.Id,
                    Name = selectedDish.Name,

                };
                Name2 = selectedDish.Name;
                dishOrIngr = true;

            }
        }
        private void ExecuteDeliteIngrCommand(object parameter)
        {
            if (parameter is DTOingredients selectedDish)
            {
                copyOfSelectedIngr = new DTOingredients
                {
                    Id = selectedDish.Id,
                    Name = selectedDish.Name,

                };
                Name2 = selectedDish.Name;
                dishOrIngr = false;

            }
        }
    }
}
