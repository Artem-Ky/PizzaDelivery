using BLL.Model;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Pizza.Services
{
    public class ListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<DTOingredients> list)
            {
                var ingredientsWithCount = list.Select(item => $"{item.Name} x{item.countT}");
                return string.Join(", ", ingredientsWithCount);
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class MenuItemsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<DTOmenu> list)
            {
                var ingredientsWithCount = list.Select(item => $"{item.Name} x{item.count}");
                return string.Join(", ", ingredientsWithCount);
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class CustomItemsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<DTOconstr> list)
            {
                var result = new List<string>();
                string name = "Пицца из конструктора";

                foreach (var item in list)
                {
                    var ingrRes = new List<string>();
                    foreach (var ingr in item.IngrList)
                    {
                        ingrRes.Add($"{ingr.Name} x{ingr.countT}");
                    }

                    var ingrlist = string.Join(", ", ingrRes);
                    result.Add($"{name} x{item.count} (Cостав: {ingrlist})");
                }

                return string.Join("\n", result);
            }

            return string.Empty;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}