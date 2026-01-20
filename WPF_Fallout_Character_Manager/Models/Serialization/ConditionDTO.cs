using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;

namespace WPF_Fallout_Character_Manager.Models.Serialization
{
    class ConditionDTO
    {
        public LabeledString BaseValue { get; set; }
        public bool IsReadOnly { get; set; }
        public Visibility DescriptionVisibility { get; set; }
    }
}
