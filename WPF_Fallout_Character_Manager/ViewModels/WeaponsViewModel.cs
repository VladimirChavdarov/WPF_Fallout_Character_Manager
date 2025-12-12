using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    class WeaponsViewModel : ViewModelBase
    {
        // local variables
        private XtrnlWeaponsModel _xtrnlWeaponsModel;
        private WeaponsModel _weaponsModel;
        private XtrnlAmmoModel _xtrnlAmmoModel;
        private AmmoModel _ammoModel;
        private SkillModel _skillModel;

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

        public XtrnlAmmoModel XtrnlAmmoModel
        {
            get => _xtrnlAmmoModel;
            set => Update(ref _xtrnlAmmoModel, value);
        }

        public AmmoModel AmmoModel
        {
            get => _ammoModel;
            set => Update(ref _ammoModel, value);
        }

        public SkillModel SkillModel
        {
            get => _skillModel;
            set => Update(ref _skillModel, value);
        }

        public Weapon SelectedWeapon
        {
            get => _selectedWeapon;
            set
            {
                Update(ref _selectedWeapon, value);
                SelectedAmmo = SelectedWeapon.CompatibleAmmos.FirstOrDefault();
                SetToHitBaseValue(SelectedWeapon);
            }
        }

        public Ammo SelectedAmmo
        {
            get => _selectedAmmo;
            set => Update(ref _selectedAmmo, value);
        }
        //

        // constructor
        public WeaponsViewModel(XtrnlWeaponsModel xtrnlWeaponsModel, WeaponsModel weaponsModel, XtrnlAmmoModel xtrnlAmmoModel, AmmoModel ammoModel, SkillModel skillModel)
        {
            _xtrnlWeaponsModel = xtrnlWeaponsModel;
            _weaponsModel = weaponsModel;
            _xtrnlAmmoModel = xtrnlAmmoModel;
            _ammoModel = ammoModel;
            _skillModel = skillModel;

            ShootCommand = new RelayCommand(Shoot);
            ReloadCommand = new RelayCommand(Reload);
            UnequipOtherWeaponsCommand = new RelayCommand(UnequipOtherWeapons);

            _skillModel.PropertyChanged += SkillModel_PropertyChanged;

            _ammoModel.Ammos.CollectionChanged += AmmoModel_Ammos_CollectionChanged;

            // testing code
            Weapon xtrnlW1 = xtrnlWeaponsModel.Weapons.FirstOrDefault(x => x.Name.BaseValue == "Assault Rifle");
            Weapon w1 = xtrnlW1.Clone();
            w1.Amount.BaseValue = 5;
            w1.Name.BaseValue = "This is my custom assault rifle now";
            w1.Equipped = true;
            WeaponUpgrade wu1 = xtrnlWeaponsModel.WeaponUpgrades.FirstOrDefault(x => x.Name == "Bayonet");
            w1.Upgrades.Add(wu1);
            WeaponsModel.Weapons.Add(w1);

            Ammo xtrnlA1 = xtrnlAmmoModel.Ammos.FirstOrDefault(x => x.Name.BaseValue == ".308");
            Ammo a1 = xtrnlA1.Clone();
            a1.Amount.BaseValue = 50;
            AmmoEffect ae1 = xtrnlAmmoModel.AmmoEffects.FirstOrDefault(x => x.Name == "Match");
            a1.Effects.Add(ae1);
            a1.Amount.BaseValue += 10;
            AmmoModel.Ammos.Add(a1);
            //

            SelectedWeapon = WeaponsModel.Weapons.FirstOrDefault();
            SelectedAmmo = SelectedWeapon.CompatibleAmmos.FirstOrDefault();
        }
        //

        // methods
        private void SkillModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SetToHitBaseValue(SelectedWeapon);
        }

        private void AmmoModel_Ammos_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach(Weapon weapon in WeaponsModel.Weapons)
            {
                weapon.CompatibleAmmos.Clear();
                foreach(Ammo ammo in AmmoModel.Ammos)
                {
                    if(ammo.Type == weapon.AmmoType)
                    {
                        weapon.CompatibleAmmos.Add(ammo);
                    }
                }
            }
        }

        private static void ReloadWeapon(Weapon weapon, Ammo ammo)
        {
            int availableAttacks = (int)(ammo.Amount.BaseValue / weapon.AmmoPerAttack.BaseValue);
            int ammoAmountToAdd = Math.Clamp(availableAttacks, 0, weapon.NumberOfAttacks.BaseValue);
            for(int i = 0; i < ammoAmountToAdd; i++)
            {
                weapon.BulletSlots[i] = true;
            }
        }

        private void SetToHitBaseValue(Weapon weapon)
        {
            switch (weapon.Type)
            {
                case "Bladed":
                    SetToHitBaseValue(Skill.MeleeWeapons);
                    break;
                case "Blunt":
                    SetToHitBaseValue(Skill.MeleeWeapons);
                    break;
                case "Mechanical":
                    SetToHitBaseValue(Skill.MeleeWeapons);
                    break;

                case "Unarmed":
                    SetToHitBaseValue(Skill.Unarmed);
                    break;

                case "Handgun":
                    SetToHitBaseValue(Skill.Guns);
                    break;
                case "Revolver":
                    SetToHitBaseValue(Skill.Guns);
                    break;
                case "Submachine Gun":
                    SetToHitBaseValue(Skill.Guns);
                    break;
                case "Rifle":
                    SetToHitBaseValue(Skill.Guns);
                    break;
                case "Shotgun":
                    SetToHitBaseValue(Skill.Guns);
                    break;
                case "Big Gun":
                    SetToHitBaseValue(Skill.Guns);
                    break;

                case "Energy":
                    SetToHitBaseValue(Skill.EnergyWeapons);
                    break;
            }
        }

        private void SetToHitBaseValue(Skill skill)
        {
            SetToHitBaseValue(SkillModel.GetSkill(skill).Total);
        }

        private void SetToHitBaseValue(int skillModifier)
        {
            SelectedWeapon.ToHit.BaseValue = skillModifier;
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

        public RelayCommand UnequipOtherWeaponsCommand {  get; private set; }
        private void UnequipOtherWeapons(object _ = null)
        {
            List<Weapon> uneqiuppedWeapons = WeaponsModel.Weapons.Where(x => x != SelectedWeapon).ToList();
            foreach(Weapon weapon in uneqiuppedWeapons)
            {
                weapon.Equipped = false;
            }
        }
        //
    }
}
