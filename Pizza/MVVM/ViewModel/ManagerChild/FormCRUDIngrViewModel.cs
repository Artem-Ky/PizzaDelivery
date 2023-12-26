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
    public class FormCRUDIngrViewModel : ViewModelBase
    {
        private bool createOrEdit;
        private List<DTOingredients> _currentIngr;
        private DTOingredients copyOfSelectedDish;
        private IServicesCRUD crudServices = NinjectServicesSingleton.CRUDServices;

        public ICommand GetObjectCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand CreateObjectCommand { get; set; }
        public List<DTOingredients> currentIngr
        {
            get { return _currentIngr; }
            set
            {
                if (_currentIngr != value)
                {
                    _currentIngr = value;
                    OnPropertyChanged(nameof(currentIngr));
                }
            }
        }

        public FormCRUDIngrViewModel()
        {
            currentIngr = new List<DTOingredients>();
            GetObjectCommand = new RelayComman(ExecuteAddCommand);
            CreateObjectCommand = new RelayComman(ExecuteCreateCommand);
            SaveChangesCommand = new RelayComman(edit);

        }

        private void edit(object obj)
        {
            if (createOrEdit == true)
            {
                crudServices.EditIngrCard(copyOfSelectedDish);
                notifier.ShowSuccess("успешно обновлено");
            }
            else if (createOrEdit == false)
            {
                crudServices.CreateIngrCard(copyOfSelectedDish);
                notifier.ShowSuccess("успешно создано");
            }
        }

        private void ExecuteAddCommand(object parameter)
        {
            if (parameter is DTOingredients selectedDish)
            {
                createOrEdit = true;
                currentIngr.Clear();
                copyOfSelectedDish = new DTOingredients
                {
                    Id = selectedDish.Id,
                    CostForOneCount = selectedDish.CostForOneCount,
                    countT = selectedDish.Count,
                    IngredientType_id = selectedDish.IngredientType_id,
                    IsAvalaible = selectedDish.IsAvalaible,
                    Name = selectedDish.Name,
                    PhotoIngr = selectedDish.PhotoIngr,
                    WeightOneCount = selectedDish.WeightOneCount

                };
                currentIngr.Add(copyOfSelectedDish);

            }
        }
        private void ExecuteCreateCommand(object parameter)
        {
            createOrEdit = false;
            currentIngr.Clear();
            copyOfSelectedDish = new DTOingredients
            {
                CostForOneCount = -999,
                countT = -999,
                IngredientType_id =-1,
                Name = "название",
                PhotoIngr = "/Images/mainform/ingr/about.jpg",
                WeightOneCount = -999



            };
            currentIngr.Add(copyOfSelectedDish);

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
