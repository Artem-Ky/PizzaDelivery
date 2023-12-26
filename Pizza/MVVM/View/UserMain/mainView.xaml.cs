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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Pizza.MVVM.ViewModel.UserMain;

namespace Pizza.MVVM.View.UserMain
{
    /// <summary>
    /// Логика взаимодействия для mainView.xaml
    /// </summary>
    public partial class mainView : Window
    {
        private bool isResizing = false;
        private Point lastMousePosition;
        private double minWindowWidth = 860; // Минимальная ширина окна
        private double minWindowHeight = 600; // Минимальная высота окна
        public mainView()
        {
            InitializeComponent();
            MainViewModel mainViewModel = new MainViewModel();
            DataContext = mainViewModel;
            MainViewModel.Instance.ShowConstrViewRequested += OnShowConstrViewRequested;


        }

        private void OnShowConstrViewRequested(object sender, EventArgs e)
        {
            consblock.IsChecked = true;
            cardblock.IsChecked = false;
        }


        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }
        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point cursorPosition = e.GetPosition(this);
                Rect resizeArea = new Rect(Width - 20, 0, 20, 20);

                if (resizeArea.Contains(cursorPosition))
                {
                    isResizing = true;
                    lastMousePosition = cursorPosition;
                    Mouse.OverrideCursor = Cursors.SizeNWSE;
                    CaptureMouse();
                }
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (isResizing)
            {
                Point currentPosition = e.GetPosition(this);
                double deltaX = currentPosition.X - lastMousePosition.X;
                double deltaY = currentPosition.Y - lastMousePosition.Y;

                double newWidth = Width + deltaX;
                double newHeight = Height - deltaY;

                // Проверка на минимальные размеры
                if (newWidth >= minWindowWidth && newHeight >= minWindowHeight)
                {
                    Width = newWidth;
                    Height = newHeight;
                }

                lastMousePosition = currentPosition;
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isResizing)
            {
                isResizing = false;
                Mouse.OverrideCursor = null;
                ReleaseMouseCapture();
            }
        }

    }
}
