using BLL.Interfaces;
using BLL.Model;
using BLL.Services;
using Pizza.MVVM.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class FormCRUDMenuViewModel : ViewModelBase
    {
        private bool createOrEdit;
        private List<DTOmenu> _currentMenu;
        private DTOmenu copyOfSelectedDish;
        private IServicesCRUD crudServices = NinjectServicesSingleton.CRUDServices;

        public ICommand GetObjectCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand CreateObjectCommand { get; set; }
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

        public FormCRUDMenuViewModel()
        {
            currentMenu = new List<DTOmenu>();
            GetObjectCommand = new RelayComman(ExecuteAddCommand);
            CreateObjectCommand = new RelayComman(ExecuteCreateCommand);
            SaveChangesCommand = new RelayComman(edit);

        }

        private void edit(object obj)
        {
            if (createOrEdit == true)
            {
                crudServices.EditMenuCard(copyOfSelectedDish);
                notifier.ShowSuccess("успешно обновлено");
            }
            else if (createOrEdit == false)
            {
                crudServices.CreateMenuCard(copyOfSelectedDish);
                notifier.ShowSuccess("успешно создано");
            }
        }

        private void ExecuteAddCommand(object parameter)
        {
            if (parameter is DTOmenu selectedDish)
            {
                createOrEdit = true;
                currentMenu.Clear();
                copyOfSelectedDish = new DTOmenu
                {
                    Id = selectedDish.Id,
                    About = selectedDish.About,
                    Cost = selectedDish.Cost,
                    Name = selectedDish.Name,
                    Photo = selectedDish.Photo,
                    Weight = selectedDish.Weight,
                    PizzaTypeId = selectedDish.PizzaTypeId
                    
                };
                currentMenu.Add(copyOfSelectedDish);

            }
        }
        private void ExecuteCreateCommand(object parameter)
        {
            createOrEdit = false;
            currentMenu.Clear();
            copyOfSelectedDish = new DTOmenu
            {
                About = "описание пиццы",
                Cost = -9999,
                Name = "название пиццы",
                Photo = "/Images/mainform/menu/about.jpg",
                Weight = -9999,
                PizzaTypeId = -1,


            };
            currentMenu.Add(copyOfSelectedDish);

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
    }

}
