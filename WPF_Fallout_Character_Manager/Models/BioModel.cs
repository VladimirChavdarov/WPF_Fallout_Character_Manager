using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.Models
{
    internal sealed class BioModel
    {
        public BioModel()
        {
            Name = "none";
            Race = "No Race";
            Background = "No Background";
            Backstory = "No Backstory";
            Level = 0;
            XP = 0;
            ImageSource = "Resources/Vault_Boy.png";
        }

        public string Name { get; set; }
        public string Race { get; set; }
        public string Background { get; set; }
        public string Backstory { get; set; }
        public int Level { get; set; }
        public int XP { get; set; }
        public string ImageSource { get; set; }
    }
}
