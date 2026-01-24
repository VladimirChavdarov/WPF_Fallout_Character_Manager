using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Models.Serialization;

namespace WPF_Fallout_Character_Manager.Models
{
    class ArmorModel : ModelBase, ISerializable<ArmorModelDTO>
    {
        // constructor
        public ArmorModel(XtrnlArmorModel xtrnlArmorModel)
        {
            Armors = new ObservableCollection<Armor>();
            PowerArmors = new ObservableCollection<PowerArmor>();

            // testing
            //Armor a1 = xtrnlArmorModel.Armors.FirstOrDefault(x => x.Name.BaseValue == "Cloth").Clone();
            //a1.Amount.BaseValue++;
            //Armor a2 = xtrnlArmorModel.Armors.FirstOrDefault(x => x.Name.BaseValue == "Steel").Clone();
            //a2.Amount.BaseValue++;
            //Armor a3 = xtrnlArmorModel.Armors.FirstOrDefault(x => x.Name.BaseValue == "Leather").Clone();
            //a3.Amount.BaseValue++;

            //a2.Decay.BaseValue = 5;
            //ArmorUpgrade au2 = xtrnlArmorModel.ArmorUpgrades.FirstOrDefault(x => x.Name.Contains("Fitted"));
            //a2.Upgrades.Add(au2);

            //Armors.Add(a1);
            //Armors.Add(a2);
            //Armors.Add(a3);


            //PowerArmor p1 = xtrnlArmorModel.PowerArmors.FirstOrDefault(x => x.Name.BaseValue == "T-45").Clone();
            //p1.Amount.BaseValue++;
            //PowerArmor p2 = xtrnlArmorModel.PowerArmors.FirstOrDefault(x => x.Name.BaseValue == "X-02").Clone();
            //p2.Amount.BaseValue++;

            //PowerArmors.Add(p1);
            //PowerArmors.Add(p2);
            //

        }
        //

        // methods
        public ArmorModelDTO ToDto()
        {
            ArmorModelDTO result = new ArmorModelDTO();

            foreach (Armor armor in Armors)
            {
                if (armor.ToDto() is not ArmorDTO aDto)
                    throw new InvalidOperationException("Expected ArmorDTO");

                result.Armors.Add(aDto);
            }

            foreach (PowerArmor armor in PowerArmors)
            {
                if (armor.ToDto() is not PowerArmorDTO apDto)
                    throw new InvalidOperationException("Expected PowerArmorDTO");

                result.PowerArmors.Add(apDto);
            }

            return result;
        }

        public void FromDto(ArmorModelDTO dto, bool versionMismatch = false)
        {
            Armors.Clear();
            PowerArmors.Clear();

            foreach(ArmorDTO aDto in dto.Armors)
            {
                Armors.Add(new Armor(aDto));
            }

            foreach (PowerArmorDTO paDto in dto.PowerArmors)
            {
                PowerArmors.Add(new PowerArmor(paDto));
            }
        }
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
