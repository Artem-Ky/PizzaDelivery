using DAL.Entities;
using DAL.Repository;
using Pizza.MVVM.ViewModel.UserChild;
using Pizza.MVVM.View.ManagerChild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Pizza.MVVM.View;
using Pizza.MVVM.ViewModel.ManagerChild;

namespace Pizza.MVVM.ViewModel.ManagerMain
{
    public class ManagerMainViewModel : ViewModelBase
    {
        public event EventHandler ShowConstrViewRequested;
        //поля
        private pizzaContex dbcontext;
        private ViewModelBase _currentChildView;

        public static ManagerMainViewModel Instance { get; private set; }

        protected virtual void OnShowConstrViewRequested()
        {
            ShowConstrViewRequested?.Invoke(this, EventArgs.Empty);
        }

        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        //command 
        public ICommand showMenuViewCommand { get; }
        public ICommand showIngrViewCommand { get; }
        public ICommand showOrderViewCommand { get; }
        public ICommand showReportViewCommand { get; }

        public ManagerMainViewModel()
        {
            this.dbcontext = new pizzaContex();
            showMenuViewCommand = new RelayComman(ExecuteShowMenuViewCommand);
            showIngrViewCommand = new RelayComman(ExecuteShowIngrViewCommand);
            showOrderViewCommand = new RelayComman(ExecuteShowOrderViewCommand);
            showReportViewCommand = new RelayComman(ExecuteShowReportViewCommand);
            ExecuteShowMenuViewCommand(null);
            Instance = this;
        }

        private void ExecuteShowReportViewCommand(object obj)
        {
            CurrentChildView = new reportViewModel();
        }

        private void ExecuteShowIngrViewCommand(object obj)
        {
            CurrentChildView = new ingrCRUDViewModel();

        }

        private void ExecuteShowOrderViewCommand(object obj)
        {
            CurrentChildView = new OrderCRUDViewModel();
        }

        private void ExecuteShowMenuViewCommand(object obj)
        {
            CurrentChildView = new menuCRUDViewModel();
        }


    }
}
