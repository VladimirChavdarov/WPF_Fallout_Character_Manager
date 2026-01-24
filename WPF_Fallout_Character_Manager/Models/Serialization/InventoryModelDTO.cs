using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization;

namespace WPF_Fallout_Character_Manager.Models.Serialization
{
    class InventoryModelDTO
    {
        public ModValueDTO<int> Caps { get; set; }
        public ModValueDTO<float> CarryLoad { get; set; }
        public ModValueDTO<float> CurrentLoad { get; set; }

        public List<AidDTO> AidItems { get; set; } = new();
        public List<ExplosiveDTO> Explosives { get; set; } = new();
    }
}
