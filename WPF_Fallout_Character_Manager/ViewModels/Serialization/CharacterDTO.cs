using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.Serialization;

namespace WPF_Fallout_Character_Manager.ViewModels.Serialization
{
    sealed class CharacterDTO
    {
        public float Version { get; set; } = 1.1f;

        public Dictionary<string, JsonElement> ModelDtos { get; set; } = new();
        //public BioModelDTO BioModelDto { get; set; }
        //public SurvivalModelDTO SurvivalModelDto { get; set; }
    }
}
