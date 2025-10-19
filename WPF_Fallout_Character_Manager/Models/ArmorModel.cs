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

            Armor a1 = xtrnlArmorModel.Armors.FirstOrDefault(x => x.Name.BaseValue == "Cloth");
            a1.Amount.BaseValue++;
            Armor a2 = xtrnlArmorModel.Armors.FirstOrDefault(x => x.Name.BaseValue == "Steel");
            a2.Amount.BaseValue++;
            Armor a3 = xtrnlArmorModel.Armors.FirstOrDefault(x => x.Name.BaseValue == "Leather");
            a3.Amount.BaseValue++;

            Armors.Add(a1);
            Armors.Add(a2);
            Armors.Add(a3);
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
