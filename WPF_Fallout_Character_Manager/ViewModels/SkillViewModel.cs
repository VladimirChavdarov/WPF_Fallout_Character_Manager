using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    class SkillViewModel : ViewModelBase
    {
        // local variables
        private SkillModel _skill;
        private SPECIALModel _special;
        private BioModel _bio;
        private int _availableSkillPoints;
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

        public BioModel Bio
        {
            get => _bio;
            set => Update(ref _bio, value);
        }

        public int AvailableSkillPoints
        {
            get => _availableSkillPoints;
            set => Update(ref _availableSkillPoints, value);
        }

        public SkillViewModel(SkillModel? skill, SPECIALModel? special, BioModel? bio)
        {
            _skill = skill;
            _special = special;
            _bio = bio;

            special.PropertyChanged += SPECIALModel_PropertyChanged;
            skill.PropertyChanged += Skill_PropertyChanged;
            bio.PropertyChanged += Bio_PropertyChanged;

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

        private void Skill_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            CalculateAvailableSkillPoints();
        }

        private void Bio_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            CalculateAvailableSkillPoints();
        }

        public void VMCalculateSkill(Skill skill)
        {
            _skill.CalculateSkill(skill, _special.GetModifier(_skill.GetSkill(skill).SelectedModifier), _special.GetClampedHalfLuckModifier());
        }

        private void CalculateAvailableSkillPoints()
        {
            int bonusSkillPointsSum = 0;
            foreach (KeyValuePair<Skill, ModIntSkill> kvp in _skill.Skills)
            {
                foreach(LabeledValue<int> modifier in kvp.Value.Modifiers)
                {
                    if(modifier.Name != ModIntSkill.TaggedModifierName)
                    {
                        bonusSkillPointsSum += modifier.Value;
                    }
                }
            }

            int lowIntelligenceBorder = 4;
            int allowedSkillPointsIndex = Math.Clamp(_special.Intelligence.Total - lowIntelligenceBorder, 0, 2);
            int allowedSkillPoints = _bio.Level.AvailableSkillPoints[allowedSkillPointsIndex];

            AvailableSkillPoints = allowedSkillPoints - bonusSkillPointsSum;
        }
    }
}
