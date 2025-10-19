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
        private ArmorViewModel _armorsViewModel;
        //

        // public variables
        public WeaponsViewModel WeaponsViewModel
        {
            get => _weaponsViewModel;
            set => Update(ref _weaponsViewModel, value);
        }

        public ArmorViewModel ArmorsViewModel
        {
            get => _armorsViewModel;
            set => Update(ref _armorsViewModel, value);
        }
        //

        // constructor
        public EquippableViewModel(WeaponsViewModel weaponsViewModel, ArmorViewModel armorsViewModel)
        {
            _weaponsViewModel = weaponsViewModel;
            _armorsViewModel = armorsViewModel;
        }
        //
    }
}
