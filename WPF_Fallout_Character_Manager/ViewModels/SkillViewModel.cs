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
    class SkillViewModel : ViewModelBase
    {
        // local variables
        private SkillModel _skill;
        private SPECIALModel _special;
        SPECIAL _breachScaling;
        SPECIAL _intimidationScaling;
        SPECIAL _medicineScaling;
        //

        public SkillModel SkillModel
        {
            get { return _skill; }
            set
            {
                _skill = value;
                Update(ref _skill, value);
            }
        }

        public SPECIALModel Special
        {
            get { return _special; }
            set
            {
                _special = value;
                Update(ref _special, value);
            }
        }

        public SPECIAL BreachScaling
        {
            get { return _breachScaling; }
            set
            {
                _breachScaling = value;
                Update(ref _breachScaling, value);
            }
        }

        public SPECIAL IntimidadtionScaling
        {
            get { return _intimidationScaling; }
            set
            {
                _intimidationScaling = value;
                Update(ref _intimidationScaling, value);
            }
        }

        public SPECIAL MedicineScaling
        {
            get { return _medicineScaling; }
            set
            {
                _medicineScaling = value;
                Update(ref _medicineScaling, value);
            }
        }

        public SkillViewModel(SkillModel? skill, SPECIALModel? special)
        {
            _skill = skill;
            _special = special;

            BreachScaling = SPECIAL.Perception;
            IntimidadtionScaling = SPECIAL.Charisma;
            MedicineScaling = SPECIAL.Intelligence;

            special.PropertyChanged += SPECIALModel_PropertyChanged;

            SkillModel.UpdateModel(_special);
        }

        private void SPECIALModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName ==  nameof(SPECIALModel.Strength))
            {
                if(_intimidationScaling == SPECIAL.Strength)
                {
                    _skill.CalculateIntimidation();
                }
            }
        }

    }
}
