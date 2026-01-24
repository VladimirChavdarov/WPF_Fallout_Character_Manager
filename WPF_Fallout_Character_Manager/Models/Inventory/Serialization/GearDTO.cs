using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization;

namespace WPF_Fallout_Character_Manager.Models.Inventory.Serialization
{
    class GearDTO : ItemDTO
    {
        public ModValueDTO<float> LoadEquippedOrFull { get; set; }
        public ModValueDTO<float> LoadUnequippedOrEmpty { get; set; }
        public bool CanBeEquippedOrFilled { get; set; }
        public bool EquippedOrFull { get; set; }
    }
}
