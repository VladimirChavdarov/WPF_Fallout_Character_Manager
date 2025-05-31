using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;

namespace WPF_Fallout_Character_Manager.Converters
{
    public class ModIntKeyToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Dictionary<string, ModInt> dict && parameter is string key)
            {
                return dict.TryGetValue(key, out ModInt modInt);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
