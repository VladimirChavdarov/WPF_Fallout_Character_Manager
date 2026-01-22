using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;
using WPF_Fallout_Character_Manager.ViewModels;

namespace WPF_Fallout_Character_Manager.Models.External.Serialization
{
    class XtrnlWeaponsModelDTO
    {
        // These don't need DTOs because they're simple enough
        public List<WeaponProperty> Properties { get; set; } = new();
        public List<WeaponUpgrade> Upgrades { get; set; } = new();

        public List<WeaponDTO> Weapons { get; set; } = new();
    }
}
