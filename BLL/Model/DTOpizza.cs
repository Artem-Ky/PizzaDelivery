using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class DTOpizza : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        private int _cost { get; set; }
        public int Cost
        {
            get { return _cost; }
            set
            {
                if (_cost != value)
                {
                    _cost = value;
                    OnPropertyChanged(nameof(Cost));
                }
            }
        }
        private string _about { get; set; }
        public string About
        {
            get { return _about; }
            set
            {
                if (_about != value)
                {
                    _about = value;
                    OnPropertyChanged(nameof(About));
                }
            }
        }
        public int _pizzaTypeId { get; set; }
        public int PizzaTypeId
        {
            get { return _pizzaTypeId; }
            set
            {
                if (_pizzaTypeId != value)
                {
                    _pizzaTypeId = value;
                    OnPropertyChanged(nameof(PizzaTypeId));
                }
            }
        }

        private int _allPizzaCost;
        public int allPizzaCost
        {
            get { return _allPizzaCost; }
            set
            {
                if (_allPizzaCost != value)
                {
                    _allPizzaCost = value;
                    OnPropertyChanged(nameof(allPizzaCost));
                }
            }
        }
        private int _weight { get; set; }
        public int Weight
        {
            get { return _weight; }
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                    OnPropertyChanged(nameof(Weight));
                }
            }
        }

        private int _count;
        public int count
        {
            get { return _count; }
            set
            {
                if (_count != value)
                {
                    _count = value;
                    OnPropertyChanged(nameof(count));
                }
            }
        }
        public string PizzaType { get; set; }


        public DTOpizza()
        {
        }

        public DTOpizza(int id, string name, int cost, string about, int pizza_type_id, int Count)
        {
            Id = id;
            Name = name;
            Cost = cost;
            About = about;
            PizzaTypeId = pizza_type_id;
            count = Count;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
