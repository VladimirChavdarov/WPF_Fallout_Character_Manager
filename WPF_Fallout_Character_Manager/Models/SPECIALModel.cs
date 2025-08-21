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
            _special = new Dictionary<SPECIAL, ModInt>
            {
                { SPECIAL.Strength,     new ModInt("Strength", 1, true) },
                { SPECIAL.Perception,   new ModInt("Perception", 2, true) },
                { SPECIAL.Endurance,    new ModInt("Endurance", 3, true) },
                { SPECIAL.Charisma,     new ModInt("Charisma", 4, true) },
                { SPECIAL.Intelligence, new ModInt("Intelligence", 5, true) },
                { SPECIAL.Agility,      new ModInt("Agility", 6, true) },
                { SPECIAL.Luck,         new ModInt("Luck", 7, true) }

            };

            foreach(var keyValue in _special)
            {
                var key = keyValue.Key;
                var value = keyValue.Value;

                value.PropertyChanged += (s, e) => OnPropertyChanged(key.ToString());
            }
        }
        //

        // helpers
        //public int GetModifier(ModInt MainStat)
        //{
        //    return MainStat.Total - 5;
        //}

        public int GetModifier(SPECIAL MainStat)
        {
            return _special[MainStat].Total - 5;
        }
        //

        // Data
        private Dictionary<SPECIAL, ModInt> _special;

        public ModInt Strength
        {
            get => _special[SPECIAL.Strength];
            set => Update(_special[SPECIAL.Strength], value, v => _special[SPECIAL.Strength] = v);
        }

        public ModInt Perception
        {
            get => _special[SPECIAL.Perception];
            set => Update(_special[SPECIAL.Perception], value, v => _special[SPECIAL.Perception] = v);
        }

        public ModInt Endurance
        {
            get => _special[SPECIAL.Endurance];
            set => Update(_special[SPECIAL.Endurance], value, v => _special[SPECIAL.Endurance] = v);
        }

        public ModInt Charisma
        {
            get => _special[SPECIAL.Charisma];
            set => Update(_special[SPECIAL.Charisma], value, v => _special[SPECIAL.Charisma] = v);
        }

        public ModInt Intelligence
        {
            get => _special[SPECIAL.Intelligence];
            set => Update(_special[SPECIAL.Intelligence], value, v => _special[SPECIAL.Intelligence] = v);
        }

        public ModInt Agility
        {
            get => _special[SPECIAL.Agility];
            set => Update(_special[SPECIAL.Agility], value, v => _special[SPECIAL.Agility] = v);
        }

        public ModInt Luck
        {
            get => _special[SPECIAL.Luck];
            set => Update(_special[SPECIAL.Luck], value, v => _special[SPECIAL.Luck] = v);
        }
        //
    }
}
