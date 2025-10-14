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
        public WeaponsModel(XtrnlWeaponsModel xtrnlWeaponsModel)
        {
            Weapons = new ObservableCollection<Weapon>();

            Weapon w1 = xtrnlWeaponsModel.Weapons.FirstOrDefault(x => x.Name.BaseValue == "Assault Rifle").Clone();
            w1.Amount.BaseValue++;
            Weapon w2 = xtrnlWeaponsModel.Weapons.FirstOrDefault(x => x.Name.BaseValue == "5.56mm pistol").Clone();
            w2.Amount.BaseValue++;
            Weapon w3 = xtrnlWeaponsModel.Weapons.FirstOrDefault(x => x.Name.BaseValue == "Crowbar").Clone();
            w3.Amount.BaseValue++;

            Weapons.Add(w1);
            Weapons.Add(w2);
            Weapons.Add(w3);
        }
        //

        // methods

        //

        // data
        public ObservableCollection<Weapon> Weapons { get; set; }
        //
    }
}
