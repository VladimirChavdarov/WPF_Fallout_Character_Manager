using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPF_Fallout_Character_Manager.Models;

namespace WPF_Fallout_Character_Manager.Converters
{
    public class SPECIALToShortConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SPECIAL special)
            {
                return special switch
                {
                    SPECIAL.Strength => "STR",
                    SPECIAL.Perception => "PER",
                    SPECIAL.Endurance => "END",
                    SPECIAL.Charisma => "CHA",
                    SPECIAL.Intelligence => "INT",
                    SPECIAL.Agility => "AGI",
                    SPECIAL.Luck => "LCK",
                    _ => special.ToString()
                };
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
