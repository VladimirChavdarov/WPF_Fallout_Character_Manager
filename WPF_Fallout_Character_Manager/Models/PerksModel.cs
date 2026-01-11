using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models
{
    class PerksModel : ModelBase
    {
        public PerksModel()
        {
            Traits = new ObservableCollection<Trait>();
            Perks = new ObservableCollection<Perk>();
        }

        public ObservableCollection<Trait> Traits { get; set; }
        public ObservableCollection<Perk> Perks { get; set; }
    }
}
