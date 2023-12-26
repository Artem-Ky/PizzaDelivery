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
using BLL.interfaces;

namespace Pizza.MVVM.ViewModel.ManagerChild
{
    public class ingrCRUDViewModel : ViewModelBase
    {
      //  IServicesOrders orderServices = NinjectServicesSingleton.OrderServices;
        IServicesIngredients ingrServices = NinjectServicesSingleton.IngredientsServices;
        IServicesCRUD crudServieces = NinjectServicesSingleton.CRUDServices;
        private List<DTOingredients> _ingrList;
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
        public List<DTOingredients> ingrList
        {
            get { return _ingrList; }
            set
            {
                _ingrList = value;
                OnPropertyChanged(nameof(ingrList));
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeliteCommand { get; set; }
        public ICommand CreateCommand { get; set; }
        private void ExecuteAddCommand(object parameter)
        {
            if (parameter is DTOingredients selectedDish)
            {

                FormCRUDIngrViewModel formCRUDIngrViewModel = new FormCRUDIngrViewModel();
                formCRUDIngrViewModel.GetObjectCommand.Execute(selectedDish);

                FormCRUDIngr inputDialog = new FormCRUDIngr();

                inputDialog.DataContext = formCRUDIngrViewModel;

                if (inputDialog != Application.Current.MainWindow)
                {
                    //  Owner только если окно Application.Current.MainWindow уже показано
                    if (Application.Current.MainWindow != null && Application.Current.MainWindow.IsVisible)
                    {
                        inputDialog.Owner = Application.Current.MainWindow;
                    }

                    inputDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    var result = inputDialog.ShowDialog();

                    ingrList = crudServieces.GetIngrList();
                }


            }
        }
        private void ExecuteCreateCommand(object parameter)
        {

            FormCRUDIngrViewModel formCRUDIngrViewModel = new FormCRUDIngrViewModel();
            formCRUDIngrViewModel.CreateObjectCommand.Execute(parameter);

            FormCRUDIngr inputDialog = new FormCRUDIngr();


            inputDialog.DataContext = formCRUDIngrViewModel;

            if (inputDialog != Application.Current.MainWindow)
            {
                //  Owner только если окно Application.Current.MainWindow уже показано
                if (Application.Current.MainWindow != null && Application.Current.MainWindow.IsVisible)
                {
                    inputDialog.Owner = Application.Current.MainWindow;
                }

                inputDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                var result = inputDialog.ShowDialog();

                ingrList = crudServieces.GetIngrList();
            }
        }
        private void ExecuteDeliteCommand(object parameter)
        {
            if (parameter is DTOingredients selectedDish)
            {

                DeliteFormViewModel formDeliteMenuViewModel = new DeliteFormViewModel();
                formDeliteMenuViewModel.DeliteIngrCommand.Execute(selectedDish);

                DeliteForm inputDialog = new DeliteForm();


                inputDialog.DataContext = formDeliteMenuViewModel;

                if (inputDialog != Application.Current.MainWindow)
                {
                    //  Owner только если окно Application.Current.MainWindow уже показано
                    if (Application.Current.MainWindow != null && Application.Current.MainWindow.IsVisible)
                    {
                        inputDialog.Owner = Application.Current.MainWindow;
                    }

                    inputDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    var result = inputDialog.ShowDialog();

                    ingrList = crudServieces.GetIngrList();
                }
            }
        }

        public ingrCRUDViewModel()
        {
            ingrList = crudServieces.GetIngrList();
            SaveCommand = new RelayComman(ExecuteSaveCommand);
            AddCommand = new RelayComman(ExecuteAddCommand);
            CreateCommand = new RelayComman(ExecuteCreateCommand);
            DeliteCommand = new RelayComman(ExecuteDeliteCommand);

        }
        private void ExecuteSaveCommand(object obj)
        {
            if (obj is CheckBox checkBox)
            {
                if (checkBox.DataContext is DTOingredients dish)
                {
                    bool? isChecked = checkBox.IsChecked;
                    ingrServices.EditIngrAvaible((bool)isChecked, dish.Id);
                    OnPropertyChanged(nameof(ingrList));
                }
            }
        }

    }
}
