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
        //

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

        public CombatViewModel(CombatModel? combat, SPECIALModel? special, BioModel? bio)
        {
            _combat = combat;
            _special = special;
            _bio = bio;

            _special.PropertyChanged += SPECIALModel_PropertyChanged;
            _bio.PropertyChanged += BioModel_PropertyChanged;

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
    }
}
