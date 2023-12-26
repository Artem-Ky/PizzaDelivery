using BLL.Model;
using Interfaces.Services;
using Ninject.Parameters;
using Pizza.MVVM.Util;
using Pizza.MVVM.ViewModel.UserChild;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using BLL.Interfaces;
using Pizza.MVVM.View.ManagerChild;
using Pizza.MVVM.View.UserChild;
using Serilog;
namespace Pizza.MVVM.ViewModel.ManagerChild
{
    public class menuCRUDViewModel : ViewModelBase
    {
        IServicesOrders orderServices = NinjectServicesSingleton.OrderServices;
        IServicesCRUD crudServieces = NinjectServicesSingleton.CRUDServices;
        private List<DTOmenu> _menuList;
        private DTOmenu _selectedItem;
        public DTOmenu SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }
        public List<DTOmenu> menuList
        {
            get { return _menuList; }
            set
            {
                _menuList = value;
                OnPropertyChanged(nameof(menuList));
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeliteCommand { get; set; }
        public ICommand CreateCommand { get; set; }
        private void ExecuteAddCommand(object parameter)
        {
            if (parameter is DTOmenu selectedDish)
            {

                FormCRUDMenuViewModel formCRUDMenuViewModel = new FormCRUDMenuViewModel();
                formCRUDMenuViewModel.GetObjectCommand.Execute(selectedDish);
            //  новое окно с formCRUDMenuViewModel
            //  экземпляр модального окна
            FormCrudMenu inputDialog = new FormCrudMenu();
                
                // DataContext окна вьюмодели
                inputDialog.DataContext = formCRUDMenuViewModel;

                if (inputDialog != Application.Current.MainWindow)
                {
                    //  Owner только если окно Application.Current.MainWindow уже показано
                    if (Application.Current.MainWindow != null && Application.Current.MainWindow.IsVisible)
                    {
                        inputDialog.Owner = Application.Current.MainWindow;
                    }

                    inputDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    var result = inputDialog.ShowDialog();

                    menuList = crudServieces.GetMenuList();
                }
            }
        }
        private void ExecuteCreateCommand(object parameter)
        {

            FormCRUDMenuViewModel formCRUDMenuViewModel = new FormCRUDMenuViewModel();
            formCRUDMenuViewModel.CreateObjectCommand.Execute(parameter);

            FormCrudMenu inputDialog = new FormCrudMenu();

            inputDialog.DataContext = formCRUDMenuViewModel;


            if (inputDialog != Application.Current.MainWindow)
            {
                //  Owner только если окно Application.Current.MainWindow уже показано
                if (Application.Current.MainWindow != null && Application.Current.MainWindow.IsVisible)
                {
                    inputDialog.Owner = Application.Current.MainWindow;
                }

                inputDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                var result = inputDialog.ShowDialog();

                menuList = crudServieces.GetMenuList();
            }

        }
        private void ExecuteDeliteCommand(object parameter)
        {
            try
            {
                if (parameter is DTOmenu selectedDish)
                {
                    DeliteFormViewModel formDeliteMenuViewModel = new DeliteFormViewModel();
                    formDeliteMenuViewModel.DeliteObjectCommand.Execute(selectedDish);

                    DeliteForm inputDialog = new DeliteForm();
                    inputDialog.DataContext = formDeliteMenuViewModel;

                    //  окно не пытается быть владельцем самого себя
                    if (inputDialog != Application.Current.MainWindow)
                    {
                        //  Owner только если окно Application.Current.MainWindow уже показано
                        if (Application.Current.MainWindow != null && Application.Current.MainWindow.IsVisible)
                        {
                            inputDialog.Owner = Application.Current.MainWindow;
                        }

                        inputDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        var result = inputDialog.ShowDialog();

                        menuList = crudServieces.GetMenuList();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ошибка при открытии окна: {ex.Message}");
            }
        }



        public menuCRUDViewModel()
        {
            menuList = crudServieces.GetMenuList();
            SaveCommand = new RelayComman(ExecuteSaveCommand);
            AddCommand = new RelayComman(ExecuteAddCommand);
            CreateCommand = new RelayComman(ExecuteCreateCommand);
            DeliteCommand = new RelayComman(ExecuteDeliteCommand);
            Log.Logger = new LoggerConfiguration()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

        }
        private void ExecuteSaveCommand(object obj)
        {
            if (obj is CheckBox checkBox)
            {
                if (checkBox.DataContext is DTOmenu dish)
                {
                    // Теперь у вас есть доступ к вашему объекту DTOmenu и значению чекбокса
                    bool? isChecked = checkBox.IsChecked;
                    orderServices.EditMenuAvaible((bool)isChecked, dish.Id);
                    OnPropertyChanged(nameof(menuList));
                }
            }
        }

    }
}
