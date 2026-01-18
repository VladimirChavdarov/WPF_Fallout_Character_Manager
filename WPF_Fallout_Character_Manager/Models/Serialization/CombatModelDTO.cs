using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization;

namespace WPF_Fallout_Character_Manager.Models.Serialization
{
    class CombatModelDTO
    {
        public ModValueDTO<int> ActionPoints { get; set; }
        public ModValueDTO<int> MaxStaminaPoints { get; set; }
        public ModValueDTO<int> StaminaPoints { get; set; }
        public ModValueDTO<int> MaxHealthPoints { get; set; }
        public ModValueDTO<int> HealthPoints { get; set; }
        public ModValueDTO<int> ArmorClass { get; set; }
        public ModValueDTO<int> DamageThreshold { get; set; }
        public ModValueDTO<int> CombatSequence { get; set; }
        public ModValueDTO<int> HealingRate { get; set; }
        public ModValueDTO<int> Fatigue { get; set; }
    }
}
