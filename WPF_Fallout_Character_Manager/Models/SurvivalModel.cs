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
            Hunger = new ModInt(1);
            Dehydration = new ModInt(2);
            Exhaustion = new ModInt(3);
            RadDC = new ModInt(4);
            Rads = new ModInt(5);
            PassiveSense = new ModInt(6);
            PartyNerve = new ModInt(7);
            GroupSneak = new ModInt(8);
        }

        public ModInt Hunger { get; set; }
        public ModInt Dehydration { get; set; }
        public ModInt Exhaustion { get; set; }
        public ModInt RadDC { get; set; }
        public ModInt Rads { get; set; }
        public ModInt PassiveSense { get; set; }
        public ModInt PartyNerve { get; set; }
        public ModInt GroupSneak { get; set; }
    }
}
