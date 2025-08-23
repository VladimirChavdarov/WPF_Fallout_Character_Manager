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
    public enum Skill
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
            _skills = new Dictionary<Skill, ModIntSkill>
            {
                { Skill.Barter,        new ModIntSkill("Barter", 0, new ObservableCollection<SPECIAL>{ SPECIAL.Charisma }, true) },
                { Skill.Breach,        new ModIntSkill("Breach", 0,new ObservableCollection<SPECIAL>{ SPECIAL.Perception, SPECIAL.Intelligence },  true) },
                { Skill.Crafting,      new ModIntSkill("Crafting", 0,new ObservableCollection<SPECIAL>{ SPECIAL.Intelligence },  true) },
                { Skill.EnergyWeapons, new ModIntSkill("Energy Weapons", 0,new ObservableCollection<SPECIAL>{ SPECIAL.Perception },  true) },
                { Skill.Explosives,    new ModIntSkill("Explosives", 0,new ObservableCollection<SPECIAL>{ SPECIAL.Perception },  true) },
                { Skill.Guns,          new ModIntSkill("Guns", 0,new ObservableCollection<SPECIAL>{ SPECIAL.Agility },  true) },
                { Skill.Intimidation,  new ModIntSkill("Intimidation", 0,new ObservableCollection<SPECIAL>{ SPECIAL.Charisma, SPECIAL.Strength },  true) },
                { Skill.Medicine,      new ModIntSkill("Medicine", 0,new ObservableCollection<SPECIAL>{ SPECIAL.Perception, SPECIAL.Intelligence },  true) },
                { Skill.MeleeWeapons,  new ModIntSkill("Melee Weapons", 0,new ObservableCollection<SPECIAL>{ SPECIAL.Strength },  true) },
                { Skill.Science,       new ModIntSkill("Science", 0,new ObservableCollection<SPECIAL>{ SPECIAL.Intelligence },  true) },
                { Skill.Sneak,         new ModIntSkill("Sneak", 0,new ObservableCollection<SPECIAL>{ SPECIAL.Agility },  true) },
                { Skill.Speech,        new ModIntSkill("Speech", 0,new ObservableCollection<SPECIAL>{ SPECIAL.Charisma },  true) },
                { Skill.Survival,      new ModIntSkill("Survival", 0,new ObservableCollection<SPECIAL>{ SPECIAL.Endurance, SPECIAL.Intelligence },  true) },
                { Skill.Unarmed,       new ModIntSkill("Unarmed", 0,new ObservableCollection<SPECIAL>{ SPECIAL.Strength },  true) }
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
        public void UpdateModel(SPECIALModel specialModel)
        {
            int luckModifier = specialModel.GetClampedHalfLuckModifier();

            CalculateSkill(Skill.Barter, specialModel.GetModifier(_skills[Skill.Barter].SelectedModifier), luckModifier);
            CalculateSkill(Skill.Breach, specialModel.GetModifier(_skills[Skill.Breach].SelectedModifier), luckModifier);
            CalculateSkill(Skill.Crafting, specialModel.GetModifier(_skills[Skill.Crafting].SelectedModifier), luckModifier);
            CalculateSkill(Skill.EnergyWeapons, specialModel.GetModifier(_skills[Skill.EnergyWeapons].SelectedModifier), luckModifier);
            CalculateSkill(Skill.Explosives, specialModel.GetModifier(_skills[Skill.Explosives].SelectedModifier), luckModifier);
            CalculateSkill(Skill.Guns, specialModel.GetModifier(_skills[Skill.Guns].SelectedModifier), luckModifier);
            CalculateSkill(Skill.Intimidation, specialModel.GetModifier(_skills[Skill.Intimidation].SelectedModifier), luckModifier);
            CalculateSkill(Skill.Medicine, specialModel.GetModifier(_skills[Skill.Medicine].SelectedModifier), luckModifier);
            CalculateSkill(Skill.MeleeWeapons, specialModel.GetModifier(_skills[Skill.MeleeWeapons].SelectedModifier), luckModifier);
            CalculateSkill(Skill.Science, specialModel.GetModifier(_skills[Skill.Science].SelectedModifier), luckModifier);
            CalculateSkill(Skill.Sneak, specialModel.GetModifier(_skills[Skill.Sneak].SelectedModifier), luckModifier);
            CalculateSkill(Skill.Speech, specialModel.GetModifier(_skills[Skill.Speech].SelectedModifier), luckModifier);
            CalculateSkill(Skill.Survival, specialModel.GetModifier(_skills[Skill.Survival].SelectedModifier), luckModifier);
            CalculateSkill(Skill.Unarmed, specialModel.GetModifier(_skills[Skill.Unarmed].SelectedModifier), luckModifier);
        }

        public void CalculateSkill(Skill skill, int specialModifier, int luckModifier/*, bool isTagged*/)
        {
            _skills[skill].BaseValue = specialModifier + luckModifier;
        }

        public ModIntSkill GetSkill(Skill skill) { return _skills[skill]; }
        //

        // Data
        private Dictionary<Skill, ModIntSkill> _skills;

        public ModIntSkill Barter
        {
            get => _skills[Skill.Barter];
            set => Update(_skills[Skill.Barter], value, v => _skills[Skill.Barter] = v);
        }

        public ModIntSkill Breach
        {
            get => _skills[Skill.Breach];
            set => Update(_skills[Skill.Breach], value, v => _skills[Skill.Breach] = v);
        }

        public ModIntSkill Crafting
        {
            get => _skills[Skill.Crafting];
            set => Update(_skills[Skill.Crafting], value, v => _skills[Skill.Crafting] = v);
        }

        public ModIntSkill EnergyWeapons
        {
            get => _skills[Skill.EnergyWeapons];
            set => Update(_skills[Skill.EnergyWeapons], value, v => _skills[Skill.EnergyWeapons] = v);
        }

        public ModIntSkill Explosives
        {
            get => _skills[Skill.Explosives];
            set => Update(_skills[Skill.Explosives], value, v => _skills[Skill.Explosives] = v);
        }

        public ModIntSkill Guns
        {
            get => _skills[Skill.Guns];
            set => Update(_skills[Skill.Guns], value, v => _skills[Skill.Guns] = v);
        }

        public ModIntSkill Intimidation
        {
            get => _skills[Skill.Intimidation];
            set => Update(_skills[Skill.Intimidation], value, v => _skills[Skill.Intimidation] = v);
        }

        public ModIntSkill Medicine
        {
            get => _skills[Skill.Medicine];
            set => Update(_skills[Skill.Medicine], value, v => _skills[Skill.Medicine] = v);
        }

        public ModIntSkill MeleeWeapons
        {
            get => _skills[Skill.MeleeWeapons];
            set => Update(_skills[Skill.MeleeWeapons], value, v => _skills[Skill.MeleeWeapons] = v);
        }

        public ModIntSkill Science
        {
            get => _skills[Skill.Science];
            set => Update(_skills[Skill.Science], value, v => _skills[Skill.Science] = v);
        }

        public ModIntSkill Sneak
        {
            get => _skills[Skill.Sneak];
            set => Update(_skills[Skill.Sneak], value, v => _skills[Skill.Sneak] = v);
        }

        public ModIntSkill Speech
        {
            get => _skills[Skill.Speech];
            set => Update(_skills[Skill.Speech], value, v => _skills[Skill.Speech] = v);
        }

        public ModIntSkill Survival
        {
            get => _skills[Skill.Survival];
            set => Update(_skills[Skill.Survival], value, v => _skills[Skill.Survival] = v);
        }

        public ModIntSkill Unarmed
        {
            get => _skills[Skill.Unarmed];
            set => Update(_skills[Skill.Unarmed], value, v => _skills[Skill.Unarmed] = v);
        }
        //
    }
}
