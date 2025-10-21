using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF_Fallout_Character_Manager.Converters
{
    class KarmaCapCountToSizeConverter : IValueConverter
    {
        public float MaxSize { get; set; } = 180.0f;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count && count > 0)
            {
                return (float)(MaxSize / Math.Ceiling(Math.Sqrt(count)));
            }
            return 100.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
