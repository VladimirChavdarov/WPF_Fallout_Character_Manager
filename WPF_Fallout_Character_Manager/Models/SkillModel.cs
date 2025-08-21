using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models
{
    enum Skill
    {
        Barter,
        Breach,
        Crafting,
        EnergyWeapons,
        Explosives,
        Guns,
        Intimidation,
        Medicine,
        MeleeWeapons,
        Science,
        Sneak,
        Speech,
        Survival,
        Unarmed
    }

    class SkillModel : ModelBase
    {
        // Constructor
        public SkillModel()
        {
            // register all skills
            _skills = new Dictionary<Skill, ModInt>
            {
                { Skill.Barter,        new ModInt("Barter", 0, true) },
                { Skill.Breach,        new ModInt("Breach", 0, true) },
                { Skill.Crafting,      new ModInt("Crafting", 0, true) },
                { Skill.EnergyWeapons, new ModInt("Energy Weapons", 0, true) },
                { Skill.Explosives,    new ModInt("Explosives", 0, true) },
                { Skill.Guns,          new ModInt("Guns", 0, true) },
                { Skill.Intimidation,  new ModInt("Intimidation", 0, true) },
                { Skill.Medicine,      new ModInt("Medicine", 0, true) },
                { Skill.MeleeWeapons,  new ModInt("Melee Weapons", 0, true) },
                { Skill.Science,       new ModInt("Science", 0, true) },
                { Skill.Sneak,         new ModInt("Sneak", 0, true) },
                { Skill.Speech,        new ModInt("Speech", 0, true) },
                { Skill.Unarmed,       new ModInt("Unarmed", 0, true) }
            };

            // subscribe to OnPropertyChange
            foreach(var keyValue in _skills)
            {
                var key = keyValue.Key;
                var value = keyValue.Value;

                value.PropertyChanged += (s, e) => OnPropertyChanged(key.ToString());
            }
        }
        //

        // Helpers

        // Only call this on construct of the ViewModel. It doesn't take into account alternative skill scaling and tagged skills.
        public void UpdateModel(SPECIALModel specialModel, SPECIAL breachScaling, SPECIAL intimidationScaling, SPECIAL medicineScaling)
        {
            CalculateSkill(Skill.Barter, specialModel.GetModifier(SPECIAL.Charisma), false);
            CalculateSkill(Skill.Breach, specialModel.GetModifier(breachScaling), false);
            CalculateSkill(Skill.Crafting, specialModel.GetModifier(SPECIAL.Intelligence), false);
            CalculateSkill(Skill.EnergyWeapons, specialModel.GetModifier(SPECIAL.Perception), false);
            CalculateSkill(Skill.Explosives, specialModel.GetModifier(SPECIAL.Perception), false);
            CalculateSkill(Skill.Guns, specialModel.GetModifier(SPECIAL.Agility), false);
            CalculateSkill(Skill.Intimidation, specialModel.GetModifier(intimidationScaling), false);
            CalculateSkill(Skill.Medicine, specialModel.GetModifier(medicineScaling), false);
            CalculateSkill(Skill.MeleeWeapons, specialModel.GetModifier(SPECIAL.Strength), false);
            CalculateSkill(Skill.Science, specialModel.GetModifier(SPECIAL.Intelligence), false);
            CalculateSkill(Skill.Sneak, specialModel.GetModifier(SPECIAL.Agility), false);
            CalculateSkill(Skill.Speech, specialModel.GetModifier(SPECIAL.Charisma), false);
            CalculateSkill(Skill.Survival, specialModel.GetModifier(SPECIAL.Endurance), false);
            CalculateSkill(Skill.Unarmed, specialModel.GetModifier(SPECIAL.Strength), false);
        }

        public void CalculateSkill(Skill skill, int specialModifier, bool isTagged)
        {
            _skills[skill].BaseValue = specialModifier;

            //TODO: Maybe we need a way to make this more error-proof. Right now we rely on the fact that the user won't renamed the Tagged modifier.
            if (isTagged)
                _skills[skill].AddModifier(new LabeledInt("Tagged", 2, "(PLEASE DON'T RENAME THIS MODIFIER) You are proficient in this skill."));
            else
                _skills[skill].RemoveModifier("Tagged");
        }
        //

        // Data
        private Dictionary<Skill, ModInt> _skills;

        public ModInt Barter
        {
            get => _skills[Skill.Barter];
            set => Update(_skills[Skill.Barter], value, v => _skills[Skill.Barter] = v);
        }

        public ModInt Breach
        {
            get => _skills[Skill.Breach];
            set => Update(_skills[Skill.Breach], value, v => _skills[Skill.Breach] = v);
        }

        public ModInt Crafting
        {
            get => _skills[Skill.Crafting];
            set => Update(_skills[Skill.Crafting], value, v => _skills[Skill.Crafting] = v);
        }

        public ModInt EnergyWeapons
        {
            get => _skills[Skill.EnergyWeapons];
            set => Update(_skills[Skill.EnergyWeapons], value, v => _skills[Skill.EnergyWeapons] = v);
        }

        public ModInt Explosives
        {
            get => _skills[Skill.Explosives];
            set => Update(_skills[Skill.Explosives], value, v => _skills[Skill.Explosives] = v);
        }

        public ModInt Guns
        {
            get => _skills[Skill.Guns];
            set => Update(_skills[Skill.Guns], value, v => _skills[Skill.Guns] = v);
        }

        public ModInt Intimidation
        {
            get => _skills[Skill.Intimidation];
            set => Update(_skills[Skill.Intimidation], value, v => _skills[Skill.Intimidation] = v);
        }

        public ModInt Medicine
        {
            get => _skills[Skill.Medicine];
            set => Update(_skills[Skill.Medicine], value, v => _skills[Skill.Medicine] = v);
        }

        public ModInt MeleeWeapons
        {
            get => _skills[Skill.MeleeWeapons];
            set => Update(_skills[Skill.MeleeWeapons], value, v => _skills[Skill.MeleeWeapons] = v);
        }

        public ModInt Science
        {
            get => _skills[Skill.Science];
            set => Update(_skills[Skill.Science], value, v => _skills[Skill.Science] = v);
        }

        public ModInt Sneak
        {
            get => _skills[Skill.Sneak];
            set => Update(_skills[Skill.Sneak], value, v => _skills[Skill.Sneak] = v);
        }

        public ModInt Speech
        {
            get => _skills[Skill.Speech];
            set => Update(_skills[Skill.Speech], value, v => _skills[Skill.Speech] = v);
        }

        public ModInt Unarmed
        {
            get => _skills[Skill.Unarmed];
            set => Update(_skills[Skill.Unarmed], value, v => _skills[Skill.Unarmed] = v);
        }
        //
    }
}
