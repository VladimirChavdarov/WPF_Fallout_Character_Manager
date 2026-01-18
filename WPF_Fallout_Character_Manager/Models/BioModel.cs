using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Models.Serialization;

namespace WPF_Fallout_Character_Manager.Models
{
    internal sealed class BioModel : ModelBase
    {
        public BioModel(XtrnlLevelModel xtrnlLevelModel)
        {
            Name = "none";
            Race = "No Race";
            Background = "No Background";
            AppearanceDescription = "";
            Backstory = "";
            Virtues = "";
            Vices = "";
            _xtrnlLevelModel = xtrnlLevelModel;
            LevelNum = 1;
            _xp = new ModInt("XP", 543, false, "");
            _xp.Note = "The GM may award the players with XP at any time, but is typically awarded when the player characters spend any amount of time resting after completing a quest, encounter, or discovering something new. Whenever you gain XP, if your XP total is lower than any other player character’s total XP, you gain XP equal to the difference between your total and theirs. (Simply put: everyone shares the same amount of XP, defaulting to whoever has the highest). Additionally, the following modifiers are added to the total: Reaching 0 Hit points (10%), Death (1000 XP), Creature Discovery (20%), Location Discovery (20%)";
            _xp.PropertyChanged += (s, e) => OnPropertyChanged(nameof(XP));

            ImageSource = "Resources/Vault_Boy.png";

            KarmaCaps = new ObservableCollection<KarmaCap> { new KarmaCap(true) };
        }

        public BioModel(BioModelDTO dto, XtrnlLevelModel xtrnlLevelModel) : this(xtrnlLevelModel)
        {
            FromDto(dto);
        }

        // method
        public void FromDto(BioModelDTO dto)
        {
            Name = dto.Name;
            Race = dto.Race;
            Background = dto.Background;
            AppearanceDescription = dto.AppearanceDescription;
            Backstory = dto.Backstory;
            Virtues = dto.Virtues;
            Vices = dto.Vices;
            LevelNum = dto.LevelNum;
            XP = new ModInt(dto.XP);
            ImageSource = dto.ImageSource;
            KarmaCaps.Clear();
            foreach (bool karmaCapFlag in dto.KarmaCaps)
            {
                KarmaCaps.Add(new KarmaCap(karmaCapFlag));
            }
        }
        
        public BioModelDTO ToDto()
        {
            BioModelDTO dto = new BioModelDTO
            {
                Name = this.Name,
                Race = this.Race,
                Background = this.Background,
                AppearanceDescription = this.AppearanceDescription,
                Backstory = this.Backstory,
                Virtues = this.Virtues,
                Vices = this.Vices,
                LevelNum = this.LevelNum,
                XP = this.XP.ToDto(),
                ImageSource = this.ImageSource,
                KarmaCaps = new List<bool>()
            };

            foreach(KarmaCap cap in this.KarmaCaps)
            {
                dto.KarmaCaps.Add(cap.IsActive);
            }

            return dto;
        }
        //

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

        private string _appearanceDescription;
        public string AppearanceDescription
        {
            get => _appearanceDescription;
            set => Update(ref _appearanceDescription, value);
        }

        private string _backstory;
        public string Backstory
        {
            get => _backstory;
            set => Update(ref _backstory, value);
        }

        private string _virtues;
        public string Virtues
        {
            get => _virtues;
            set => Update(ref _virtues, value);
        }

        private string _vices;
        public string Vices
        {
            get => _vices;
            set => Update(ref _vices, value);
        }

        private string _notes;
        public string Notes
        {
            get => _notes;
            set => Update(ref _notes, value);
        }

        private int _levelNum;
        public int LevelNum
        {
            get => _levelNum;
            set
            {
                Update(ref _levelNum, value);
            }
        }

        //private Level _level;
        public Level Level
        {
            get => _xtrnlLevelModel.Levels.FirstOrDefault(x => x.LevelModInt.BaseValue == _levelNum);
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

        private XtrnlLevelModel _xtrnlLevelModel;
        public ObservableCollection<KarmaCap> KarmaCaps { get; set; }

        public string KarmaCapFrontImagePath => "/Resources/bottlecap_front.png";
        public string KarmaCapBackImagePath => "/Resources/bottlecap_back.png";
    }

    public class KarmaCap : ModTypeBase
    {
        public KarmaCap(bool active)
        {
            IsActive = active;
        }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => Update(ref _isActive, value);
        }
    }
}
