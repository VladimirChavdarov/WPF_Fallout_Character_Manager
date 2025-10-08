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
    class WeaponsModel : ModelBase
    {
        // constructor
        public WeaponsModel()
        {
            Weapons = new ObservableCollection<Weapon>();
        }
        //

        // methods

        //

        // data
        public ObservableCollection<Weapon> Weapons { get; set; }
        //
    }
}
