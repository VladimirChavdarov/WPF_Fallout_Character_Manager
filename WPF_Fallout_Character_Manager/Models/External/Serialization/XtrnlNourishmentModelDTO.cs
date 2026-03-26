using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;

namespace WPF_Fallout_Character_Manager.Models.External.Serialization
{
    class XtrnlNourishmentModelDTO
    {
        public List<NourishmentProperty> NourishmentProperties { get; set; } = new List<NourishmentProperty>();

        public List<NourishmentDTO> Nourishments { get; set; } = new List<NourishmentDTO>();
    }
}
