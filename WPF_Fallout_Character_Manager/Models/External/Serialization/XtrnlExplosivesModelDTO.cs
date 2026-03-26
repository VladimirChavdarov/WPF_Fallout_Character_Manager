using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;

namespace WPF_Fallout_Character_Manager.Models.External.Serialization
{
    class XtrnlExplosivesModelDTO
    {
        public List<ExplosiveProperty> ExplosiveProperties { get; set; } = new List<ExplosiveProperty>();

        public List<ExplosiveDTO> Explosives { get; set; } = new List<ExplosiveDTO>();
    }
}
