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

            PowerArmor p1 = xtrnlArmorModel.PowerArmors.FirstOrDefault(x => x.Name.BaseValue == "T-45");
            p1.Amount.BaseValue++;
            PowerArmor p2 = xtrnlArmorModel.PowerArmors.FirstOrDefault(x => x.Name.BaseValue == "X-02");
            p2.Amount.BaseValue++;

            PowerArmors.Add(p1);
            PowerArmors.Add(p2);


        }
        //

        // methods

        //

        // data
        public ObservableCollection<Armor> Armors { get; set; }
        public ObservableCollection<PowerArmor> PowerArmors { get; set; }

        private Armor _equippedArmor;
        public Armor EquippedArmor
        {
            get => _equippedArmor;
            set => Update(ref _equippedArmor, value);
        }

        private PowerArmor _equippedPowerArmor;
        public PowerArmor EquippedPowerArmor
        {
            get => _equippedPowerArmor;
            set => Update(ref _equippedPowerArmor, value);
        }
        //
    }
}
