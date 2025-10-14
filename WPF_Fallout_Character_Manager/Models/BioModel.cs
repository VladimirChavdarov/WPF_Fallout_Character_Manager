using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models
{
    internal sealed class BioModel : ModelBase
    {
        public BioModel()
        {
            Name = "none";
            Race = "No Race";
            Background = "No Background";
            Backstory = "No Backstory";
            _level = new ModInt("Level", 1, false, "");
            _level.Note = "Levels are a measurement of your character’s experience and adaptation to the wasteland. Throughout the game, the Game Master (GM) may reward the player characters with Experience Points (XP). Whenever you gain 1000 XP, you gain a level. Each level grants you something.";
            _level.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Level));
            _xp = new ModInt("XP", 543, false, "");
            _xp.Note = "The GM may award the players with XP at any time, but is typically awarded when the player characters spend any amount of time resting after completing a quest, encounter, or discovering something new. Whenever you gain XP, if your XP total is lower than any other player character’s total XP, you gain XP equal to the difference between your total and theirs. (Simply put: everyone shares the same amount of XP, defaulting to whoever has the highest). Additionally, the following modifiers are added to the total: Reaching 0 Hit points (10%), Death (1000 XP), Creature Discovery (20%), Location Discovery (20%)";
            _xp.PropertyChanged += (s, e) => OnPropertyChanged(nameof(XP));
            ImageSource = "Resources/Vault_Boy.png";
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => Update(ref _name, value);
        }

        private string _race;
        public string Race
        {
            get => _race;
            set => Update(ref _race, value);
        }

        private string _background;
        public string Background
        {
            get => _background;
            set => Update(ref _background, value);
        }

        private string _backstory;
        public string Backstory
        {
            get => _backstory;
            set => Update(ref _backstory, value);
        }

        private ModInt _level;
        public ModInt Level
        {
            get => _level;
            set => Update(ref _level, value);
        }

        private ModInt _xp;
        public ModInt XP
        {
            get => _xp;
            set => Update(ref _xp, value);
        }

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => Update(ref _imageSource, value);
        }
    }
}
