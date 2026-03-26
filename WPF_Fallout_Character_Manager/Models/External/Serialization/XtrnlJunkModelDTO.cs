using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;

namespace WPF_Fallout_Character_Manager.Models.External.Serialization
{
    class XtrnlJunkModelDTO
    {
        // We don't have a way to create custom junk components so no reason to serialize this in the catalogue
        //public List<JunkComponent> JunkComponents { get; set; } = new List<JunkComponent>();

        public List<JunkDTO> JunkItems { get; set; } = new List<JunkDTO>();
    }
}
