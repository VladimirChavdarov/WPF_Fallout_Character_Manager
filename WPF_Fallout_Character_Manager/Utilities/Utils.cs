using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.Utilities
{
    // https://stackoverflow.com/questions/17810092/how-to-bind-an-observablecollectionbool-to-a-listbox-of-checkboxes-in-wpf
    public class TypeWrap<T> : INotifyPropertyChanged
    {
        private T value;
        public T Value
        {
            get { return value; }
            set
            {
                {
                    this.value = value;
                    OnPropertyChanged();
                }
            }
        }

        public static implicit operator TypeWrap<T>(T value)
        {
            return new TypeWrap<T> { value = value };
        }
        public static implicit operator T(TypeWrap<T> wrapper)
        {
            return wrapper.value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    static class Utils
    {
        public static float FloatFromString(string s)
        {
            if (float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
            {
                return result;
            }

            throw new Exception($"Failed to convert {s} to float...");
        }

        /// <summary>
        /// Returns the numeric value of the cost multiplier.
        /// If isFlatCost is true,you should use the return value as a flat cost increase.
        /// If it is false, you should use the return value as a cost multiplier.
        /// </summary>
        /// <param name="costMultiplier">The string you want to convert to a numeric value.</param>
        /// <param name="isFlatCost">True if the function output should be used as a flat cost increase. False if the function output should be used as a cost multiplier.</param>
        /// <returns></returns>
        public static float GetCostMultiplier(string costMultiplier, out bool isFlatCost)
        {
            if (costMultiplier.StartsWith("x"))
            {
                string ss = costMultiplier.Substring(1);
                isFlatCost = false;
                return Utils.FloatFromString(ss);
            }
            else if (costMultiplier.StartsWith("c"))
            {
                string ss = costMultiplier.Substring(1);
                isFlatCost = true;
                return Utils.FloatFromString(ss);
            }

            throw new Exception($"CostModifier has a wrong format: {costMultiplier}");
        }

        public static bool GetRangeMultiplier(string ranges, out ValueTuple<int, int> result)
        {
            result = default;
            if (!ranges.Contains('/'))
                return false;
            var rangeArr = ranges.Split('/');
            string ss1 = rangeArr[0].Substring(1);
            string ss2 = rangeArr[1].Substring(1);
            result = new ValueTuple<int, int>(Int32.Parse(ss1), Int32.Parse(ss2));
            return true;

        }
    }
}
