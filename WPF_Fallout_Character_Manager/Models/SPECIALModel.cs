using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WPF_Fallout_Character_Manager.Models.ModifierSystem;
namespace WPF_Fallout_Character_Manager.Models
{
    internal sealed class SPECIALModel
    {
        public SPECIALModel()
        {
            IntegerValues = new Dictionary<string, ModInt>();

            IntegerValues.Add("Strength", new ModInt(1));
            IntegerValues.Add("Perception", new ModInt(2));
            IntegerValues.Add("Endurance", new ModInt(3));
            IntegerValues.Add("Charisma", new ModInt(4));
            IntegerValues.Add("Intelligence", new ModInt(5));
            IntegerValues.Add("Agility", new ModInt(6));
            IntegerValues.Add("Luck", new ModInt(7));
        }

        public Dictionary<string, ModInt>? IntegerValues;
    }
}
