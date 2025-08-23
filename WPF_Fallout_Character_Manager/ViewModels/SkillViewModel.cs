using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    class SkillViewModel : ViewModelBase
    {
        // local variables
        private SkillModel _skill;
        private SPECIALModel _special;

        // The SPECIAL stat with which the skill currently scales. Changing a value in this ObservableCollection should trigger SkillModel.CalculateSkill
        ObservableCollection<SPECIAL> SelectedSkillModifier;
        //

        public SkillModel SkillModel
        {
            get { return _skill; }
            set
            {
                _skill = value;
                Update(ref _skill, value);
            }
        }

        public SPECIALModel Special
        {
            get { return _special; }
            set
            {
                _special = value;
                Update(ref _special, value);
            }
        }

        public SkillViewModel(SkillModel? skill, SPECIALModel? special)
        {
            _skill = skill;
            _special = special;

            special.PropertyChanged += SPECIALModel_PropertyChanged;

            SkillModel.UpdateModel(_special);
        }

        private void SPECIALModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            int luckModifier = _special.GetClampedHalfLuckModifier();

            if(e.PropertyName ==  nameof(SPECIALModel.Strength))
            {
                _skill.CalculateSkill(Skill.Intimidation, _special.GetModifier(_skill.Intimidation.SelectedModifier), luckModifier);
                _skill.CalculateSkill(Skill.MeleeWeapons, _special.GetModifier(_skill.MeleeWeapons.SelectedModifier), luckModifier);
                _skill.CalculateSkill(Skill.Unarmed, _special.GetModifier(_skill.Unarmed.SelectedModifier), luckModifier);
            }
            if (e.PropertyName == nameof(SPECIALModel.Perception))
            {
                _skill.CalculateSkill(Skill.Breach, _special.GetModifier(_skill.Breach.SelectedModifier), luckModifier);
                _skill.CalculateSkill(Skill.EnergyWeapons, _special.GetModifier(_skill.EnergyWeapons.SelectedModifier), luckModifier);
                _skill.CalculateSkill(Skill.Explosives, _special.GetModifier(_skill.Explosives.SelectedModifier), luckModifier);
                _skill.CalculateSkill(Skill.Medicine, _special.GetModifier(_skill.Medicine.SelectedModifier), luckModifier);
            }
            if (e.PropertyName == nameof(SPECIALModel.Endurance))
            {
                _skill.CalculateSkill(Skill.Survival, _special.GetModifier(_skill.Survival.SelectedModifier), luckModifier);
            }
            if (e.PropertyName == nameof(SPECIALModel.Charisma))
            {
                _skill.CalculateSkill(Skill.Barter, _special.GetModifier(_skill.Barter.SelectedModifier), luckModifier);
                _skill.CalculateSkill(Skill.Intimidation, _special.GetModifier(_skill.Intimidation.SelectedModifier), luckModifier);
                _skill.CalculateSkill(Skill.Speech, _special.GetModifier(_skill.Speech.SelectedModifier), luckModifier);
            }
            if (e.PropertyName == nameof(SPECIALModel.Intelligence))
            {
                _skill.CalculateSkill(Skill.Breach, _special.GetModifier(_skill.Breach.SelectedModifier), luckModifier);
                _skill.CalculateSkill(Skill.Crafting, _special.GetModifier(_skill.Crafting.SelectedModifier), luckModifier);
                _skill.CalculateSkill(Skill.Medicine, _special.GetModifier(_skill.Medicine.SelectedModifier), luckModifier);
                _skill.CalculateSkill(Skill.Science, _special.GetModifier(_skill.Science.SelectedModifier), luckModifier);
                _skill.CalculateSkill(Skill.Survival, _special.GetModifier(_skill.Survival.SelectedModifier), luckModifier);
            }
            if (e.PropertyName == nameof(SPECIALModel.Agility))
            {
                _skill.CalculateSkill(Skill.Guns, _special.GetModifier(_skill.Guns.SelectedModifier), luckModifier);
                _skill.CalculateSkill(Skill.Sneak, _special.GetModifier(_skill.Sneak.SelectedModifier), luckModifier);
            }
            if (e.PropertyName == nameof(SPECIALModel.Luck))
            {
                _skill.UpdateModel(_special);
            }
        }

        public void VMCalculateSkill(Skill skill)
        {
            _skill.CalculateSkill(skill, _special.GetModifier(_skill.GetSkill(skill).SelectedModifier), _special.GetClampedHalfLuckModifier());
        }
    }
}
