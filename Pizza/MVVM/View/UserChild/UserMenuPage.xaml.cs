using DAL.Entities;
using DAL.Repository;
using Pizza.MVVM.ViewModel.UserChild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pizza.MVVM.View.UserChild
{
    public partial class UserMenuPage : UserControl
    {
        public UserMenuPage()
        {
            //UserMenuView viewModel = new UserMenuView();

            //// Установка созданного экземпляра UserMenuView в качестве DataContext
            //DataContext = viewModel;
            InitializeComponent();
            // Создание экземпляра UserMenuView

        }
    }
}
