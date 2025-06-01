using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF_Fallout_Character_Manager.Converters
{
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,  object parameter, System.Globalization.CultureInfo culture)
        {
            return value?.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse(value?.ToString(), out int result))
                return result;

            return 0;
        }
    }
}
