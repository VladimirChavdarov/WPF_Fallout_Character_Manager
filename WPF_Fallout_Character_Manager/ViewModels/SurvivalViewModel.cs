using System;
using System.Collections.Generic;
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

        // constructor
        public SurvivalViewModel(SurvivalModel? survival)
        {
            _survival = survival;
        }
        //
    }
}
