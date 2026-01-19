using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External;

namespace WPF_Fallout_Character_Manager.Models.Serialization
{
    public class LimbConditionsModelDTO
    {
        public List<LimbConditionDTO> LimbConditions { get; set; } = new();
    }
}
