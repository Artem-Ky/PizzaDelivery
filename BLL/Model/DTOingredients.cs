using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class DTOingredients : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string _name { get; set; }
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
        public int Count { get; set; }
        private int _weightOneCount { get; set; }
        public int WeightOneCount
        {
            get { return _weightOneCount; }
            set
            {
                if (_weightOneCount != value)
                {
                    _weightOneCount = value;
                    OnPropertyChanged(nameof(WeightOneCount));
                }
            }
        }
        private int _costForOneCount { get; set; }
        public int CostForOneCount
        {
            get { return _costForOneCount; }
            set
            {
                if (_costForOneCount != value)
                {
                    _costForOneCount = value;
                    OnPropertyChanged(nameof(CostForOneCount));
                }
            }
        }
        public bool IsAvalaible { get; set; }
        private string _photoIngr { get; set; }
        public string PhotoIngr
        {
            get { return _photoIngr; }
            set
            {
                if (_photoIngr != value)
                {
                    _photoIngr = value;
                    OnPropertyChanged(nameof(PhotoIngr));
                }
            }
        }
        public int IngredientType_id { get; set; }
        public string IngredientType { get; set; }
        private int _count;
        public int countT
        {
            get { return _count; }
            set
            {
                if (_count != value)
                {
                    _count = value;
                    OnPropertyChanged(nameof(countT));
                }
            }
        }
        public DTOingredients()
        {
        }

        public DTOingredients(int id, string name, int count, int weightOneCount, int costForOneCount, bool isAvalaible, string photoIngr, int ingredientType_id, int countTT)
        {
            Id = id;
            Name = name;
            Count = count;
            WeightOneCount = weightOneCount;
            CostForOneCount = costForOneCount;
            IsAvalaible = isAvalaible;
            PhotoIngr = photoIngr;
            IngredientType_id = ingredientType_id;
            countT = countTT;

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
