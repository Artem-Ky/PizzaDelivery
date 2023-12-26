using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Pizza.Services
{
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
