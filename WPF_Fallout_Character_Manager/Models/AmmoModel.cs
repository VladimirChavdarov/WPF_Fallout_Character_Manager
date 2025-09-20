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
    class AmmoModel : ModelBase
    {
        // constructor
        public AmmoModel()
        {
            Ammos = new ObservableCollection<Ammo>();
        }
        //

        // data
        public ObservableCollection<Ammo> Ammos { get; set; }
        //
    }
}
