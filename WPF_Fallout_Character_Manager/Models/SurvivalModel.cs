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
            _hunger = 1;
            _dehydration = 2;
            _exhaustion = 3;
            _radDc = 4;
            _rads = 5;
            _passiveSense = 6;
            _partyNerve = 7;
            _groupSneak = 8;
        }
        //

        // helpers

        // Updates any properties that depend on other Models via functions here.
        // Updates the RadDC based on the Endurance Modifier
        void UpdateModel(SPECIALModel specialModel)
        {
            CalculateRadDC(specialModel.GetModifier(specialModel.Endurance));
        }

        void CalculateRadDC(int enduranceModifier)
        {
            RadDC = 12 - enduranceModifier;
        }
        //

        // Data
        private int _hunger;
        public int Hunger
        {
            get => _hunger;
            set => Update(ref _hunger, value);
        }

        private int _dehydration;
        public int Dehydration
        {
            get => _dehydration;
            set => Update(ref _dehydration, value);
        }

        private int _exhaustion;
        public int Exhaustion
        {
            get => _exhaustion;
            set => Update(ref _exhaustion, value);
        }

        private int _radDc;
        public int RadDC
        {
            get => _radDc;
            set => Update(ref _radDc, value);
        }

        private int _rads;
        public int Rads
        {
            get => _rads;
            set => Update(ref _rads, value);
        }

        private int _passiveSense;
        public int PassiveSense
        {
            get => _passiveSense;
            set => Update(ref _passiveSense, value);
        }

        private int _partyNerve;
        public int PartyNerve
        {
            get => _partyNerve;
            set => Update(ref _partyNerve, value);
        }

        private int _groupSneak;
        public int GroupSneak
        {
            get => _groupSneak;
            set => Update(ref _groupSneak, value);
        }
        //
    }
}
