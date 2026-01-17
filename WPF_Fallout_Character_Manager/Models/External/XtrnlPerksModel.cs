using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models.External
{
    class XtrnlPerksModel : ModelBase
    {
        // constructor
        public XtrnlPerksModel()
        {
            Traits = new ObservableCollection<Trait>();
            Perks = new ObservableCollection<Perk>();
            ImageIcons = new Dictionary<string, string>();

            {
                ImageIcons.Add("None", "/Resources/vault_boy_thumbsup.png");
                ImageIcons.Add("Human", "/Resources/TraitsPerksIcons/human1.png");
                ImageIcons.Add("Ghoul", "/Resources/TraitsPerksIcons/ghoul2.png");
                ImageIcons.Add("Robot", "/Resources/TraitsPerksIcons/robot1.png");
                ImageIcons.Add("Synth", "/Resources/TraitsPerksIcons/synth1.png");
                ImageIcons.Add("Super Mutant", "/Resources/TraitsPerksIcons/super_mutant1.png");
                ImageIcons.Add("Missing Limb", "/Resources/TraitsPerksIcons/severed_limbs1.png");
                ImageIcons.Add("Blind", "/Resources/TraitsPerksIcons/blind1.png");
                ImageIcons.Add("Strength", "/Resources/TraitsPerksIcons/Strength_icon.png");
                ImageIcons.Add("Perception", "/Resources/TraitsPerksIcons/Perception_icon.png");
                ImageIcons.Add("Endurance", "/Resources/TraitsPerksIcons/Endurance_icon.png");
                ImageIcons.Add("Charisma", "/Resources/TraitsPerksIcons/Charisma_icon.png");
                ImageIcons.Add("Intelligence", "/Resources/TraitsPerksIcons/Intelligence_icon.png");
                ImageIcons.Add("Agility", "/Resources/TraitsPerksIcons/Agility_icon.png");
                ImageIcons.Add("Luck", "/Resources/TraitsPerksIcons/Luck_icon.png");
                ImageIcons.Add("Humanoid", "/Resources/TraitsPerksIcons/humanoids.png");
                ImageIcons.Add("Machine", "/Resources/TraitsPerksIcons/machines.png");
            }

            var traitsLines = File.ReadAllLines("Resources/Spreadsheets/traits.csv");
            foreach (var line in traitsLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 4)
                    continue;

                Trait newTrait = new Trait(
                    name: parts[0],
                    description: parts[2],
                    wildWastelandDescription: parts[3],
                    prerequisite: parts[1],
                    imagePath: DetermineImagePath(parts[1]));

                Traits.Add(newTrait);
            }

            var perksLines = File.ReadAllLines("Resources/Spreadsheets/perks.csv");
            foreach (var line in perksLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 5)
                    continue;

                Perk newPerk = new Perk(
                    name: parts[0],
                    description: parts[3],
                    repeatDescription: parts[4],
                    max: Int32.Parse(parts[1]),
                    requirement: parts[2],
                    imagePath: DetermineImagePath(parts[2])
                    );

                Perks.Add(newPerk);
            }
        }
        //

        // methods
        private string DetermineImagePath(string prerequisite)
        {
            if (prerequisite.Contains("human", StringComparison.InvariantCultureIgnoreCase) &&
                prerequisite.Contains("ghoul", StringComparison.InvariantCultureIgnoreCase) &&
                prerequisite.Contains("super mutant", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["Humanoid"];
            }

            if (prerequisite.Contains("synth", StringComparison.InvariantCultureIgnoreCase) &&
                prerequisite.Contains("robot", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["Machine"];
            }

            if (prerequisite.Contains("human", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["Human"];
            }

            if (prerequisite.Contains("ghoul", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["Ghoul"];
            }

            if (prerequisite.Contains("robot", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["Robot"];
            }

            if (prerequisite.Contains("synth", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["Synth"];
            }

            if (prerequisite.Contains("super mutant", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["Super Mutant"];
            }

            if (prerequisite.Contains("missing", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["Missing Limb"];
            }

            if (prerequisite.Contains("blind", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["Blind"];
            }

            if (prerequisite.Contains("strength", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["Strength"];
            }

            if (prerequisite.Contains("perception", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["Perception"];
            }

            if (prerequisite.Contains("endurance", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["Endurance"];
            }

            if (prerequisite.Contains("charisma", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["Charisma"];
            }

            if (prerequisite.Contains("intelligence", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["Intelligence"];
            }

            if (prerequisite.Contains("agility", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["Agility"];
            }

            if (prerequisite.Contains("luck", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["Luck"];
            }

            return ImageIcons["None"];
        }
        //

        public ObservableCollection<Trait> Traits { get; set; }
        public ObservableCollection<Perk> Perks { get; set; }
        public Dictionary<string, string> ImageIcons { get; set; }
    }

    public class TPCard : LabeledString
    {
        // constructor
        public TPCard(string name = "", string description = "", string extraDescription = "", string prerequisite = "", string cardType = "None", string imagePath = "")
            : base(name, description, description, true)
        {
            Prerequisite = prerequisite;
            ExtraDescription = extraDescription;
            PickImageFromCardType = true;
            CardType = cardType;
            ImagePath = imagePath;
            ShowDescription = false;

            ConstructNote();
            OnPropertyChanged(nameof(DescriptionToShow));
        }

        public TPCard(TPCard other) : base(other)
        {
            Prerequisite = other.Prerequisite;
            ExtraDescription = other.ExtraDescription;
            PickImageFromCardType = other.PickImageFromCardType;
            CardType = other.CardType;
            ImagePath = other.ImagePath;
            ShowDescription = other.ShowDescription;

            ConstructNote();
            OnPropertyChanged(nameof(DescriptionToShow));
        }
        //

        // method
        public virtual void ConstructNote() { }
        //

        // members
        private string _prerequisite;
        public string Prerequisite
        {
            get => _prerequisite;
            set
            {
                Update(ref _prerequisite, value);
                ConstructNote();
            }
        }

        private string _extraDescription;
        public string ExtraDescription
        {
            get => _extraDescription;
            set
            {
                Update(ref _extraDescription, value);
                ConstructNote();
            }
        }

        private bool _pickImageFromCardType;
        public bool PickImageFromCardType
        {
            get => _pickImageFromCardType;
            set => Update(ref _pickImageFromCardType, value);
        }

        private string _cardType;
        public string CardType
        {
            get => _cardType;
            set => Update(ref _cardType, value);
        }

        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set => Update(ref _imagePath, value);
        }

        private bool _showDescription;
        public bool ShowDescription
        {
            get => _showDescription;
            set
            {
                Update(ref _showDescription, value);
                OnPropertyChanged(nameof(ToggleDescriptionVisibility));
            }
        }

        public Visibility ToggleDescriptionVisibility => ShowDescription ? Visibility.Visible : Visibility.Collapsed;

        public virtual string DescriptionToShow => Value;
        //
    }

    public class Trait : TPCard
    {
        // constructor
        public Trait(string name = "", string description = "", string wildWastelandDescription = "", string prerequisite = "", string cardType = "None", string imagePath = "")
            : base(name, description, wildWastelandDescription, prerequisite, cardType, imagePath)
        {
        }

        public Trait(Trait other) : base(other)
        {
        }
        //

        // method
        public override void ConstructNote()
        {
            IsReadOnly = false;
            Note = "";
            if(!Prerequisite.Equals("none", StringComparison.InvariantCultureIgnoreCase))
            {
                Note += "Requirement: " + Prerequisite + "\n\n";
            }
            Note += Value + "\n\nWild Wasteland: " + ExtraDescription;
            IsReadOnly = true;
        }
        //

        // members
        private bool _isWildWastelandToggled;
        public bool IsWildWastelandToggled
        {
            get => _isWildWastelandToggled;
            set
            {
                Update(ref _isWildWastelandToggled, value);
                OnPropertyChanged(nameof(DescriptionToShow));
            }
        }

        public override string DescriptionToShow
        {
            get
            {
                string result = "";
                if (!Prerequisite.Equals("none", StringComparison.InvariantCultureIgnoreCase))
                {
                    result += "Requirement: " + Prerequisite + "\n\n";
                }

                result += Value;

                if (IsWildWastelandToggled)
                {
                    result += Value + "\n\nWild Wasteland: " + ExtraDescription;
                }

                return result;
            }
        }
        //
    }

    public class Perk : TPCard
    {
        // constructor
        public Perk(string name = "", string description = "", string repeatDescription = "", int max = 0, string requirement = "", string cardType = "None", string imagePath = "")
            : base(name, description, repeatDescription, requirement, cardType, imagePath)
        {
            MaxStacks = max;

            ConstructNote();
        }

        public Perk(Perk other) : base(other)
        {
            MaxStacks = other.MaxStacks;

            ConstructNote();
        }
        //

        // methods
        public override void ConstructNote()
        {
            IsReadOnly = false;
            Note = "Requirement: " + Prerequisite + "\nMax Stacks: " + MaxStacks + "\n\n" + Value;
            if(ExtraDescription != "")
            {
                Note += "\n\nRepeat: " + ExtraDescription;
            }
            IsReadOnly = true;
        }
        //

        // members
        public override string DescriptionToShow
        {
            get
            {
                string result = "Requirement: " + Prerequisite + "\n\n" + Value;
                if (ExtraDescription != "")
                {
                    result += "\n\nRepeat: " + ExtraDescription;
                }
                return result;
            }
        }

        int _currentStacks;
        public int CurrentStacks
        {
            get => _currentStacks;
            set
            {
                Update(ref _currentStacks, value);
                OnPropertyChanged(nameof(ReachedMaxStacks));
            }
        }

        int _maxStacks;
        public int MaxStacks
        {
            get => _maxStacks;
            set
            {
                Update(ref _maxStacks, value);
                ConstructNote();
            }
        }

        public bool ReachedMaxStacks => MaxStacks == CurrentStacks ? true : false;
        //
    }
}
