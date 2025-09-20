using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.Utilities
{
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
        static float GetCostMultiplier(string costMultiplier, out bool isFlatCost)
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
    }
}
