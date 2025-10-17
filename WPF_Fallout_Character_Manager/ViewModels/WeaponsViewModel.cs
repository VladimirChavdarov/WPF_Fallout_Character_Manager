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
            set
            {
                Update(ref _selectedWeapon, value);
                SelectedAmmo = SelectedWeapon.CompatibleAmmos.FirstOrDefault();
            }
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

            ShootCommand = new RelayCommand(Shoot);
            ReloadCommand = new RelayCommand(Reload);
        }
        //

        // methods
        private static void ReloadWeapon(Weapon weapon, Ammo ammo)
        {
            int availableAttacks = (int)(ammo.Amount.BaseValue / weapon.AmmoPerAttack.BaseValue);
            int ammoAmountToAdd = Math.Clamp(availableAttacks, 0, weapon.NumberOfAttacks.BaseValue);
            for(int i = 0; i < ammoAmountToAdd; i++)
            {
                weapon.BulletSlots[i] = true;
            }
        }
        //

        // commands
        public RelayCommand ShootCommand { get; private set; }
        private void Shoot(object _ = null)
        {
            if(SelectedAmmo == null)
            {
                return;
            }

            if(SelectedAmmo.Amount.BaseValue < SelectedWeapon.AmmoPerAttack.BaseValue)
            {
                return; // doesn't have enough ammo to shoot
            }

            if(SelectedWeapon.BulletSlots.Count(x => x) < 1)
            {
                return; // there are not enough bullets in the chamber to make the attack
            }

            int lastBulletIndex = -1;
            for(int i = SelectedWeapon.BulletSlots.Count - 1;  i >= 0; i--)
            {
                if (SelectedWeapon.BulletSlots[i] == true)
                {
                    lastBulletIndex = i;
                    break;
                }
            }
            SelectedWeapon.BulletSlots[lastBulletIndex] = false;
            SelectedWeapon.UsedAmmoFirepower += SelectedWeapon.AmmoPerAttack.BaseValue;

            if(SelectedWeapon.UsedAmmoFirepower >= 1.0f)
            {
                SelectedAmmo.Amount.BaseValue -= (int) SelectedWeapon.UsedAmmoFirepower;
                SelectedWeapon.UsedAmmoFirepower = 0.0f;

            }
            OnPropertyChanged(nameof(SelectedAmmo));
        }

        public RelayCommand ReloadCommand { get; private set; }
        private void Reload(object _ = null)
        {
            if (SelectedAmmo == null)
            {
                return;
            }

            ReloadWeapon(SelectedWeapon, SelectedAmmo);
        }
        //
    }
}
