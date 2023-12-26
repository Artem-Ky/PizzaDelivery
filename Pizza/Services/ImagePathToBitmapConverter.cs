using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Pizza.Services
{
    public class ImagePathToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string imagePath)
            {
                if (IsLocalPath(imagePath))
                {
                    imagePath = "pack://application:,,,/Pizza;component" + imagePath;
                }

                return new BitmapImage(new Uri(imagePath));
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private bool IsLocalPath(string path)
        {
            // Ваш локальный путь может быть определен по его формату
            // Например, если он начинается с /Images/ или каким-то другим локальным ключевым словом
            return path.StartsWith("/Images/");
        }

        public class InRangeConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is int intValue && parameter is string range)
                {
                    string[] rangeValues = range.Split(',');

                    if (rangeValues.Length == 2 && int.TryParse(rangeValues[0], out int min) && int.TryParse(rangeValues[1], out int max))
                    {
                        return intValue >= min && intValue <= max;
                    }
                }

                return false;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }


    }
}
