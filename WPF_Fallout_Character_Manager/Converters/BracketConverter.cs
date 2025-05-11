using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF_Fallout_Character_Manager.Converters
{
    public class BracketConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s && s.StartsWith(" ") && s.EndsWith(" "))
            {
                return $"[{s.Substring(1, s.Length - 2)}]";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s && s.StartsWith("[") && s.EndsWith("]"))
            {
                return s.Substring(1, s.Length - 2);
            }
            return value;
        }
    }
}
