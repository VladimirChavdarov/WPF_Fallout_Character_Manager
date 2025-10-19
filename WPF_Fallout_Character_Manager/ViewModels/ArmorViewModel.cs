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

            SelectedArmor = XtrnlArmorModel.Armors.FirstOrDefault();
            SelectedPowerArmor = XtrnlArmorModel.PowerArmors.FirstOrDefault();

            UnequipOtherArmorsCommand = new RelayCommand(UnequipOtherArmors);
            UnequipOtherPowerArmorsCommand = new RelayCommand(UnequipOtherPowerArmors);
            AddUpgCommand = new RelayCommand(AddUpg);
            RemoveUpgCommand = new RelayCommand(RemoveUpg);
        }
        //

        // commands
        public RelayCommand UnequipOtherArmorsCommand { get; private set; }
        private void UnequipOtherArmors(object _ = null)
        {
            List<Armor> uneqiuppedArmors = ArmorsModel.Armors.Where(x => x != SelectedArmor).ToList();
            foreach (Armor armor in uneqiuppedArmors)
            {
                armor.Equipped = false;
            }
        }

        public RelayCommand UnequipOtherPowerArmorsCommand { get; private set; }
        private void UnequipOtherPowerArmors(object _ = null)
        {
            List<PowerArmor> uneqiuppedPowerArmors = ArmorsModel.PowerArmors.Where(x => x != SelectedPowerArmor).ToList();
            foreach (PowerArmor powerArmor in uneqiuppedPowerArmors)
            {
                powerArmor.Equipped = false;
            }
        }

        public RelayCommand AddUpgCommand { get; private set; }
        public void AddUpg(object _ = null)
        {
            ArmorUpgrade upg = XtrnlArmorModel.ArmorUpgrades.FirstOrDefault(x => x.Name.Contains("Fitted"));
            SelectedArmor.Upgrades.Add(upg);
        }

        public RelayCommand RemoveUpgCommand { get; private set; }
        public void RemoveUpg(object _ = null)
        {
            SelectedArmor.Upgrades.Clear();
        }
        //
    }
}
