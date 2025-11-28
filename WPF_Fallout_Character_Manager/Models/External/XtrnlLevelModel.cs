using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Utilities;

namespace WPF_Fallout_Character_Manager.Models.External
{
    enum SkillPointDecider
    {
        LowIntelligence = 0,
        MediumIntelligence = 1,
        HighIntelligence = 2,
    }

    class XtrnlLevelModel : ModelBase
    {
        // constructor
        public XtrnlLevelModel()
        {
            Levels = new ObservableCollection<Level>();

            var lines = File.ReadAllLines("Resources/Spreadsheets/levels.csv");

            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(';');

                // skip invalid rows
                if (parts.Length < 7)
                    continue;

                int[] availableSkillPoints = [Int32.Parse(parts[4]), Int32.Parse(parts[5]), Int32.Parse(parts[6])];

                ProcessBaseValueAndMultiplier(parts[1], SPECIAL.Endurance, out string baseHp, out string hpMultiplier);
                ProcessBaseValueAndMultiplier(parts[2], SPECIAL.Agility, out string baseSp, out string spMultiplier);

                Level level = new Level(
                    levelNum: Int32.Parse(parts[0]),
                    baseHp: Int32.Parse(baseHp),
                    hpMultiplier: Int32.Parse(hpMultiplier),
                    baseSp: Int32.Parse(baseSp),
                    spMultiplier: Int32.Parse(spMultiplier),
                    perkCount: Int32.Parse(parts[3]),
                    availableSkillPoints: availableSkillPoints
                    );

                Levels.Add(level);
            }
        }
        //

        // methods
        private void ProcessBaseValueAndMultiplier(string str, SPECIAL specialStat, out string baseValue, out string multiplier)
        {
            string[] keywords = { "STR", "PER", "END", "CHA", "INT", "AGI", "LCK" };
            string specialString = "+" + keywords[(int)specialStat];

            baseValue = Utils.Between(str, "", specialString);
            str = str.Replace(baseValue + specialString, "");
            multiplier = "1";
            if (str.Length != 0)
            {
                multiplier = Utils.Between(str, "*", "");
            }
        }
        //

        // data
        public ObservableCollection<Level>? Levels { get; }
        //
    }

    class Level : ModTypeBase
    {
        // constructor
        public Level(int levelNum = 0, int baseHp = 0, int hpMultiplier = 1, int baseSp = 0, int spMultiplier = 1, int perkCount = 0, int[] availableSkillPoints = null)
        {
            _levelModInt = new ModInt("Level", levelNum, false, "");
            _levelModInt.Note = "Levels are a measurement of your character’s experience and adaptation to the wasteland. Throughout the game, the Game Master (GM) may reward the player characters with Experience Points (XP). Whenever you gain 1000 XP, you gain a level. Each level grants you something.";
            _baseHp = baseHp;
            _hpMultiplier = hpMultiplier;
            _baseSp = baseSp;
            _spMultiplier = spMultiplier;
            _perkCount = perkCount;

            if(availableSkillPoints.Length != 3)
            {
                throw new ArgumentException("There are only 3 options for skill point amount per level. Maybe there is an issue with parsing data from the .csv file.");
            }
            _availableSkillPoints = availableSkillPoints ?? [0, 0, 0]; // check if availableSkillPoints is null, if yes - make an array of 3 zeros.
        }
        //

        // members
        private ModInt _levelModInt;
        public ModInt LevelModInt
        {
            get => _levelModInt;
        }

        private int _baseHp;
        public int BaseHp
        {
            get => _baseHp;
        }

        private int _hpMultiplier;
        public int HpMultiplier
        {
            get => _hpMultiplier;
        }

        private int _baseSp;
        public int BaseSp
        {
            get => _baseSp;
        }

        private int _spMultiplier;
        public int SpMultiplier
        {
            get => _spMultiplier;
        }

        private int _perkCount;
        public int PerkCount
        {
            get => _perkCount;
        }

        private int[] _availableSkillPoints;
        public int[] AvailableSkillPoints
        {
            get => _availableSkillPoints;
        }
        //

        // methods

        //
    }
}
