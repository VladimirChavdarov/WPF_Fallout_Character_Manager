using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    internal class SurvivalViewModel : ViewModelBase
    {

        // local variables
        private SurvivalModel? _survival;
        private SPECIALModel? _special;

        private Dictionary<string, ModInt> _integerValues;
        
        //

        public SurvivalModel? SurvivalModel
        {
            get { return _survival; }
            set
            {
                _survival = value;
                OnPropertyChanged("SurvivalModel");
            }
        }

        public Dictionary<string, ModInt> IntegerValues
        {
            get { return _integerValues; }
            set
            {
                _integerValues = value;
                OnPropertyChanged("IntegerValues");
            }
        }

        // constructor
        public SurvivalViewModel(SurvivalModel? survival, SPECIALModel? special)
        {
            _survival = survival;
            _special = special;
            _integerValues = _survival.IntegerValues;
        }
        //

        // commands

        //
    }
}
