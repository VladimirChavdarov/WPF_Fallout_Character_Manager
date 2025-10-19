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
    class ArmorModel : ModelBase
    {
        // constructor
        public ArmorModel(XtrnlArmorModel xtrnlArmorModel)
        {
            Armors = new ObservableCollection<Armor>();
            PowerArmors = new ObservableCollection<PowerArmor>();


        }
        //

        // methods

        //

        // data
        public ObservableCollection<Armor> Armors { get; set; }
        public ObservableCollection<PowerArmor> PowerArmors { get; set; }
        //
    }
}
