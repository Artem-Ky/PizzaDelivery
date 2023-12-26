using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace Pizza.Services
{
    public class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                return count == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class CountToVisibilityConverterReverse : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                return count != 0 ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MultiVisibilityLastButtonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is int totalPageNumber && values[1] is int currentPageNumber)
            {
                // Проверяем условия видимости кнопки
                if (totalPageNumber < 2 || totalPageNumber == currentPageNumber)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }

            return Visibility.Visible; // По умолчанию делаем видимой кнопку
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class MultiVisibilityStartButtonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is int totalPageNumber && values[1] is int currentPageNumber)
            {
                // Проверяем условия видимости кнопки
                if (totalPageNumber < 2 || currentPageNumber == 1)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }

            return Visibility.Visible; // По умолчанию делаем видимой кнопку
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class VisibilityConverterForPointPage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Преобразование значения CurrentPageNumber в видимость или невидимость
            if (value is int currentPageNumber)
            {
                return currentPageNumber > 4 ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed; // Если значение не int, делаем кнопку невидимой
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class VisibilityConverterForbackPage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Преобразование значения CurrentPageNumber в видимость или невидимость
            if (value is int currentPageNumber)
            {
                return currentPageNumber > 2 ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed; // Если значение не int, делаем кнопку невидимой
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class VisibilityConverterForback2Page : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Преобразование значения CurrentPageNumber в видимость или невидимость
            if (value is int currentPageNumber)
            {
                return currentPageNumber > 3 ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed; // Если значение не int, делаем кнопку невидимой
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class MultiVisibilityNextButtonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is int totalPageNumber && values[1] is int currentPageNumber)
            {
                // Проверяем условия видимости кнопки
                if (totalPageNumber - currentPageNumber > 1)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }

            return Visibility.Collapsed; // По умолчанию делаем видимой кнопку
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class MultiVisibilityNext2ButtonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is int totalPageNumber && values[1] is int currentPageNumber)
            {
                // Проверяем условия видимости кнопки
                if (totalPageNumber - currentPageNumber > 2)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }

            return Visibility.Collapsed; // По умолчанию делаем видимой кнопку
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class MultiVisibilityPoint2ButtonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is int totalPageNumber && values[1] is int currentPageNumber)
            {
                // Проверяем условия видимости кнопки
                if (totalPageNumber - currentPageNumber > 3)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }

            return Visibility.Collapsed; // По умолчанию делаем видимой кнопку
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class VisibilityConverterForCurrentPage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Преобразование значения CurrentPageNumber в видимость или невидимость
            if (value is int TotalPageNumber)
            {
                return TotalPageNumber > 1 ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed; // Если значение не int, делаем кнопку невидимой
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

