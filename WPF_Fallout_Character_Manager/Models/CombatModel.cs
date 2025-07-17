using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models
{
    class CombatModel : ModelBase
    {
        // constructor
        public CombatModel()
        {
            _ap = new ModInt("Action Points", 0, true);
            _ap.PropertyChanged += (s, e) => OnPropertyChanged(nameof(ActionPoints));
            _msp = new ModInt("Max Stamina Points", 0, true);
            _msp.PropertyChanged += (s, e) => OnPropertyChanged(nameof(MaxStaminaPoints));
            _sp = new ModInt("Stamina Points", 0, false);
            _sp.PropertyChanged += (s, e) => OnPropertyChanged(nameof(StaminaPoints));
            _mhp = new ModInt("Max Health Points", 0, true);
            _mhp.PropertyChanged += (s, e) => OnPropertyChanged(nameof(MaxHealthPoints));
            _hp = new ModInt("Health Points", 0, false);
            _hp.PropertyChanged += (s, e) => OnPropertyChanged(nameof(HealthPoints));
            _ac = new ModInt("Armor Class", 0, true);
            _ac.PropertyChanged += (s, e) => OnPropertyChanged(nameof(ArmorClass));
            _dt = new ModInt("Damage Threshold", 0, true);
            _dt.PropertyChanged += (s, e) => OnPropertyChanged(nameof(DamageThreshold));
            _cs = new ModInt("Combat Sequence", 0, true);
            _cs.PropertyChanged += (s, e) => OnPropertyChanged(nameof(CombatSequence));
            _hr = new ModInt("Healing Rate", 0, true);
            _hr.PropertyChanged += (s, e) => OnPropertyChanged(nameof(HealingRate));
            _ft = new ModInt("Fatigue", 0, false);
            _ft.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Fatigue));
        }
        //

        // helpers
        // TODO: BaseValues of AC and DT should scale with the equipped armor. For now, these two will be fully calculated via modifiers
        public void UpdateModel(SPECIALModel specialModel, int level)
        {
            CalculateActionPoints(specialModel.GetModifier(specialModel.Agility));
            CalculateMaxStaminaPoints(specialModel.GetModifier(specialModel.Agility), level);
            CalculateMaxHealthPoints(specialModel.GetModifier(specialModel.Endurance), level);
            CalculateCombatSequence(specialModel.GetModifier(specialModel.Perception));
            CalculateHealingRate(specialModel.Endurance.Total, level);

            StaminaPoints.BaseValue = MaxStaminaPoints.BaseValue;
            HealthPoints.BaseValue = MaxHealthPoints.BaseValue;
        }

        public void CalculateActionPoints(int agilityModifier)
        {
            ActionPoints.BaseValue = 10 + agilityModifier;
        }

        public void CalculateMaxStaminaPoints(int agilityModifier, int level)
        {
            //TODO: Apply the correct formula when you upload the datatables from the rulebook.
            MaxStaminaPoints.BaseValue = 10 + agilityModifier + level;
        }

        public void CalculateMaxHealthPoints(int enduranceModifier, int level)
        {
            //TODO: Apply the correct formula when you upload the datatables from the rulebook.
            MaxHealthPoints.BaseValue = 10 + enduranceModifier + level;
        }

        public void CalculateCombatSequence(int perceptionModifier)
        {
            CombatSequence.BaseValue = perceptionModifier;
        }

        public void CalculateHealingRate(int enduranceScore, int level)
        {
            HealingRate.BaseValue = (enduranceScore + level) / 2;
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
