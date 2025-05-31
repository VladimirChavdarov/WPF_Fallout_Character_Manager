using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
namespace WPF_Fallout_Character_Manager.Models
{
    internal sealed class SurvivalModel
    {
        public SurvivalModel()
        {
            IntegerValues = new Dictionary<string, ModInt>();

            IntegerValues.Add("Hunger", new ModInt(1));
            IntegerValues.Add("Dehydration", new ModInt(2));
            IntegerValues.Add("Exhaustion", new ModInt(3));
            IntegerValues.Add("RadDC", new ModInt(4));
            IntegerValues.Add("Rads", new ModInt(5));
            IntegerValues.Add("PassiveSense", new ModInt(6));
            IntegerValues.Add("PartyNerve", new ModInt(7));
            IntegerValues.Add("GroupSneak", new ModInt(8));
        }

        public Dictionary<string, ModInt>? IntegerValues;
    }
}