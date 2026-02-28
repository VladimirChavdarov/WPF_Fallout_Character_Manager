using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    internal class SPECIALViewModel : ViewModelBase
    {
        // local variables
        private SPECIALModel? _special;
        private ArmorModel? _armorModel; 
        //

        // public variables
        public SPECIALModel? SPECIALModel
        {
            get { return _special; }
            set
            {
                _special = value;
                Update(ref _special, value);
            }
        }

        public static string PowerArmorModifierName = "Power Armor";
        //

        // constructor
        public SPECIALViewModel(SPECIALModel? special, ArmorModel armorModel)
        {
            _special = special;
            _armorModel = armorModel;

            _armorModel.PropertyChanged += ArmorModel_PropertyChanged;
        }
        //

        private void ArmorModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_armorModel.EquippedPowerArmor))
            {
                var modifierToRemove = _special.Strength.Modifiers.FirstOrDefault(x => x.Name == PowerArmorModifierName);
                if (modifierToRemove != null)
                {
                    modifierToRemove.IsReadOnly = false;
                    _special.Strength.RemoveModifier(modifierToRemove);
                    _special.Strength.IsBaseValueReadOnly = false;
                }

                if (_armorModel.EquippedPowerArmor != null)
                {
                    int modifierValue = 12 - _special.Strength.Total;
                    _special.Strength.AddModifier(new LabeledInt(PowerArmorModifierName, modifierValue, "Hydraulic Machine: Your Strength ability score is considered 12 and your size is Large.", true));
                    _special.Strength.IsBaseValueReadOnly = true;
                }
            }
        }
    }
}
