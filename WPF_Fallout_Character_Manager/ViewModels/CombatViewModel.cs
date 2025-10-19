using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    class CombatViewModel : ViewModelBase
    {
        // local variables
        private CombatModel _combat;
        private SPECIALModel _special;
        private BioModel _bio;
        private ArmorModel? _armorModel;
        //

        // public variables
        public CombatModel CombatModel
        {
            get { return _combat; }
            set
            {
                _combat = value;
                Update(ref _combat, value);
            }
        }

        public SPECIALModel SPECIALModel
        {
            get { return _special; }
            set
            {
                _special = value;
                Update(ref _special, value);
            }
        }

        public ArmorModel ArmorModel
        {
            get => _armorModel;
            set => Update(ref _armorModel, value);
        }
        //

        public CombatViewModel(CombatModel? combat, SPECIALModel? special, BioModel? bio, ArmorModel armorModel)
        {
            _combat = combat;
            _special = special;
            _bio = bio;
            _armorModel = armorModel;

            _special.PropertyChanged += SPECIALModel_PropertyChanged;
            _bio.PropertyChanged += BioModel_PropertyChanged;
            _armorModel.PropertyChanged += ArmorModel_PropertyChanged;

            CombatModel.UpdateModel(_special, _bio.Level.Total);
        }

        private void SPECIALModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SPECIALModel.Endurance))
            {
                _combat.CalculateHealingRate(_special.Endurance.Total, _bio.Level.Total);
                _combat.CalculateMaxHealthPoints(_special.GetModifier(SPECIAL.Endurance), _bio.Level.Total);
            }
            if(e.PropertyName == nameof(SPECIALModel.Agility))
            {
                _combat.CalculateActionPoints(_special.GetModifier(SPECIAL.Agility));
                _combat.CalculateMaxStaminaPoints(_special.GetModifier(SPECIAL.Agility), _bio.Level.Total);
            }
            if (e.PropertyName == nameof(SPECIALModel.Perception))
            {
                _combat.CalculateCombatSequence(_special.GetModifier(SPECIAL.Perception));
            }
        }

        private void BioModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            CombatModel.UpdateModel(_special, _bio.Level.Total);
        }

        private void ArmorModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_armorModel.EquippedArmor))
            {
                CombatModel.ArmorClass.BaseValue = 10;
                CombatModel.DamageThreshold.BaseValue = 0;

                if (ArmorModel.EquippedArmor != null)
                {
                    CombatModel.ArmorClass.BaseValue = _armorModel.EquippedArmor.AC.BaseValue;
                    CombatModel.DamageThreshold.BaseValue = _armorModel.EquippedArmor.DT.BaseValue;
                }

                if(ArmorModel.EquippedPowerArmor != null)
                {
                    CombatModel.ArmorClass.BaseValue = _armorModel.EquippedPowerArmor.AC.BaseValue;
                    CombatModel.DamageThreshold.BaseValue = 0;
                }
            }

            if(e.PropertyName == nameof(_armorModel.EquippedPowerArmor))
            {
                CombatModel.ArmorClass.BaseValue = 10;
                CombatModel.DamageThreshold.BaseValue = 0;

                if(ArmorModel.EquippedPowerArmor != null)
                {
                    CombatModel.ArmorClass.BaseValue = _armorModel.EquippedPowerArmor.AC.BaseValue;
                }

                if(ArmorModel.EquippedArmor != null && ArmorModel.EquippedPowerArmor == null)
                {
                    CombatModel.ArmorClass.BaseValue = _armorModel.EquippedArmor.AC.BaseValue;
                    CombatModel.DamageThreshold.BaseValue = _armorModel.EquippedArmor.DT.BaseValue;
                }
            }
        }
    }
}
