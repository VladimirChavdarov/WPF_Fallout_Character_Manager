using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External;

namespace WPF_Fallout_Character_Manager.Models.Serialization
{
    class PerksModelDTO
    {
        public List<Trait> Traits { get; set; } = new();
        public List<Perk> Perks { get; set; } = new();
    }
}
