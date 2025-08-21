using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models
{
    enum SPECIAL
    {
        Strength,
        Perception,
        Endurance,
        Charisma,
        Intelligence,
        Agility,
        Luck
    }

    internal sealed class SPECIALModel : ModelBase
    {
        // constructor
        public SPECIALModel()
        {
            _strength = new ModInt("Strength", 1);
            _strength.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Strength));
            _perception = new ModInt("Perception", 2);
            _perception.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Perception));
            _endurance = new ModInt("Endurance", 3);
            _endurance.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Endurance));
            _charisma = new ModInt("Charisma", 4);
            _charisma.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Charisma));
            _intelligence = new ModInt("Intelligence", 5);
            _intelligence.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Intelligence));
            _agililty = new ModInt("Agility", 6);
            _agililty.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Agility));
            _luck = new ModInt("Luck", 7);
            _luck.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Luck));
        }
        //

        // helpers
        //public int GetModifier(ModInt MainStat)
        //{
        //    return MainStat.Total - 5;
        //}

        public int GetModifier(SPECIAL MainStat)
        {
            switch(MainStat)
            {
                case SPECIAL.Strength:
                    return Strength.Total - 5;
                case SPECIAL.Perception:
                    return Perception.Total - 5;
                case SPECIAL.Endurance:
                    return Endurance.Total - 5;
                case SPECIAL.Charisma:
                    return Charisma.Total - 5;
                case SPECIAL.Intelligence:
                    return Intelligence.Total - 5;
                case SPECIAL.Agility:
                    return Agility.Total - 5;
                case SPECIAL.Luck:
                    return Luck.Total - 5;
                default:
                    throw new Exception("Invalid enum value...");
            }
        }
        //

        // Data
        private ModInt _strength;
        public ModInt Strength
        {
            get => _strength;
            set => Update(ref _strength, value);
        }

        private ModInt _perception;
        public ModInt Perception
        {
            get => _perception;
            set => Update(ref _perception, value);
        }

        private ModInt _endurance;
        public ModInt Endurance
        {
            get => _endurance;
            set => Update(ref _endurance, value);
        }

        private ModInt _charisma;
        public ModInt Charisma
        {
            get => _charisma;
            set => Update(ref _charisma, value);
        }

        private ModInt _intelligence;
        public ModInt Intelligence
        {
            get => _intelligence;
            set => Update(ref _intelligence, value);
        }

        private ModInt _agililty;
        public ModInt Agility
        {
            get => _agililty;
            set => Update(ref _agililty, value);
        }

        private ModInt _luck;
        public ModInt Luck
        {
            get => _luck;
            set => Update(ref _luck, value);
        }
        //
    }
}
