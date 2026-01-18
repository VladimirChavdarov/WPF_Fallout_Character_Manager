using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization;

namespace WPF_Fallout_Character_Manager.Models.Serialization
{
    class SurvivalModelDTO
    {
        public ModValueDTO<int> Hunger { get; set; }
        public ModValueDTO<int> Dehydration { get; set; }
        public ModValueDTO<int> Exhaustion { get; set; }
        public ModValueDTO<int> RadDC { get; set; }
        public ModValueDTO<int> Rads { get; set; }
        public ModValueDTO<int> PassiveSense { get; set; }
        public ModValueDTO<int> PartyNerve { get; set; }
        public ModValueDTO<int> GroupSneak { get; set; }
    }
}
