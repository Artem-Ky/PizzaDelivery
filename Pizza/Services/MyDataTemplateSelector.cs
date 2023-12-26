using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using DAL.Entities;
using BLL.Model;

namespace Pizza.Services
{
    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Template1 { get; set; }
        public DataTemplate Template2 { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is DTOmenu)
                return Template1;
            else if (item is DTOconstr)
                return Template2;

            return base.SelectTemplate(item, container);
        }
    }
}
