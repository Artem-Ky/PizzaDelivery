using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL.Model
{
    public class DTOmenu : DTOpizza
    {
        private string _photo { get; set; }
        public string Photo
        {
            get { return _photo; }
            set
            {
                if (_photo != value)
                {
                    _photo = value;
                    OnPropertyChanged(nameof(Photo));
                }
            }
        }

        private bool _isAvailable;

        public bool IsAvailable
        {
            get { return _isAvailable; }
            set
            {
                if (_isAvailable != value)
                {
                    _isAvailable = value;
                    OnPropertyChanged(nameof(IsAvailable));
                }
            }
        }

        public DTOmenu()
        {
        }

        public DTOmenu(int id, string name, int cost, string about, bool isAvailable, int pizza_type_id, string photo, int count)
            : base(id, name, cost, about, pizza_type_id, count)
        {
            Photo = photo;
            IsAvailable = isAvailable;
        }
    }
}
