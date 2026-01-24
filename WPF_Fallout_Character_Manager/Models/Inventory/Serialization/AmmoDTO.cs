using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.Models.Inventory.Serialization
{
    class AmmoDTO : ItemDTO
    {
        public string Type { get;set; }
        public List<Guid> EffectIds { get; set; } = new();
    }
}
