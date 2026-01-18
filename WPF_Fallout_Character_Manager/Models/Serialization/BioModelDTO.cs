using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization;

namespace WPF_Fallout_Character_Manager.Models.Serialization
{
    class BioModelDTO
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public string Background {  get; set; }
        public string AppearanceDescription { get; set; }
        public string Backstory {  get; set; }
        public string Virtues { get; set; }
        public string Vices { get; set; }
        public int LevelNum { get; set; }
        public ModValueDTO<int> XP { get; set; }
        public string ImageSource { get; set; }
        public List<bool> KarmaCaps { get; set; }
    }
}
