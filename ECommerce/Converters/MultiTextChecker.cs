using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ECommerce.Converters
{
    public class MultiTextChecker : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (String.IsNullOrEmpty( values[0].ToString()) || String.IsNullOrEmpty(values[1].ToString()))
            {
                return false;
            }
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] values = null;
            if (value != null)
                return values = value.ToString().Split(' ');
            return values;
        }
    }
}
