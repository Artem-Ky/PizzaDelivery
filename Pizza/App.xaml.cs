
using Pizza.MVVM.View.UserMain;
using Pizza.MVVM.View;
using System;
using System.Windows;
using Ninject;
using BLL.interfaces;
using Interfaces.Services;
using Pizza.MVVM.Util;

namespace Pizza
{
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    //var mainView = new mainView();
                    //mainView.Show();
                    loginView.Close();
                }
            };

        }
    }
}
