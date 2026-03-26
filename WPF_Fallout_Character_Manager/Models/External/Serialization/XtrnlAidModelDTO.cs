using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;

namespace WPF_Fallout_Character_Manager.Models.External.Serialization
{
    class XtrnlAidModelDTO
    {
        public List<AidProperty> AidProperties { get; set; } = new List<AidProperty>();

        public List<AidDTO> AidItems { get; set; } = new List<AidDTO>();
    }
}
