using DAL.Entities;
using DAL.Interfaces;
using DAL.Repository;
using Pizza.MVVM.View;
using Pizza.MVVM.View.UserMain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pizza.MVVM.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;
        private pizzaContex dbcontext;
        private userRepositorySQL userRepository;
        private user _currentUser;
        //Properties
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }
        //-> Commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }
        //Constructor
        public LoginViewModel()
        {
            this.dbcontext = new pizzaContex();
            userRepository = new userRepositorySQL(dbcontext);
            LoginCommand = new RelayComman(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new RelayComman(p => ExecuteRecoverPassCommand("", ""));
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }
        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                _currentUser = userRepository.GetByUserName(Thread.CurrentPrincipal.Identity.Name);
                if (!_currentUser.isManager)
                {
                    var mainView = new mainView();
                    mainView.Show();
                }
                else if (_currentUser.isManager)
                {
                    var mainView = new managerMainView();
                    mainView.Show();
                }
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }
        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
    }
}