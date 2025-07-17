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
            _level = new ModInt("Level", 1, false);
            _level.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Level));
            _xp = new ModInt("XP", 543, false);
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
