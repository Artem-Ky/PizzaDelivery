using DAL.Entities;
using DAL.Interfaces;
using DAL.Repository;
using FontAwesome.Sharp;
using Pizza.MVVM.ViewModel.UserChild;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pizza.MVVM.ViewModel.UserMain
{
    public class MainViewModel : ViewModelBase
    {
        public event EventHandler ShowConstrViewRequested;
        //поля
        private user _currentUser;
        private pizzaContex dbcontext;
        private userRepositorySQL _userRepository;
        private ViewModelBase _currentChildView;
        private string _caption;
        private ImageSource _icon;

        public static MainViewModel Instance { get; private set; }

        public string WelcomeMessage => " ,Добро пожаловать";
        protected virtual void OnShowConstrViewRequested()
        {
            ShowConstrViewRequested?.Invoke(this, EventArgs.Empty);
        }
        public user CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
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
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public ImageSource Icon 
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        //command 
        public ICommand showMenuViewCommand { get;}
        public ICommand showConstrViewCommand { get; }
        public ICommand showCardViewCommand { get; }
        public ICommand showProfileViewCommand { get; }

        public MainViewModel() 
        {
            this.dbcontext = new pizzaContex();
            _userRepository = new userRepositorySQL(dbcontext);
            showMenuViewCommand = new RelayComman(ExecuteShowMenuViewCommand);
            showConstrViewCommand = new RelayComman(ExecuteShowConstrViewCommand);
            showCardViewCommand = new RelayComman(ExecuteShowCardViewCommand);
            showProfileViewCommand = new RelayComman(ExecuteShowProfileViewCommand);
            ExecuteShowMenuViewCommand(null);
            LoadCurrentUserData();
            Instance = this;
        }

        private void ExecuteShowProfileViewCommand(object obj)
        {
            CurrentChildView = new UserProfileView();
            Caption = "История заказов";
            BitmapImage bitmap = new BitmapImage(new Uri("/Pizza;component/Images/mainform/profile.png", UriKind.Relative));
            Icon = bitmap;
        }

        private void ExecuteShowConstrViewCommand(object obj)
        {
            CurrentChildView = new UserConstrView();
            Caption = "Конструктор пиццы";
            BitmapImage bitmap = new BitmapImage(new Uri("/Pizza;component/Images/mainform/const.png", UriKind.Relative));
            Icon = bitmap;

        }
        public void ShowConstrMenu (object obj)
        {
            ExecuteShowConstrViewCommand (obj);
            OnShowConstrViewRequested();
        }

        private void ExecuteShowCardViewCommand(object obj)
        {
            CurrentChildView = new UserCardViewModel();
            Caption = "Корзина";
            BitmapImage bitmap = new BitmapImage(new Uri("/Pizza;component/Images/mainform/card.png", UriKind.Relative));
            Icon = bitmap;
        }

        private void ExecuteShowMenuViewCommand(object obj)
        {
            CurrentChildView = new UserMenuView();
            Caption = "Меню";
            BitmapImage bitmap = new BitmapImage(new Uri("/Pizza;component/Images/mainform/menuLogo.png", UriKind.Relative));
            Icon = bitmap; 
        }

        private void LoadCurrentUserData()
        {
            var user = _userRepository.GetByUserName(Thread.CurrentPrincipal.Identity.Name);
            if (user == null)
            {
                MessageBox.Show("Invalid user");
                Application.Current.Shutdown();

            }
            else
                CurrentUser = user;
        }
        public int GetUserId ()
        {
            return CurrentUser.id;
        }

    }
}
