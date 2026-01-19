using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization
{
    public class ModIntSkillDTO : ModValueDTO<int>
    {
        public bool IsTagged { get; set; }
        public SPECIAL SelectedModifier { get; set; }
        public List<SPECIAL> SkillModifiers { get; set; } = new();
    }
}
