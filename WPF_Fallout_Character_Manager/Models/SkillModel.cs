using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models
{
    enum Models
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
        Unarmed
    }

    class SkillModel : ModelBase
    {
        // Constructor
        public SkillModel()
        {
            _barter = new ModInt("Barter", 0, true);
            _barter.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Barter));
            _breach = new ModInt("Breach", 0, true);
            _breach.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Breach));
            _crafting = new ModInt("Crafting", 0, true);
            _crafting.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Crafting));
            _energyWeapons = new ModInt("Energy Weapons", 0, true);
            _energyWeapons.PropertyChanged += (s, e) => OnPropertyChanged(nameof(EnergyWeapons));
            _explosives = new ModInt("Explosives", 0, true);
            _explosives.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Explosives));
            _guns = new ModInt("Guns", 0, true);
            _guns.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Guns));
            _intimidation = new ModInt("Intimidation", 0, true);
            _intimidation.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Intimidation));
            _medicine = new ModInt("Medicine", 0, true);
            _medicine.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Medicine));
            _meleeWeapons = new ModInt("Melee Weapons", 0, true);
            _meleeWeapons.PropertyChanged += (s, e) => OnPropertyChanged(nameof(MeleeWeapons));
            _science = new ModInt("Science", 0, true);
            _science.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Science));
            _sneak = new ModInt("Sneak", 0, true);
            _sneak.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Sneak));
            _speech = new ModInt("Speech", 0, true);
            _speech.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Speech));
            _survival = new ModInt("Survival", 0, true);
            _survival.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Survival));
            _unarmed = new ModInt("Unarmed", 0, true);
            _unarmed.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Unarmed));
        }
        //

        // Helpers

        // Only call this on construct of the ViewModel. It doesn't take into account alternative skill scaling and tagged skills.
        public void UpdateModel(SPECIALModel specialModel, SPECIAL breachScaling, SPECIAL intimidationScaling, SPECIAL medicineScaling)
        {
            CalculateBarter(specialModel.GetModifier(SPECIAL.Charisma), false);
            switch(breachScaling)
            {
                case SPECIAL.Perception:
                    CalculateBreach(specialModel.GetModifier(SPECIAL.Perception), false);
                    break;
                case SPECIAL.Intelligence:
                    CalculateBreach(specialModel.GetModifier(SPECIAL.Intelligence), false);
                    break;
                default:
                    break;
            }
            CalculateBreach(specialModel.GetModifier(SPECIAL.Perception), false);
            CalculateCrafting(specialModel.GetModifier(SPECIAL.Intelligence), false);
            CalculateEnergyWeapons(specialModel.GetModifier(SPECIAL.Perception), false);
            CalculateExplosives(specialModel.GetModifier(SPECIAL.Perception), false);
            CalculateGuns(specialModel.GetModifier(SPECIAL.Agility), false);
            CalculateIntimidation(specialModel.GetModifier(SPECIAL.Charisma), false);
            CalculateMedicine(specialModel.GetModifier(SPECIAL.Intelligence), false);
            CalculateMeleeWeapons(specialModel.GetModifier(SPECIAL.Strength), false);
            CalculateScience(specialModel.GetModifier(SPECIAL.Intelligence), false);
            CalculateSneak(specialModel.GetModifier(SPECIAL.Agility), false);
            CalculateSpeech(specialModel.GetModifier(SPECIAL.Charisma), false);
            CalculateSurvival(specialModel.GetModifier(SPECIAL.Endurance), false);
            CalculateUnarmed(specialModel.GetModifier(SPECIAL.Strength), false);
        }

        public void CalculateBarter(int charismaMod, bool isTagged)
        {
            Barter.BaseValue = charismaMod;

            //TODO: Maybe we need a way to make this more error-proof. Right now we rely on the fact that the user won't renamed the Tagged modifier.
            if (isTagged)
                Barter.AddModifier(new LabeledInt("Tagged", 2, "(PLEASE DON'T RENAME THIS MODIFIER) You are proficient in this skill."));
            else
                Barter.RemoveModifier("Tagged");
        }

        public void CalculateBreach(int perOrIntMod, bool isTagged)
        {
            Breach.BaseValue = perOrIntMod;

            //TODO: Maybe we need a way to make this more error-proof. Right now we rely on the fact that the user won't renamed the Tagged modifier.
            if (isTagged)
                Breach.AddModifier(new LabeledInt("Tagged", 2, "(PLEASE DON'T RENAME THIS MODIFIER) You are proficient in this skill."));
            else
                Breach.RemoveModifier("Tagged");
        }

        public void CalculateCrafting(int intelligenceMod, bool isTagged)
        {
            Crafting.BaseValue = intelligenceMod;

            //TODO: Maybe we need a way to make this more error-proof. Right now we rely on the fact that the user won't renamed the Tagged modifier.
            if (isTagged)
                Crafting.AddModifier(new LabeledInt("Tagged", 2, "(PLEASE DON'T RENAME THIS MODIFIER) You are proficient in this skill."));
            else
                Crafting.RemoveModifier("Tagged");
        }

        public void CalculateEnergyWeapons(int perceptionMod, bool isTagged)
        {
            EnergyWeapons.BaseValue = perceptionMod;

            //TODO: Maybe we need a way to make this more error-proof. Right now we rely on the fact that the user won't renamed the Tagged modifier.
            if (isTagged)
                EnergyWeapons.AddModifier(new LabeledInt("Tagged", 2, "(PLEASE DON'T RENAME THIS MODIFIER) You are proficient in this skill."));
            else
                EnergyWeapons.RemoveModifier("Tagged");
        }

        public void CalculateExplosives(int explosivesMod, bool isTagged)
        {
            Explosives.BaseValue = explosivesMod;

            //TODO: Maybe we need a way to make this more error-proof. Right now we rely on the fact that the user won't renamed the Tagged modifier.
            if (isTagged)
                Explosives.AddModifier(new LabeledInt("Tagged", 2, "(PLEASE DON'T RENAME THIS MODIFIER) You are proficient in this skill."));
            else
                Explosives.RemoveModifier("Tagged");
        }

        public void CalculateGuns(int agilityMod, bool isTagged)
        {
            Guns.BaseValue = agilityMod;

            //TODO: Maybe we need a way to make this more error-proof. Right now we rely on the fact that the user won't renamed the Tagged modifier.
            if (isTagged)
                Guns.AddModifier(new LabeledInt("Tagged", 2, "(PLEASE DON'T RENAME THIS MODIFIER) You are proficient in this skill."));
            else
                Guns.RemoveModifier("Tagged");
        }

        public void CalculateIntimidation(int strOrChaMod, bool isTagged)
        {
            Intimidation.BaseValue = strOrChaMod;

            //TODO: Maybe we need a way to make this more error-proof. Right now we rely on the fact that the user won't renamed the Tagged modifier.
            if (isTagged)
                Intimidation.AddModifier(new LabeledInt("Tagged", 2, "(PLEASE DON'T RENAME THIS MODIFIER) You are proficient in this skill."));
            else
                Intimidation.RemoveModifier("Tagged");
        }

        public void CalculateMedicine(int intOrPerMod, bool isTagged)
        {
            Medicine.BaseValue = intOrPerMod;

            //TODO: Maybe we need a way to make this more error-proof. Right now we rely on the fact that the user won't renamed the Tagged modifier.
            if (isTagged)
                Medicine.AddModifier(new LabeledInt("Tagged", 2, "(PLEASE DON'T RENAME THIS MODIFIER) You are proficient in this skill."));
            else
                Medicine.RemoveModifier("Tagged");
        }

        public void CalculateMeleeWeapons(int strengthMod, bool isTagged)
        {
            MeleeWeapons.BaseValue = strengthMod;

            //TODO: Maybe we need a way to make this more error-proof. Right now we rely on the fact that the user won't renamed the Tagged modifier.
            if (isTagged)
                MeleeWeapons.AddModifier(new LabeledInt("Tagged", 2, "(PLEASE DON'T RENAME THIS MODIFIER) You are proficient in this skill."));
            else
                MeleeWeapons.RemoveModifier("Tagged");
        }

        public void CalculateScience(int intelligenceMod, bool isTagged)
        {
            Science.BaseValue = intelligenceMod;

            //TODO: Maybe we need a way to make this more error-proof. Right now we rely on the fact that the user won't renamed the Tagged modifier.
            if (isTagged)
                Science.AddModifier(new LabeledInt("Tagged", 2, "(PLEASE DON'T RENAME THIS MODIFIER) You are proficient in this skill."));
            else
                Science.RemoveModifier("Tagged");
        }

        public void CalculateSneak(int agilityMod, bool isTagged)
        {
            Sneak.BaseValue = agilityMod;

            //TODO: Maybe we need a way to make this more error-proof. Right now we rely on the fact that the user won't renamed the Tagged modifier.
            if (isTagged)
                Sneak.AddModifier(new LabeledInt("Tagged", 2, "(PLEASE DON'T RENAME THIS MODIFIER) You are proficient in this skill."));
            else
                Sneak.RemoveModifier("Tagged");
        }

        public void CalculateSpeech(int charismaMod, bool isTagged)
        {
            Speech.BaseValue = charismaMod;

            //TODO: Maybe we need a way to make this more error-proof. Right now we rely on the fact that the user won't renamed the Tagged modifier.
            if (isTagged)
                Speech.AddModifier(new LabeledInt("Tagged", 2, "(PLEASE DON'T RENAME THIS MODIFIER) You are proficient in this skill."));
            else
                Speech.RemoveModifier("Tagged");
        }

        //TODO: Consider making this skill scale with either Endurance or Intelligence
        public void CalculateSurvival(int enduranceMod, bool isTagged)
        {
            Survival.BaseValue = enduranceMod;

            //TODO: Maybe we need a way to make this more error-proof. Right now we rely on the fact that the user won't renamed the Tagged modifier.
            if (isTagged)
                Survival.AddModifier(new LabeledInt("Tagged", 2, "(PLEASE DON'T RENAME THIS MODIFIER) You are proficient in this skill."));
            else
                Survival.RemoveModifier("Tagged");
        }

        public void CalculateUnarmed(int strengthMod, bool isTagged)
        {
            Unarmed.BaseValue = strengthMod;

            //TODO: Maybe we need a way to make this more error-proof. Right now we rely on the fact that the user won't renamed the Tagged modifier.
            if (isTagged)
                Unarmed.AddModifier(new LabeledInt("Tagged", 2, "(PLEASE DON'T RENAME THIS MODIFIER) You are proficient in this skill."));
            else
                Unarmed.RemoveModifier("Tagged");
        }
        //

        // Data
        private ModInt _barter;
        public ModInt Barter
        {
            get => _barter;
            set => Update(ref _barter, value);
        }

        private ModInt _breach;
        public ModInt Breach
        {
            get => _breach;
            set => Update(ref _breach, value);
        }

        private ModInt _crafting;
        public ModInt Crafting
        {
            get => _crafting;
            set => Update(ref _crafting, value);
        }

        private ModInt _energyWeapons;
        public ModInt EnergyWeapons
        {
            get => _energyWeapons;
            set => Update(ref _energyWeapons, value);
        }

        private ModInt _explosives;
        public ModInt Explosives
        {
            get => _explosives;
            set => Update(ref _explosives, value);
        }

        private ModInt _guns;
        public ModInt Guns
        {
            get => _guns;
            set => Update(ref _guns, value);
        }

        private ModInt _intimidation;
        public ModInt Intimidation
        {
            get => _intimidation;
            set => Update(ref _intimidation, value);
        }

        private ModInt _medicine;
        public ModInt Medicine
        {
            get => _medicine;
            set => Update(ref _medicine, value);
        }

        private ModInt _meleeWeapons;
        public ModInt MeleeWeapons
        {
            get => _meleeWeapons;
            set => Update(ref _meleeWeapons, value);
        }

        private ModInt _science;
        public ModInt Science
        {
            get => _science;
            set => Update(ref _science, value);
        }

        private ModInt _sneak;
        public ModInt Sneak
        {
            get => _sneak;
            set => Update(ref _sneak, value);
        }

        private ModInt _speech;
        public ModInt Speech
        {
            get => _speech;
            set => Update(ref _speech, value);
        }

        private ModInt _survival;
        public ModInt Survival
        {
            get => _survival;
            set => Update(ref _survival, value);
        }

        private ModInt _unarmed;
        public ModInt Unarmed
        {
            get => _unarmed;
            set => Update(ref _unarmed, value);
        }
        //
    }
}
