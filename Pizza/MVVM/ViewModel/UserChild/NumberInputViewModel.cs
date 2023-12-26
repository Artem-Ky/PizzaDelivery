using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.MVVM.ViewModel.UserChild
{
    public class NumberInputViewModel : ViewModelBase //возможно сломал ссылки на этот класс
    {
        private int _enteredNumber;

        public int EnteredNumber
        {
            get { return _enteredNumber; }
            set
            {
                if (_enteredNumber != value)
                {
                    _enteredNumber = value;
                    OnPropertyChanged(nameof(EnteredNumber));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
