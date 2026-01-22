using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization;

namespace WPF_Fallout_Character_Manager.Models.Inventory.Serialization
{
    class WeaponDTO : ItemDTO
    {
        public string Type { get; set; }
        public ModValueDTO<int> AP { get; set; }
        public ModValueDTO<int> ToHit { get; set; }
        public ModValueDTO<string> Damage { get; set; }
        public ModValueDTO<string> RangeMultiplier { get; set; }
        public ModValueDTO<int> CritChance { get; set; }
        public ModValueDTO<string> CritDamage { get; set; }
        public string AmmoType { get; set; }
        public ModValueDTO<int> AmmoCapacity { get; set; }
        public ModValueDTO<float> AmmoPerAttack { get; set; }
        public ModValueDTO<int> NumberOfAttacks { get; set; }
        public float UsedAmmoFirepower { get; set; }
        public ModValueDTO<int> StrRequirement { get; set; }

        public ModValueDTO<int> Decay { get; set; }
        public WeaponType WeaponType { get; set; }
        public ModValueDTO<int> AvailableUpgradeSlots { get; set; }
        public ModValueDTO<int> TakenUpgradeSlots { get; set; }
        public bool Equipped { get; set; }
        public List<Guid> PropertyIds { get; set; } = new();
        public List<Guid> UpgradeIds { get; set; } = new();
    }
}
