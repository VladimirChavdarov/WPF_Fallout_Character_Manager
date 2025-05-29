using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.Models
{
    internal sealed class SurvivalModel
    {
        public SurvivalModel()
        {
            Hunger = 0;
            Dehydration = 0;
            Exhaustion = 0;
            RadDC = 0;
            Rads = 0;
            PassiveSense = 0;
            PartyNerve = 0;
            GroupSneak = 0;
        }

        public int Hunger { get; set; }
        public int Dehydration { get; set; }
        public int Exhaustion { get; set; }
        public int RadDC { get; set; }
        public int Rads { get; set; }
        public int PassiveSense { get; set; }
        public int PartyNerve { get; set; }
        public int GroupSneak { get; set; }
    }
}
