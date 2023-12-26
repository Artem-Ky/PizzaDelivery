using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pizza.MVVM.ViewModel
{
    public class RelayComman : ICommand
    {
        //поля
        private readonly Action<object> _executeAction; //запуск метода
        private Predicate<object> _canExecuteAction; //проверка возможено ли действие (вкл/выкл элемента управления)



        //конструкторы
        public RelayComman(Action<object> executeAction) //без проверки
        {
            _executeAction = executeAction;
            _canExecuteAction = null;
        }
        public RelayComman(Action<object> executeAction, Predicate<object> canExecuteAction) //с проверкой
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }


        
        //события
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }


        
        //методы
        public bool CanExecute(object parameter)
        {
            return _canExecuteAction == null?true:_canExecuteAction(parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }
    }
}
