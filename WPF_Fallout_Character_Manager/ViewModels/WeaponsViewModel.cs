using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    class WeaponsViewModel : ViewModelBase
    {
        // local variables
        private XtrnlWeaponsModel _xtrnlWeaponsModel;
        private WeaponsModel _weaponsModel;
        private Weapon _selectedWeapon;
        private Ammo _selectedAmmo;
        //

        // public variables
        public XtrnlWeaponsModel XtrnlWeaponsModel
        {
            get => _xtrnlWeaponsModel;
            set => Update(ref _xtrnlWeaponsModel, value);
        }

        public WeaponsModel WeaponsModel
        {
            get => _weaponsModel;
            set => Update(ref _weaponsModel, value);
        }

        public Weapon SelectedWeapon
        {
            get => _selectedWeapon;
            set => Update(ref _selectedWeapon, value);
        }

        public Ammo SelectedAmmo
        {
            get => _selectedAmmo;
            set => Update(ref _selectedAmmo, value);
        }
        //

        // constructor
        public WeaponsViewModel(XtrnlWeaponsModel xtrnlWeaponsModel, WeaponsModel weaponsModel)
        {
            _xtrnlWeaponsModel = xtrnlWeaponsModel;
            _weaponsModel = weaponsModel;

            SelectedWeapon = WeaponsModel.Weapons.FirstOrDefault();
            SelectedAmmo = SelectedWeapon.CompatibleAmmos.FirstOrDefault();
        }
        //
    }
}
