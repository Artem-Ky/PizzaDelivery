using BLL.Model;
using Interfaces.Services;
using Pizza.MVVM.Util;
using Pizza.MVVM.View.ManagerChild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pizza.MVVM.ViewModel.ManagerChild
{
    public class reportViewModel : ViewModelBase
    {
        private ReportBestWorseData reportBestWorseData;
        private ReportTimeData reportTimeData;
        private DateTime _dateStart;

        public DateTime DateStart
        {
            get { return _dateStart; }
            set
            {
                if (_dateStart != value)
                {
                    _dateStart = value;
                    OnPropertyChanged(nameof(DateStart));
                }
            }
        }
        private DateTime _dateEnd;

        public DateTime DateEnd
        {
            get { return _dateEnd; }
            set
            {
                if (_dateEnd != value)
                {
                    _dateEnd = value;
                    OnPropertyChanged(nameof(DateEnd));
                }
            }
        }
        private DateTime _dateStart2;

        public DateTime DateStart2
        {
            get { return _dateStart2; }
            set
            {
                if (_dateStart2 != value)
                {
                    _dateStart2 = value;
                    OnPropertyChanged(nameof(DateStart2));
                }
            }
        }
        private DateTime _dateEnd2;

        public DateTime DateEnd2
        {
            get { return _dateEnd2; }
            set
            {
                if (_dateEnd2 != value)
                {
                    _dateEnd2 = value;
                    OnPropertyChanged(nameof(DateEnd2));
                }
            }
        }
        public ICommand bestWorseCommand { get; set; }
        public ICommand timeCommand { get; set; }
        public reportViewModel() 
        {
            reportBestWorseData = new ReportBestWorseData();
            reportTimeData = new ReportTimeData();
            DateStart = DateTime.Today;
            DateEnd = DateTime.Today;
            DateStart2 = DateTime.Today;
            DateEnd2 = DateTime.Today;
            timeCommand = new RelayComman(ExecuteTimeReportCommand);
            bestWorseCommand = new RelayComman(ExecuteBestWorseReportCommand);
        }
        private void ExecuteBestWorseReportCommand(object parameter)
        {
            reportBestWorseData.DateStart = DateStart.AddDays(-1);
            reportBestWorseData.DateEnd = DateEnd.AddDays(1);

            FormBestWorseReportViewModel formBestWorseViewModel = new FormBestWorseReportViewModel();
            formBestWorseViewModel.SetCommand.Execute(reportBestWorseData);

            FormBestWorseReportView inputDialog = new FormBestWorseReportView();


            inputDialog.DataContext = formBestWorseViewModel;

            if (inputDialog != Application.Current.MainWindow)
            {
                //  Owner только если окно Application.Current.MainWindow уже показано
                if (Application.Current.MainWindow != null && Application.Current.MainWindow.IsVisible)
                {
                    inputDialog.Owner = Application.Current.MainWindow;
                }

                inputDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                inputDialog.ShowDialog();

            }

        }
        private void ExecuteTimeReportCommand(object parameter)
        {
            reportTimeData.DateStart = DateStart2.AddDays(-1);
            reportTimeData.DateEnd = DateEnd2.AddDays(1);

            FormTimeReportViewModel formTimeViewModel = new FormTimeReportViewModel();
            formTimeViewModel.SetCommand.Execute(reportTimeData);

            FormTimeReportView inputDialog = new FormTimeReportView();


            inputDialog.DataContext = formTimeViewModel;

            if (inputDialog != Application.Current.MainWindow)
            {
                //  Owner только если окно Application.Current.MainWindow уже показано
                if (Application.Current.MainWindow != null && Application.Current.MainWindow.IsVisible)
                {
                    inputDialog.Owner = Application.Current.MainWindow;
                }

                inputDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                inputDialog.ShowDialog();
            }

        }

    }
}
