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
using System.Windows.Shapes;

namespace Pizza.MVVM.View.UserChild
{
    /// <summary>
    /// Логика взаимодействия для NumberInputWindow.xaml
    /// </summary>
    public partial class NumberInputWindow : Window
    {
        public NumberInputWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что введенное значение - целое число
            if (int.TryParse(NumberTextBox.Text, out int enteredNumber))
            {
                // Сохраняем введенное число в свойство EnteredNumber вьюмодели
                ((NumberInputViewModel)DataContext).EnteredNumber = enteredNumber;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Некорректный ввод. Введите целое число.");
            }

        }

    }
}
