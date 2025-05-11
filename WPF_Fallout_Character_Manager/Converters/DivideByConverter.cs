using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WPF_Fallout_Character_Manager.Converters
{
    public class DivideByConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d && parameter != null &&
                double.TryParse(parameter.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out double divisor) &&
                divisor != 0)
            {
                return d * divisor;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
