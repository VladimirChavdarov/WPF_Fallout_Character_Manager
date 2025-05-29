using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    internal class BioViewModel : ViewModelBase
    {
        // local variables
        private BioModel? _bioModel;
        //

        public BioModel? BioModel
        {
            get { return _bioModel; }
            set
            {
                _bioModel = value;
                OnPropertyChanged("BioModel");
            }
        }

        // constructor
        public BioViewModel(BioModel? bioModel)
        {
            _bioModel = bioModel;
        }

        //

        
    }
}
