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
            _skills[Skill.Barter].Note = "The Barter skill measures your expertise in trade. Whether that be with merchants when trading valuables, or with powerful figures to discuss terms. Barter encompasses all manner of exchange, this for that. The higher your Barter skill, the more convincing you are when it comes to sweetening your deals. You can also use this skill with a unique ability called Discount: When you purchase an item from a merchant with any kind of currency, you can gain a percentage discount equal to your Barter skill bonus on that item. Once you use this ability, you cannot use it again until you rest for 8 hours.";
            _skills[Skill.Breach].Note = "The Breach skill measures your ability to open locks, hack computers, disarm traps, or anything that lets you get to places you’re not supposed to. Whenever you make a Breach check, you can re-roll the check a number of times equal to your Luck modifier (up to GM’s discretion). You can optionally choose for your Breach skill to be modified by your Perception modifier instead of Intelligence.";
            _skills[Skill.Crafting].Note = "The Crafting skill measures your knowledge of schematics to repair and craft items. To craft most weapons, armor, mods, and upgrades; your Crafting skill must be equal to or more than its requirement. If you do not meet the requirement, you can roll a Crafting skill check with the DC being 12 + the Crafting skill requirement bonus. On a success you craft the items, on a failure you do not craft the items and you use half of your choice of the required materials. If an item has a level of Decay, you can use your Crafting skill to repair it and remove the level of Decay.";
            _skills[Skill.EnergyWeapons].Note = "The Energy Weapons skill measures your expertise in the use of technological weapons that shoot powerful lasers, flash arcs of electricity, fire superheated bolts of plasma, and even launch ammo with electromagnetic coils to accelerate projectiles at extreme velocities. When you make an attack roll with an energy weapon, you add your Energy Weapons skill bonus to the attack roll.";
            _skills[Skill.Explosives].Note = "The Explosives skill measures your know-how in arming, disarming, and overall handling of any explosive device. Whether that be a fragmentation grenade, C-4 explosive, molotov cocktail, plasma grenade, or smoke bomb. When you use your AP to arm and throw an explosive, roll a d20 and add your explosives skill bonus. If the total is a 1, the explosive detonates immediately before you throw it. If the total is a 2 or 3, the explosive is thrown half the distance and detonates at the start of your next turn. If the total is between 3 and 14, the explosive detonates at the start of your next turn. If the result is a 15 or higher, the explosive detonates at the end of your turn. When you use your AP to throw an explosive that has already been armed, roll an Explosives check. If the result is a 12 or below, the explosive is not thrown and explodes immediately. If the result is above an 13, the explosive detonates at the end of your turn.";
            _skills[Skill.Guns].Note = "The Guns skill measures your ability to wield and shoot all manner of firearms. From revolvers, to snipers rifles, to mini-guns. When you make an attack roll with a big gun, handgun, rifle, shotgun, or submachine gun you add your Guns skill bonus to the attack roll.";
            _skills[Skill.Intimidation].Note = "The Intimidation skill measures how physically daunting you are, your ability to frighten others, or even make them re-evaluate confronting you. You can optionally choose for your Intimidation skill to be modified by your Strength modifier instead of Charisma.";
            _skills[Skill.Medicine].Note = "The Medicine skill measures your aptitude for accessing symptoms and properly treating them with care while under pressure. Knowledge of Medicine also allows you to craft chems. To craft most chem recipes, your Medicine skill must be equal to or more than its requirement. If you are within 5 feet of a creature with the bleeding condition, you can spend 6 AP, use 1 cloth junk item, and succeed a DC 15 Medicine skill check to end the condition. If another character is dying and has not failed all of their Death Saves, you can (at GM’s discretion) spend 6 AP to make a Medicine check with the DC being 10 + their failed Death Saves and - their successful Death Saves. On a success the creature gains 1 hit point. You can optionally choose for your Medicine skill to be modified by your Perception modifier instead of Intelligence.";
            _skills[Skill.MeleeWeapons].Note = "The Melee Weapons skill measures your ability to chop, stab, or bludgeon with weapons such as knives, boards, nine-irons, or even chainsaws. When you make an attack roll with a melee weapon, you add your Melee Weapons skill bonus to the attack roll.";
            _skills[Skill.Science].Note = "The Science skill measures your knowledge and deductive reasoning for all kinds of studies. Biology, historiography, cosmology, chemistry, geology, etc. You can use the Science skill to craft theories and produce a likely outcome. If your character ever encounters something mysterious, you can make a Science skill check to deduce the reasoning. Alternatively, if your GM allows it, you can ask the GM a question about a specific subject that you may not know the answer to, but your character might. On a successful roll you may learn the information.";
            _skills[Skill.Sneak].Note = "The Sneak skill measures your ability to remain quiet or inconspicuous. On your turn you can spend 6 AP to hide. When you hide you roll a Sneak skill check, if you have cover against any creatures and your Sneak skill check total is equal to or greater than their passive Perception, then you are hidden from them and they cannot see you. If you attack a creature who you are hidden from, you gain advantage on the attack roll. If a creature is aware of your presence, but cannot see you because you are hidden, they may make a Perception check contested against a Sneak check from you. If their total is higher, you are no longer hidden. If your total is higher, then you remain hidden";
            _skills[Skill.Speech].Note = "The Speech skill measures your ability to communicate and to read others. Speech encompasses your ability to persuade, dissuade, convince, or deceive. Knowledge of language, oratory, and expression also falls under Speech. You can use the Speech skill to convince non-player characters to agree with you, perform a favor for you, or even risk their life for you (at GM’s discretion). You can also use Speech to tell a convincing lie, read the intentions of someone who may have ill-intent towards you, or to communicate non-verbally.";
            _skills[Skill.Survival].Note = "The Survival skill measures a mixture of your adaptation, knowledge of the land, ability to hunt, and expertise in cuisine. You can use your Survival skill to track other creatures. To craft most food and drink recipes, your Survival skill must be equal to or more than its requirement. Additionally, learning more about a creature’s habits, type, or physical abilities might require a Survival check (at GM’s discretion).";
            _skills[Skill.Unarmed].Note = "The Unarmed skill measures your ability to punch, strike, wrestle, kick, elbow, knee, or grapple other creatures. When you make an unarmed attack roll, you add your Unarmed skill bonus to the attack roll. Unarmed attack rolls deal 1d4 + your Strength or Agility modifier bludgeoning damage, they critically hit when you roll a 20 on the attack roll and deal extra damage equal to double the total amount of damage. Unarmed attacks can be non-lethal.";
            

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
        public Dictionary<Skill, ModIntSkill> Skills
        {
            get => _skills;
        }

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
