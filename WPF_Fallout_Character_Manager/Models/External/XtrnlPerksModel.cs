using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                ImageIcons.Add("none", "Resources/vault_boy_thumbsup.png");
                ImageIcons.Add("human", "Resources/TraitsPerksIcons/human1.png");
                ImageIcons.Add("ghoul", "Resources/TraitsPerksIcons/human2.png");
                ImageIcons.Add("robot", "Resources/TraitsPerksIcons/robot1.png");
                ImageIcons.Add("synth", "Resources/TraitsPerksIcons/synth1.png");
                ImageIcons.Add("super mutant", "Resources/TraitsPerksIcons/synth1.png");
                ImageIcons.Add("missing limb", "Resources/TraitsPerksIcons/severed_limbs1.png");
                ImageIcons.Add("blind", "Resources/TraitsPerksIcons/blind1.png");
                ImageIcons.Add("strength", "Resources/TraitsPerksIcons/Strength_icon.png");
                ImageIcons.Add("perception", "Resources/TraitsPerksIcons/Perception_icon.png");
                ImageIcons.Add("endurance", "Resources/TraitsPerksIcons/Endurance_icon.png");
                ImageIcons.Add("charisma", "Resources/TraitsPerksIcons/Charisma_icon.png");
                ImageIcons.Add("intelligence", "Resources/TraitsPerksIcons/Intelligence_icon.png");
                ImageIcons.Add("agility", "Resources/TraitsPerksIcons/Agility_icon.png");
                ImageIcons.Add("luck", "Resources/TraitsPerksIcons/Luck_icon.png");
                ImageIcons.Add("humanoid", "Resources/TraitsPerksIcons/humanoids.png");
                ImageIcons.Add("machine", "Resources/TraitsPerksIcons/machines.png");
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
                return ImageIcons["humanoid"];
            }

            if (prerequisite.Contains("synth", StringComparison.InvariantCultureIgnoreCase) &&
                prerequisite.Contains("robot", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["machine"];
            }

            if (prerequisite.Contains("human", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["human"];
            }

            if (prerequisite.Contains("ghoul", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["ghoul"];
            }

            if (prerequisite.Contains("robot", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["robot"];
            }

            if (prerequisite.Contains("synth", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["synth"];
            }

            if (prerequisite.Contains("super mutant", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["super mutant"];
            }

            if (prerequisite.Contains("missing", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["missing limb"];
            }

            if (prerequisite.Contains("blind", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["blind"];
            }

            if (prerequisite.Contains("strength", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["strength"];
            }

            if (prerequisite.Contains("perception", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["perception"];
            }

            if (prerequisite.Contains("endurance", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["endurance"];
            }

            if (prerequisite.Contains("charisma", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["charisma"];
            }

            if (prerequisite.Contains("intelligence", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["intelligence"];
            }

            if (prerequisite.Contains("agility", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["agility"];
            }

            if (prerequisite.Contains("luck", StringComparison.InvariantCultureIgnoreCase))
            {
                return ImageIcons["luck"];
            }

            return ImageIcons["none"];
        }
        //

        public ObservableCollection<Trait> Traits { get; set; }
        public ObservableCollection<Perk> Perks { get; set; }
        public Dictionary<string, string> ImageIcons { get; set; }
    }

    class Trait : LabeledString
    {
        // constructor
        public Trait(string name = "", string description = "", string wildWastelandDescription = "", string prerequisite = "", string imagePath = "")
            : base(name, description, description)
        {
            Prerequisite = prerequisite;
            WildWastelandDescription = wildWastelandDescription;
            ImagePath = imagePath;

            ConstructNote();
        }

        public Trait(Trait other) : base(other)
        {
            Prerequisite = other.Prerequisite;
            WildWastelandDescription = other.WildWastelandDescription;
            ImagePath = other.ImagePath;

            ConstructNote();
        }
        //

        // method
        public void ConstructNote()
        {
            Note = Value + "\nWild Wasteland: " + WildWastelandDescription;
        }
        //

        // members
        string _prerequisite;
        string Prerequisite
        {
            get => _prerequisite;
            set
            {
                Update(ref _prerequisite, value);
                ConstructNote();
            }
        }

        string _wildWastelandDescription;
        string WildWastelandDescription
        {
            get => _wildWastelandDescription;
            set
            {
                Update(ref _wildWastelandDescription, value);
                ConstructNote();
            }
        }

        string _imagePath;
        string ImagePath
        {
            get => _imagePath;
            set => Update(ref _imagePath, value);
        }
        //
    }

    class Perk : LabeledString
    {
        // constructor
        public Perk(string name = "", string description = "", string repeatDescription = "", int max = 0, string requirement = "", string imagePath = "")
            : base(name, description, description)
        {
            RepeatDescription = repeatDescription;
            Requirement = requirement;
            Max = max;
            ImagePath = imagePath;

            ConstructNote();
        }

        public Perk(Perk other) : base(other)
        {
            RepeatDescription = other.RepeatDescription;
            Requirement = other.Requirement;
            Max = other.Max;
            ImagePath = other.ImagePath;

            ConstructNote();
        }
        //

        // methods
        public void ConstructNote()
        {
            Note = "Requirement: " + Requirement + "\nMax Stacks: " + Max + "\n" + Value;
            if(RepeatDescription != "")
            {
                Note += "\nRepeat: " + RepeatDescription;
            }
        }
        //

        // members
        string _requirement;
        string Requirement
        {
            get => _requirement;
            set
            {
                Update(ref _requirement, value);
                ConstructNote();
            }
        }

        string _repeatDescription;
        string RepeatDescription
        {
            get => _repeatDescription;
            set
            {
                Update(ref _repeatDescription, value);
                ConstructNote();
            }
        }

        int _max;
        int Max
        {
            get => _max;
            set
            {
                Update(ref _max, value);
                ConstructNote();
            }
        }

        string _imagePath;
        string ImagePath
        {
            get => _imagePath;
            set => Update(ref _imagePath, value);
        }
        //
    }
}
