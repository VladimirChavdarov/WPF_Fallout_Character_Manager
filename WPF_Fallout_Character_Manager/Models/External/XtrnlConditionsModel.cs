using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Models.Serialization;
using WPF_Fallout_Character_Manager.Utilities;

namespace WPF_Fallout_Character_Manager.Models.External
{
    class XtrnlConditionsModel : ModelBase
    {
        // constructor
        public XtrnlConditionsModel()
        {
            Conditions = new ObservableCollection<Condition>();

            var diseasesLines = File.ReadAllLines("Resources/Spreadsheets/diseases.csv");

            foreach (var line in diseasesLines.Skip(1))
            {
                var parts = line.Split(';');

                // skip invalid rows
                if (parts.Length < 4)
                    continue;

                string description = parts[1] + "\n\nDuration: " + parts[2] + "\n\nCure: " + parts[3];

                Condition condition = new Condition(
                    name: parts[0],
                    description: description
                    );

                Conditions.Add(condition);
            }

            var lines = File.ReadAllLines("Resources/Spreadsheets/conditions.csv");

            foreach(var line in lines.Skip(1))
            {
                var parts = line.Split(';');

                // skip invalid rows
                if(parts.Length < 2 )
                    continue;

                Condition condition = new Condition(
                    name: parts[0],
                    description: parts[1]
                    );

                Conditions.Add(condition);
            }

            Conditions.SortObservableCollection(x => x.BaseValue.Name);

            Conditions.Add(new Condition("Custom...", "Enter your description here..."));
        }
        //

        // data
        public ObservableCollection<Condition>? Conditions { get; }
        //
    }

    class Condition : ModTypeBase, ISerializable<ConditionDTO>
    {
        // constructor
        public Condition(string name = "NewCondition", string description = "No Description", bool isReadOnly = true, Visibility descriptionVisibility = Visibility.Visible)
        {
            _baseValue = new LabeledString(name, description, description);
            _isReadOnly = isReadOnly;
            _descriptionVisibility = descriptionVisibility;
        }

        public Condition(ConditionDTO dto)
        {
            FromDto(dto);
        }
        //

        // members
        private LabeledString _baseValue;
        public LabeledString BaseValue
        {
            get => _baseValue;
            set => Update(ref _baseValue, value);
        }

        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                Update(ref _isReadOnly, value);
                if(value == false)
                    DescriptionVisibility = Visibility.Visible; // automatically show the description if we enter edit mode.
            } 
        }

        private Visibility _descriptionVisibility;
        public Visibility DescriptionVisibility
        {
            get => _descriptionVisibility;
            set => Update(ref _descriptionVisibility, value);
        }
        //

        // methods
        public Condition Clone() => new Condition
        {
            BaseValue = new LabeledString(this.BaseValue.Name, this.BaseValue.Value, this.BaseValue.Note),
            IsReadOnly = this.IsReadOnly,
            DescriptionVisibility = this.DescriptionVisibility,
        };

        public ConditionDTO ToDto()
        {
            return new ConditionDTO
            {
                BaseValue = this.BaseValue,
                IsReadOnly = this.IsReadOnly,
                DescriptionVisibility = this.DescriptionVisibility,
            };
        }

        public void FromDto(ConditionDTO dto, bool versionMismatch = false)
        {
            BaseValue = dto.BaseValue.Clone();
            IsReadOnly = dto.IsReadOnly;
            DescriptionVisibility = dto.DescriptionVisibility;
        }
        //
    }
}
