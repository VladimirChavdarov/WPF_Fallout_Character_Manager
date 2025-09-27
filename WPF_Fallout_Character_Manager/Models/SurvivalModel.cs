using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.MVVM;
namespace WPF_Fallout_Character_Manager.Models
{
    internal sealed class SurvivalModel : ModelBase
    {
        // constructor
        public SurvivalModel()
        {
            _hunger = new ModInt("Hunger", 0);
            _hunger.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Hunger));
            _dehydration = new ModInt("Dehydration", 0);
            _dehydration.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Dehydration));
            _exhaustion = new ModInt("Exhaustion", 0);
            _exhaustion.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Exhaustion));
            _radDc = new ModInt("RadDC", 0, true);
            _radDc.PropertyChanged += (s, e) => OnPropertyChanged(nameof(RadDC));
            _rads = new ModInt("Rads", 0);
            _rads.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Rads));
            _passiveSense = new ModInt("Passive Sense", 0, true);
            _passiveSense.PropertyChanged += (s, e) => OnPropertyChanged(nameof(PassiveSense));
            _partyNerve = new ModInt("Party Nerve", 0);
            _partyNerve.PropertyChanged += (s, e) => OnPropertyChanged(nameof(PartyNerve));
            _groupSneak = new ModInt("Group Sneak", 5); //TODO: remove placeholder value
            _groupSneak.PropertyChanged += (s, e) => OnPropertyChanged(nameof(GroupSneak));
        }
        //

        // helpers

        // Updates any properties that depend on other Models via functions here.
        // Updates the RadDC based on the Endurance Modifier
        public void UpdateModel(SPECIALModel specialModel)
        {
            CalculateRadDC(specialModel.GetModifier(SPECIAL.Endurance));
            CalculatePassiveSense(specialModel.GetModifier(SPECIAL.Perception));
        }

        void CalculateRadDC(int enduranceModifier)
        {
            RadDC.BaseValue.Value = 12 - enduranceModifier;
        }

        void CalculatePassiveSense(int perceptionModifier)
        {
            PassiveSense.BaseValue.Value = 12 + perceptionModifier;
        }
        //

        // Data
        private ModInt _hunger;
        public ModInt Hunger
        {
            get => _hunger;
            set => Update(ref _hunger, value);
        }

        private ModInt _dehydration;
        public ModInt Dehydration
        {
            get => _dehydration;
            set => Update(ref _dehydration, value);
        }

        private ModInt _exhaustion;
        public ModInt Exhaustion
        {
            get => _exhaustion;
            set => Update(ref _exhaustion, value);
        }

        private ModInt _radDc;
        public ModInt RadDC
        {
            get => _radDc;
            set => Update(ref _radDc, value);
        }

        private ModInt _rads;
        public ModInt Rads
        {
            get => _rads;
            set => Update(ref _rads, value);
        }

        private ModInt _passiveSense;
        public ModInt PassiveSense
        {
            get => _passiveSense;
            set => Update(ref _passiveSense, value);
        }

        private ModInt _partyNerve;
        public ModInt PartyNerve
        {
            get => _partyNerve;
            set => Update(ref _partyNerve, value);
        }

        private ModInt _groupSneak;
        public ModInt GroupSneak
        {
            get => _groupSneak;
            set => Update(ref _groupSneak, value);
        }
        //
    }
}
