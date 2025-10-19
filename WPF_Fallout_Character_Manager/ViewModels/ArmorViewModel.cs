using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    class ArmorViewModel : ViewModelBase
    {
        // local variables
        private XtrnlArmorModel _xtrnlArmorModel;
        private ArmorModel _armorsModel;

        private Armor _selectedArmor;
        private PowerArmor _selectedPowerArmor;
        //

        // public variables
        public XtrnlArmorModel XtrnlArmorModel
        {
            get => _xtrnlArmorModel;
            set => Update(ref  _xtrnlArmorModel, value);
        }

        public ArmorModel ArmorsModel
        {
            get => _armorsModel;
            set => Update(ref _armorsModel, value);
        }

        public Armor SelectedArmor
        {
            get => _selectedArmor;
            set => Update(ref _selectedArmor, value);
        }

        public PowerArmor SelectedPowerArmor
        {
            get => _selectedPowerArmor;
            set => Update(ref _selectedPowerArmor, value);
        }
        //

        // constructor
        public ArmorViewModel(XtrnlArmorModel xtrnlArmorModel, ArmorModel armorsModel)
        {
            _xtrnlArmorModel = xtrnlArmorModel;
            _armorsModel = armorsModel;

            SelectedArmor = ArmorsModel.Armors.FirstOrDefault();
            SelectedPowerArmor = ArmorsModel.PowerArmors.FirstOrDefault();

            UnequipOtherArmorsCommand = new RelayCommand(UnequipOtherArmors);
            UnequipOtherPowerArmorsCommand = new RelayCommand(UnequipOtherPowerArmors);
        }
        //

        // commands
        public RelayCommand UnequipOtherArmorsCommand { get; private set; }
        private void UnequipOtherArmors(object _ = null)
        {
            ArmorsModel.EquippedArmor = SelectedArmor;
            List<Armor> uneqiuppedArmors = ArmorsModel.Armors.Where(x => x != SelectedArmor).ToList();
            foreach (Armor armor in uneqiuppedArmors)
            {
                armor.Equipped = false;
            }

            List<Armor> equippedArmors = ArmorsModel.Armors.Where(x => x.Equipped == true).ToList();
            if(equippedArmors.Count <= 0)
            {
                ArmorsModel.EquippedArmor = null;
            }
        }

        public RelayCommand UnequipOtherPowerArmorsCommand { get; private set; }
        private void UnequipOtherPowerArmors(object _ = null)
        {
            ArmorsModel.EquippedPowerArmor = SelectedPowerArmor;
            List<PowerArmor> uneqiuppedPowerArmors = ArmorsModel.PowerArmors.Where(x => x != SelectedPowerArmor).ToList();
            foreach (PowerArmor powerArmor in uneqiuppedPowerArmors)
            {
                powerArmor.Equipped = false;
            }

            List<PowerArmor> equippedPowerArmors = ArmorsModel.PowerArmors.Where(x => x.Equipped == true).ToList();
            if (equippedPowerArmors.Count <= 0)
            {
                ArmorsModel.EquippedPowerArmor = null;
            }
        }
        //
    }
}
