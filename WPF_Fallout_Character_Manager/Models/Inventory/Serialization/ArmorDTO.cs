using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization;

namespace WPF_Fallout_Character_Manager.Models.Inventory.Serialization
{
    class ArmorDTO : ItemDTO
    {
        public ModValueDTO<int> AC { get; set; }
        public ModValueDTO<int> DT { get; set; }
        public ModValueDTO<int> AvailableUpgradeSlots { get; set; }
        public ModValueDTO<int> TakenUpgradeSlots { get; set; }
        public ModValueDTO<int> StrRequirement { get; set; }
        public ModValueDTO<int> Decay { get; set; }
        public bool Equipped { get; set; }
        public List<Guid> UpgradeIds { get; set; } = new();
    }

    class PowerArmorDTO : ItemDTO
    {
        public ModValueDTO<int> AC { get; set; }
        public ModValueDTO<int> DP { get; set; }
        public ModValueDTO<int> AvailableUpgradeSlots { get; set; }
        public ModValueDTO<int> TakenUpgradeSlots { get; set; }
        public ModValueDTO<int> RepairDC { get; set; }
        public ModValueDTO<int> AllottedTime { get; set; }
        public ModValueDTO<int> Decay { get; set; }
        public bool Equipped { get; set; }
        public List<Guid> UpgradeIds { get; set; } = new();
    }
}
