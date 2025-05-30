using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels.ModifierSystem
{
    public class ModIntViewModel : ViewModelBase
    {
        // local variables
        private ModInt? _modInt;
        //

        public ModInt? ModInt
        { 
            get { return _modInt; }
            set
            {
                _modInt = value;
                OnPropertyChanged("ModInt");
            }
        }

        //constructor
        public ModIntViewModel(ModInt modInt)
        {
            _modInt = modInt;
        }
        //
    }
}
