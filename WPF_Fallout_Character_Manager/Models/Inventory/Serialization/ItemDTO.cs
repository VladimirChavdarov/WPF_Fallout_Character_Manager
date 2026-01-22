using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization;

namespace WPF_Fallout_Character_Manager.Models.Inventory.Serialization
{
    class ItemDTO
    {
        public ModValueDTO<string> Name { get; set; }
        public ModValueDTO<int> Cost { get; set; }
        public ModValueDTO<int> Amount { get; set; }
        public ModValueDTO<float> Load { get; set; }
        public bool CanBeEdited { get; set; }
    }
}
