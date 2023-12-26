using BLL.Model;
using Interfaces.Services;
using LiveCharts;
using Ninject.Parameters;
using Pizza.MVVM.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pizza.MVVM.ViewModel.ManagerChild
{
    public class FormBestWorseReportViewModel : ViewModelBase
    {
        IServicesReport reportServices = NinjectServicesSingleton.ReportServices;
        private Dictionary<string, int> _diagramData;
        public Dictionary<string, int> DiagramData
        {
            get { return _diagramData; }
            set
            {
                if (_diagramData != value)
                {
                    _diagramData = value;
                    OnPropertyChanged(nameof(DiagramData));
                }
            }
        }
        private ReportBestWorseData reportBestWorseData;
        public ICommand SetCommand { get; set; }
        public FormBestWorseReportViewModel()
        {
            reportBestWorseData = new ReportBestWorseData();
            SetCommand = new RelayComman(SetData);
        }

        private void SetData(object parameter)
        {
            if (parameter is ReportBestWorseData selected)
            {
                reportBestWorseData = selected;
                DiagramData = reportServices.ReportBestWorse(reportBestWorseData);
                
            }
        }
    }
}
