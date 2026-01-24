using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization;

namespace WPF_Fallout_Character_Manager.Models.Inventory.Serialization
{
    class ExplosiveDTO : ItemDTO
    {
        public string Type { get; set; }
        public ModValueDTO<int> AP { get; set; }
        public ModValueDTO<string> Damage { get; set; }
        public ModValueDTO<int> ArmDC { get; set; }
        public ModValueDTO<string> Range{ get; set; }
        public ModValueDTO<string> AreaOfEffect{ get; set; }
        public List<Guid> PropertyIds { get; set; } = new();
    }
}
