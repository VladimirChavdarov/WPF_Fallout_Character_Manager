using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.ViewModels.Serialization
{
    sealed class CatalogueDTO
    {
        public float Version { get; set; } = 1.1f;
        public Dictionary<string, JsonElement> XtrnlModelDtos { get; set; } = new();

    }
}
