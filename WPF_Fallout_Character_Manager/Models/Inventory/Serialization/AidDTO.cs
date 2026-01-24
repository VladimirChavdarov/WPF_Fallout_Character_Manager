using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;

namespace WPF_Fallout_Character_Manager.Models.Inventory.Serialization
{
    class AidDTO : ItemDTO
    {
        public LabeledString Description { get; set; }
        public List<Guid> PropertiesIds { get; set; } = new();
    }
}
