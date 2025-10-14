using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    class EquippableViewModel : ViewModelBase
    {
        // local variables
        private WeaponsViewModel _weaponsViewModel;
        //

        // public variables
        public WeaponsViewModel WeaponsViewModel
        {
            get => _weaponsViewModel;
            set => Update(ref _weaponsViewModel, value);
        }
        //

        // constructor
        public EquippableViewModel(WeaponsViewModel weaponsViewModel)
        {
            _weaponsViewModel = weaponsViewModel;
        }
        //
    }
}
