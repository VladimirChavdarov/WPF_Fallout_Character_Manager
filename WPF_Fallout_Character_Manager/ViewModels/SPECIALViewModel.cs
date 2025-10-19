using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    internal class SPECIALViewModel : ViewModelBase
    {
        // local variables
        private SPECIALModel? _special;
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
        //

        // constructor
        public SPECIALViewModel(SPECIALModel? special)
        {
            _special = special;
        }
        //
    }
}
