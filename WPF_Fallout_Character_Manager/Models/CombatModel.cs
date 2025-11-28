using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models
{
    class CombatModel : ModelBase
    {
        // constructor
        public CombatModel()
        {
            _ap = new ModInt("Action Points", 0, true, "Every creature has Action Points (abbreviated to AP) that quantify what they can do on a turn. A creature’s AP total is equal to 10 + their Agility modifier. On your turn, you can spend AP to perform certain actions in combat.");
            _ap.PropertyChanged += (s, e) => OnPropertyChanged(nameof(ActionPoints));
            _msp = new ModInt("Max Stamina Points", 0, true, "Stamina Points are a measurement of your exhaustion, dodging abilities, and fortitude. Dodging a near life ending bullet is harrowing, sometimes you can shrug off a punch, but as battle continues you grow weary and thus your stamina points drain. Whenever a creature takes damage, and can see or is otherwise aware of the damage; the damage is subtracted from their stamina points. When your stamina points drop to 0, damage is subsequently subtracted from your Hit Points.");
            _msp.PropertyChanged += (s, e) => OnPropertyChanged(nameof(MaxStaminaPoints));
            _sp = new ModInt("Stamina Points", 0, false, "Stamina Points are a measurement of your exhaustion, dodging abilities, and fortitude. Dodging a near life ending bullet is harrowing, sometimes you can shrug off a punch, but as battle continues you grow weary and thus your stamina points drain. Whenever a creature takes damage, and can see or is otherwise aware of the damage; the damage is subtracted from their stamina points. When your stamina points drop to 0, damage is subsequently subtracted from your Hit Points.");
            _sp.PropertyChanged += (s, e) => OnPropertyChanged(nameof(StaminaPoints));
            _mhp = new ModInt("Max Health Points", 0, true, "Hit points represent a combination of physical and mental durability, the will to live, and luck. Whenever a creature takes damage and has no stamina points, that damage is subtracted from its hit points. Creatures also take damage to their hit points if they are unconscious, unaware of their surroundings, or otherwise incapable of moving.");
            _mhp.PropertyChanged += (s, e) => OnPropertyChanged(nameof(MaxHealthPoints));
            _hp = new ModInt("Health Points", 0, false, "Hit points represent a combination of physical and mental durability, the will to live, and luck. Whenever a creature takes damage and has no stamina points, that damage is subtracted from its hit points. Creatures also take damage to their hit points if they are unconscious, unaware of their surroundings, or otherwise incapable of moving.");
            _hp.PropertyChanged += (s, e) => OnPropertyChanged(nameof(HealthPoints));
            _ac = new ModInt("Armor Class", 10, true, "A representation of how tough your armor is. Abbreviated to AC, while you wear armor your AC is equal to the number listed. Otherwise, your AC is 10 and cannot be lower than 10 unless another ability specifies.");
            _ac.PropertyChanged += (s, e) => OnPropertyChanged(nameof(ArmorClass));
            _dt = new ModInt("Damage Threshold", 0, true, "The measurement of how much the armor protects you. Whenever you take damage to your hit points, the damage is reduced by a number equal to your DT. Armor with higher DT covers more of your body, protecting you from all attacks. Your DT can never be lower than 0.");
            _dt.PropertyChanged += (s, e) => OnPropertyChanged(nameof(DamageThreshold));
            _cs = new ModInt("Combat Sequence", 0, true, "This ability measures how quickly you are to act in the round of combat. At the start of combat, everybody rolls Combat Sequence. This determines the order of each creature during combat and who acts when. Combat Sequence equals the Perception modifier.");
            _cs.PropertyChanged += (s, e) => OnPropertyChanged(nameof(CombatSequence));
            _hr = new ModInt("Healing Rate", 0, true, "Every creature has a Healing Rate which measures how many hit points you gain from a source that heals you. Your Healing Rate is equal to half of the total of: your level + your Endurance ability score. You heal a number of hit points equal to your Healing Rate whenever you rest for 8 hours, eat certain foods, use the First Aid perk, a stimpak, or RobCo Quick Fix-It.");
            _hr.PropertyChanged += (s, e) => OnPropertyChanged(nameof(HealingRate));
            _ft = new ModInt("Fatigue", 0, false, "Whenever you roll a d20 (besides Luck), the total is subtracted by 1 for each level of fatigue you have. You can only ever have a total of nine levels of fatigue. At the end of each of your turns you lose one level of fatigue.");
            _ft.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Fatigue));
        }
        //

        // helpers
        // TODO: BaseValues of AC and DT should scale with the equipped armor. For now, these two will be fully calculated via modifiers
        public void UpdateModel(SPECIALModel specialModel, Level level)
        {
            CalculateActionPoints(specialModel.GetModifier(SPECIAL.Agility));
            CalculateMaxStaminaPoints(specialModel.GetModifier(SPECIAL.Agility), level);
            CalculateMaxHealthPoints(specialModel.GetModifier(SPECIAL.Endurance), level);
            CalculateCombatSequence(specialModel.GetModifier(SPECIAL.Perception));
            CalculateHealingRate(specialModel.Endurance.Total, level);

            StaminaPoints.BaseValue = MaxStaminaPoints.BaseValue;
            HealthPoints.BaseValue = MaxHealthPoints.BaseValue;
        }

        public void CalculateActionPoints(int agilityModifier)
        {
            ActionPoints.BaseValue = 10 + agilityModifier;
        }

        public void CalculateMaxStaminaPoints(int agilityModifier, Level level)
        {
            //TODO: Apply the correct formula when you upload the datatables from the rulebook.
            //MaxStaminaPoints.BaseValue = 10 + agilityModifier + level;
            MaxStaminaPoints.BaseValue = level.BaseSp + agilityModifier * level.SpMultiplier;
        }

        public void CalculateMaxHealthPoints(int enduranceModifier, Level level)
        {
            //TODO: Apply the correct formula when you upload the datatables from the rulebook.
            //MaxHealthPoints.BaseValue = 10 + enduranceModifier + level;
            MaxHealthPoints.BaseValue = level.BaseHp + enduranceModifier * level.HpMultiplier;
        }

        public void CalculateCombatSequence(int perceptionModifier)
        {
            CombatSequence.BaseValue = perceptionModifier;
        }

        public void CalculateHealingRate(int enduranceScore, Level level)
        {
            HealingRate.BaseValue = (enduranceScore + level.LevelModInt.BaseValue) / 2;
        }
        //

        // Data
        private ModInt _ap;
        public ModInt ActionPoints
        {
            get => _ap;
            set => Update(ref  _ap, value);
        }

        private ModInt _msp;
        public ModInt MaxStaminaPoints
        {
            get => _msp;
            set => Update(ref _msp, value);
        }

        private ModInt _sp;
        public ModInt StaminaPoints
        {
            get => _sp;
            set => Update(ref _sp, value);
        }

        private ModInt _mhp;
        public ModInt MaxHealthPoints
        {
            get => _mhp;
            set => Update(ref _mhp, value);
        }

        private ModInt _hp;
        public ModInt HealthPoints
        {
            get => _hp;
            set => Update(ref _hp, value);
        }

        private ModInt _ac;
        public ModInt ArmorClass
        {
            get => _ac;
            set => Update(ref _ac, value);
        }

        private ModInt _dt;
        public ModInt DamageThreshold
        {
            get => _dt;
            set => Update(ref _dt, value);
        }

        private ModInt _cs;
        public ModInt CombatSequence
        {
            get => _cs;
            set => Update(ref _cs, value);
        }

        private ModInt _hr;
        public ModInt HealingRate
        {
            get => _hr;
            set => Update(ref _hr, value);
        }

        private ModInt _ft;
        public ModInt Fatigue
        {
            get => _ft;
            set => Update(ref _ft, value);
        }
        //
    }
}
