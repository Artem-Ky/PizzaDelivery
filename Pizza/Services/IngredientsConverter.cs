using BLL.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Pizza.Services
{
    public class IngredientsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<DTOingredients> ingredientsList)
            {
                // Объединяем имена ингредиентов в одну строку
                var ingredientsNames = string.Join(", ", ingredientsList.Select(i => i.Name));
                return $"Пицца из конструктора ({ingredientsNames})";
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
