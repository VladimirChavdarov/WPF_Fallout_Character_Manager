using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.Models
{
    internal sealed class SPECIALModel
    {
        public SPECIALModel()
        {
            Strength = 1;
            Perception = 2;
            Endurance = 3;
            Charisma = 4;
            Intelligence = 5;
            Agility = 6;
            Luck = 7;
        }

        public int GetModifier(int MainStat)
        {
            return MainStat - 5;
        }

        public int Strength {  get; set; }
        public int Perception {  get; set; }
        public int Endurance {  get; set; }
        public int Charisma {  get; set; }
        public int Intelligence {  get; set; }
        public int Agility {  get; set; }
        public int Luck {  get; set; }
    }
}
