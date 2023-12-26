using BLL.Model;
using Interfaces.Services;
using LiveCharts;
using Pizza.MVVM.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pizza.MVVM.ViewModel.ManagerChild
{
    public class FormTimeReportViewModel : ViewModelBase
    {
        private ChartValues<int> _chartValues;
        public Func<int, string> PointLabel { get; set; }
        public ChartValues<int> ChartValues
        {
            get { return _chartValues; }
            set
            {
                if (_chartValues != value)
                {
                    _chartValues = value;
                    OnPropertyChanged(nameof(ChartValues));
                }
            }
        }

        private List<string> _labels;
        public List<string> Labels
        {
            get { return _labels; }
            set
            {
                if (_labels != value)
                {
                    _labels = value;
                    OnPropertyChanged(nameof(Labels));
                }
            }
        }
        IServicesReport reportServices = NinjectServicesSingleton.ReportServices;
        private Dictionary<int, int> _diagramData;
        public Dictionary<int, int> DiagramData
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
        public ICommand SetCommand { get; set; }
        private ReportTimeData reportTimeData;

        public FormTimeReportViewModel()
        {
            SetCommand = new RelayComman(SetData);
        }

        private void SetData(object parameter)
        {
            if (parameter is ReportTimeData selected)
            {
                reportTimeData = selected;
                DiagramData = reportServices.ReportTime(reportTimeData);
                ChartValues = new ChartValues<int>(DiagramData.Values);
                Labels = DiagramData.Keys.Select(key => key.ToString()).ToList();

            }
        }

    }
}
