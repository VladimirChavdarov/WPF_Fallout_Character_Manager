using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models
{
    internal sealed class SPECIALModel : ModelBase
    {
        // constructor
        public SPECIALModel()
        {
            _strength = 1;
            _perception = 2;
            _endurance = 3;
            _charisma = 4;
            _intelligence = 5;
            _agililty = 6;
            _luck = 7;
        }
        //

        // helpers
        public int GetModifier(int MainStat)
        {
            return MainStat - 5;
        }
        //

        // Data
        private int _strength;
        public int Strength
        {
            get => _strength;
            set => Update(ref _strength, value);
        }

        private int _perception;
        public int Perception
        {
            get => _perception;
            set => Update(ref _perception, value);
        }

        private int _endurance;
        public int Endurance
        {
            get => _endurance;
            set => Update(ref _endurance, value);
        }

        private int _charisma;
        public int Charisma
        {
            get => _charisma;
            set => Update(ref _charisma, value);
        }

        private int _intelligence;
        public int Intelligence
        {
            get => _intelligence;
            set => Update(ref _intelligence, value);
        }

        private int _agililty;
        public int Agility
        {
            get => _agililty;
            set => Update(ref _agililty, value);
        }

        private int _luck;
        public int Luck
        {
            get => _luck;
            set => Update(ref _luck, value);
        }
        //
    }
}
