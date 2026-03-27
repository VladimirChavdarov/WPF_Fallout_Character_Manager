using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.Models.External.Serialization
{
    class XtrnlPerksModelDTO
    {
        public List<Trait> Traits { get; set; } = new List<Trait>();
        public List<Perk> Perks { get; set; } = new List<Perk>();
    }
}
