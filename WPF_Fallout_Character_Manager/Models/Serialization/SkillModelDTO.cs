using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization;

namespace WPF_Fallout_Character_Manager.Models.Serialization
{
    class SkillModelDTO
    {
        public Dictionary<Skill, ModIntSkillDTO> Skills { get; set; } = new();
    }
}
