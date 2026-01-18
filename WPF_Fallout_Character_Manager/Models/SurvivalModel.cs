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
            _hunger = new ModInt("Hunger", 0, false, "Whenever you roll a d20 (besides Luck), the total is subtracted by 1 for each level of hunger you have. At the end of each day, or after 24 hours, if you did not consume at least one food, you gain one level of hunger. When you gain your tenth level of hunger, you die.");
            _dehydration = new ModInt("Dehydration", 0, false, "Whenever you roll a d20 (besides Luck), the total is subtracted by 1 for each level of dehydration you have. At the end of each day, or every 24 hours, if you did not consume at least three drinks or a drink with the hydrating property, you gain three levels of dehydration. When you gain your tenth level of dehydration, you die.");
            _exhaustion = new ModInt("Exhaustion", 0, false, "Whenever you roll a d20 (besides Luck), the total is subtracted by 1 for each level of exhaustion you have. When you gain your tenth level of exhaustion, you die. If you are a human, ghoul, or super mutant; you can remove one level of exhaustion after resting for at least 6 hours. If you are a robot or gen-2 synth; you can remove one level of exhaustion after resting for at least 2 hours.");
            _radDc = new ModInt("RadDC", 0, true, "Your Radiation DC is equal to 12 - your Endurance ability modifier. Radiation DC is a measurement of how much you can resist radiation before taking levels of Rads. Whenever your character enters or starts their turn for the first time in an Irradiated Zone you must roll a d20 roll against your Radiation DC. If you fail, you take 1 level of radiation. If you succeed, your Radiation DC increases by 2 until you remove all your levels of Rads. All Irradiated Zones have a Radiation Severity Score that determines how often you must roll a d20 against your Radiation DC.");
            _rads = new ModInt("Rads", 0, false, "Whenever you roll a d20 (besides Luck), the total is subtracted by 1 for each level of rads you have. Additionally, each time you gain a level of rads you take 1d4 radiation damage to your hit points and stamina points that cannot be healed until you no longer have any levels of rads. If this radiation damage reduces you to 0 hit points, or you would gain your 10th level of radiation; you die.");
            _passiveSense = new ModInt("Passive Sense", 0, true, "A measurement of your senses at all times. When you roll a Perception ability check, your character is actively trying to find something they may already be aware of. Creatures that sneak remain undetected if they roll higher than your passive sense score. Your passive sense score is equal to 12 + your Perception modifier.");
            _partyNerve = new ModInt("Party Nerve", 0, false, "Party Nerve is the manifestation of your infectious Charisma and strong leadership, allowing your party members to push their limits and survive. Your Party Nerve bonus is equal to every players character’s Charisma modifier added together, then halved (rounded down). Your Party Nerve bonus is added to your Death Saves. Additionally, whenever you roll combat sequence you gain temporary stamina points equal to your Party Nerve bonus.");
            _groupSneak = new ModInt("Group Sneak", 0, false, "Player characters, while traveling, can move at half pace and use their Group Sneak to remain stealthy while traveling. Your Group Sneak is equal to each player character’s Sneak modifier divided by the number of player characters. (Simply put; Group Sneak is the mean, or average, Sneak modifier of each player character).");
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
            RadDC.BaseValue = 12 - enduranceModifier;
        }

        void CalculatePassiveSense(int perceptionModifier)
        {
            PassiveSense.BaseValue = 12 + perceptionModifier;
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
