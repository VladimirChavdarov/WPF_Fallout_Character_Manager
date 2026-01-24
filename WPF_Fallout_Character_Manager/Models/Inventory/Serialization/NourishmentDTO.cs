using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.Models.Inventory.Serialization
{
    class NourishmentDTO : ItemDTO
    {
        public List<Guid> PropertyIds { get; set; } = new();
    }
}
