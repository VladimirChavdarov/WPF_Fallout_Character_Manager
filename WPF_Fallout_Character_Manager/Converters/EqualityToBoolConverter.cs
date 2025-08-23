using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF_Fallout_Character_Manager.Converters
{
    public class EqualityToBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2) return false;
            return values[0]?.Equals(values[1]) ?? false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            //if ((bool)value)
            //{
            //    // When this RadioButton is checked, return its enum as the SelectedModifier
            //    return new object[] { Binding.DoNothing, values[0] };
            //}
            return new object[] { Binding.DoNothing, Binding.DoNothing };
        }
    }
}
