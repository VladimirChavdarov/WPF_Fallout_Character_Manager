using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;

namespace WPF_Fallout_Character_Manager.Models.Serialization
{
    public class LimbConditionDTO
    {
        public LabeledString BaseValue { get; set; }
        public string Target { get; set; }
        public string APCost { get; set; }
        public string Modifier { get; set; }
        public string Effects { get; set; }
    }
}
