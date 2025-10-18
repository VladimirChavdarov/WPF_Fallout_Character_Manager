﻿using System;
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
        public WeaponsViewModel(XtrnlWeaponsModel xtrnlWeaponsModel, WeaponsModel weaponsModel, SkillModel skillModel)
        {
            _xtrnlWeaponsModel = xtrnlWeaponsModel;
            _weaponsModel = weaponsModel;
            _skillModel = skillModel;

            SelectedWeapon = WeaponsModel.Weapons.FirstOrDefault();
            SelectedAmmo = SelectedWeapon.CompatibleAmmos.FirstOrDefault();

            ShootCommand = new RelayCommand(Shoot);
            ReloadCommand = new RelayCommand(Reload);
            UnequipOtherWeaponsCommand = new RelayCommand(UnequipOtherWeapons);

            _skillModel.PropertyChanged += SkillModel_PropertyChanged;
        }

        private void SkillModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SetToHitBaseValue(SelectedWeapon);
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

        private void SetToHitBaseValue(Weapon weapon)
        {
            switch (weapon.Type.BaseValue)
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
