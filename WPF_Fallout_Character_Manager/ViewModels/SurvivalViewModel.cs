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
    internal class SurvivalViewModel : ViewModelBase
    {

        // local variables
        private SurvivalModel? _survival;
        private SPECIALModel? _special;
        //

        public SurvivalModel? SurvivalModel
        {
            get { return _survival; }
            set
            {
                _survival = value;
                Update(ref _survival, value);
            }
        }

        // constructor
        public SurvivalViewModel(SurvivalModel? survival, SPECIALModel? special)
        {
            _survival = survival;
            _special = special;

            _special.PropertyChanged += SPECIALModel_PropertyChanged;

            // update once on initialize
            SurvivalModel.UpdateModel(_special);
        }

        private void SPECIALModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SPECIALModel.Endurance) ||
                e.PropertyName == nameof(SPECIALModel.Perception))
            {
                _survival.UpdateModel(_special);
            }
        }
        //
    }
}
