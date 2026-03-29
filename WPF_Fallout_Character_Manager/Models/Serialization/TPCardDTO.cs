using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization;

namespace WPF_Fallout_Character_Manager.Models.Serialization
{
    public class TPCardDTO
    {
        public LabeledString BaseObject { get; set; }
        public bool IsFromSpreadsheet { get; set; }
        public string Prerequisite { get; set; }
        public string ExtraDscription { get; set; }
        public string CardType { get; set; }
        public string ImagePath { get; set; }
        public bool ShowDescription { get; set; }
    }

    public class TraitDTO : TPCardDTO
    {
        public bool IsWildWastelandToggled { get; set; }
    }

    public class PerkDTO : TPCardDTO
    {
        public ModValueDTO<int> CurrentStacks { get; set; }
        public ModValueDTO<int> MaxStacks { get; set; }
    }
}
