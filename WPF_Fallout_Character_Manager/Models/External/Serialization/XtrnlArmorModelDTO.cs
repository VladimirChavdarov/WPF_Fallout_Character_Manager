using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;

namespace WPF_Fallout_Character_Manager.Models.External.Serialization
{
    class XtrnlArmorModelDTO
    {
        public List<ArmorUpgrade> Upgrades { get; set; } = new List<ArmorUpgrade>();

        public List<ArmorDTO> Armors { get; set; } = new List<ArmorDTO>();
        public List<PowerArmorDTO> PowerArmors { get; set; } = new List<PowerArmorDTO>();
    }
}
