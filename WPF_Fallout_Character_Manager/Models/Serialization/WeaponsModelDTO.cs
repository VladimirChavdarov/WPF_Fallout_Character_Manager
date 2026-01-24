using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;

namespace WPF_Fallout_Character_Manager.Models.Serialization
{
    class WeaponsModelDTO
    {
        public List<WeaponDTO> Weapons { get; set; } = new();
    }
}
