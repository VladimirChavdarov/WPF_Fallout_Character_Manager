using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.Models.Serialization
{
    class ConditionsModelDTO
    {
        public List<ConditionDTO> Conditions { get; set; } = new();
    }
}
