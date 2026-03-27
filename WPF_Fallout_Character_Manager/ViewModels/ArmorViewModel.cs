using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
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

            _armorsModel.Armors.CollectionChanged += Armors_CollectionChanged;
            _armorsModel.PowerArmors.CollectionChanged += PowerArmors_CollectionChanged;
        }
        //

        // methods
        public void UpdateSelectedArmorsRefs()
        {
            SelectedArmor = ArmorsModel.Armors.FirstOrDefault(x => x.Equipped == true);
            ArmorsModel.EquippedArmor = SelectedArmor;
            SelectedPowerArmor = ArmorsModel.PowerArmors.FirstOrDefault(x => x.Equipped == true);
            ArmorsModel.EquippedPowerArmor = SelectedPowerArmor;
        }

        private void Armors_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach(Armor armor in e.NewItems)
                {
                    armor.PropertyChanged -= Armor_PropertyChanged;
                    armor.PropertyChanged += Armor_PropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (Armor armor in e.OldItems)
                {
                    armor.PropertyChanged -= Armor_PropertyChanged;
                }
            }
        }

        private void Armor_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(Armor.Equipped))
                UnequipOtherArmors(sender);
        }

        private void PowerArmors_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (PowerArmor powerArmor in e.NewItems)
                {
                    powerArmor.PropertyChanged -= PowerArmor_PropertyChanged;
                    powerArmor.PropertyChanged += PowerArmor_PropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (PowerArmor powerArmor in e.OldItems)
                {
                    powerArmor.PropertyChanged -= PowerArmor_PropertyChanged;
                }
            }
        }

        private void PowerArmor_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PowerArmor.Equipped))
                UnequipOtherPowerArmors(sender);
        }
        //

        // commands
        public RelayCommand UnequipOtherArmorsCommand { get; private set; }
        private void UnequipOtherArmors(object obj = null)
        {
            ArmorsModel.EquippedArmor = SelectedArmor;
            List<Armor> uneqiuppedArmors = new List<Armor>();
            if (SelectedArmor != null)
            {
                uneqiuppedArmors = ArmorsModel.Armors.Where(x => x != SelectedArmor).ToList();
            }
            else
            {
                if (obj != null && obj is Armor senderArmor)
                {
                    uneqiuppedArmors = ArmorsModel.Armors.Where(x => x != senderArmor).ToList();
                    SelectedArmor = senderArmor;
                }
            }
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
        private void UnequipOtherPowerArmors(object obj = null)
        {
            ArmorsModel.EquippedPowerArmor = SelectedPowerArmor;
            List<PowerArmor> uneqiuppedPowerArmors = new List<PowerArmor>();
            if (SelectedPowerArmor != null)
            {
                uneqiuppedPowerArmors = ArmorsModel.PowerArmors.Where(x => x != SelectedPowerArmor).ToList();
            }
            else
            {
                if (obj != null && obj is PowerArmor senderPowerArmor)
                {
                    uneqiuppedPowerArmors = ArmorsModel.PowerArmors.Where(x => x != senderPowerArmor).ToList();
                    SelectedPowerArmor = senderPowerArmor;
                }
            }
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
